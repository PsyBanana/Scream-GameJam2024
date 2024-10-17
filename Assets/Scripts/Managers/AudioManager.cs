using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StimpakEssentials
{
    public class AudioManager : SingletonBehaviourDontDestroy<AudioManager>
    {
        [SerializeField] AudioSource _musicAudioSource;
        [SerializeField] AudioSource _globalSFXAudioSource;
        [SerializeField] int _pooledAudioSources = 15;
        [SerializeField] int _maxPooledAudioSources = 30;

        Queue<AudioSource> _sfxPool;
        Dictionary<string, AudioClip> _audioClips;
        Dictionary<string, List<AudioSource>> _activeSources;

        void Awake()
        {
            InitializeAudioManager();
        }

        void InitializeAudioManager()
        {
            _audioClips = new Dictionary<string, AudioClip>();
            _sfxPool = new Queue<AudioSource>();
            _activeSources = new Dictionary<string, List<AudioSource>>();

            for (int i = 0; i < _pooledAudioSources; i++)
            {
                GameObject audioSourceObject = new GameObject("Audio Source Object");
                AudioSource newSource = audioSourceObject.AddComponent<AudioSource>();
                audioSourceObject.transform.SetParent(transform, false);
                audioSourceObject.SetActive(false);
                _sfxPool.Enqueue(newSource);
            }
        }

        public void RegisterAudioClip(string clipName, AudioClip clip)
        {
            if (!_audioClips.ContainsKey(clipName))
            {
                _audioClips.Add(clipName, clip);
            }
        }

        public void PlayMusic(string clipName, float volume = 1f, bool loop = true)
        {
            if (_audioClips.ContainsKey(clipName))
            {
                _musicAudioSource.clip = _audioClips[clipName];
                _musicAudioSource.volume = volume;
                _musicAudioSource.loop = loop;
                _musicAudioSource.Play();
            }
        }

        public void PlayGlobalSFX(string clipName, float volume = 1f)
        {
            if (_audioClips.ContainsKey(clipName))
            {
                _globalSFXAudioSource.PlayOneShot(_audioClips[clipName], volume);
            }
        }

        public void PlaySFXAtPoint(string clipName, Vector3 position, float volume = 1f, bool loop = false)
        {
            if (_audioClips.ContainsKey(clipName))
            {
                AudioSource source = GetPooledAudioSource();
                source.transform.position = position;
                source.clip = _audioClips[clipName];
                source.volume = volume;
                source.spatialBlend = 1f;
                source.loop = loop;
                source.gameObject.SetActive(true);
                source.Play();

                TrackSource(clipName, source);

                if (!loop)
                {
                    StartCoroutine(ReturnToPoolAfterPlaying(source));
                }
            }
        }

        public void PlaySFXAttachedTo(string clipName, Transform target, float volume = 1f, bool loop = false)
        {
            if (_audioClips.ContainsKey(clipName))
            {
                AudioSource source = GetPooledAudioSource();
                source.transform.SetParent(target);
                source.transform.localPosition = Vector3.zero;
                source.clip = _audioClips[clipName];
                source.volume = volume;
                source.spatialBlend = 1f;
                source.loop = loop;
                source.gameObject.SetActive(true);
                source.Play();

                TrackSource(clipName, source);

                if (!loop)
                {
                    StartCoroutine(ReturnToPoolAfterPlaying(source));
                }
            }
        }

        void TrackSource(string clipName, AudioSource source)
        {
            if (!_activeSources.ContainsKey(clipName))
            {
                _activeSources[clipName] = new List<AudioSource>();
            }

            _activeSources[clipName].Add(source);
        }

        public void StopSFX(string clipName)
        {
            if (_activeSources.ContainsKey(clipName))
            {
                foreach (AudioSource source in _activeSources[clipName])
                {
                    source.Stop();
                    source.gameObject.transform.SetParent(null);
                    source.gameObject.SetActive(false);
                    _sfxPool.Enqueue(source);

                    if (_sfxPool.Count > _maxPooledAudioSources)
                    {
                        AudioSource excessSource = _sfxPool.Dequeue();
                        Destroy(excessSource.gameObject);
                    }
                }

                _activeSources[clipName].Clear();
            }
        }

        AudioSource GetPooledAudioSource()
        {
            if (_sfxPool.Count > 0)
            {
                AudioSource audioSource = _sfxPool.Dequeue();
                audioSource.gameObject.SetActive(true);
                return audioSource;
            }
            else
            {
                GameObject audioSourceObject = new GameObject("Audio Source Object");
                AudioSource newSource = audioSourceObject.AddComponent<AudioSource>();
                return newSource;
            }
        }

        IEnumerator ReturnToPoolAfterPlaying(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.Stop();
            source.gameObject.transform.SetParent(null);
            source.gameObject.SetActive(false);

            if (!source.loop)
            {
                _sfxPool.Enqueue(source);

                if (_sfxPool.Count > _maxPooledAudioSources)
                {
                    AudioSource excessSource = _sfxPool.Dequeue();
                    Destroy(excessSource.gameObject);
                }
            }
        }
    }
}
