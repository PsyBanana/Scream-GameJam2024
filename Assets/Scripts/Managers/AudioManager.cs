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
        Dictionary<AudioClip, List<AudioSource>> _activeSources;

        void Awake()
        {
            InitializeAudioManager();
        }

        void InitializeAudioManager()
        {
            _sfxPool = new Queue<AudioSource>();
            _activeSources = new Dictionary<AudioClip, List<AudioSource>>();

            for (int i = 0; i < _pooledAudioSources; i++)
            {
                GameObject audioSourceObject = new GameObject("Audio Source Object");
                AudioSource newSource = audioSourceObject.AddComponent<AudioSource>();
                audioSourceObject.transform.SetParent(transform, false);
                audioSourceObject.SetActive(false);
                _sfxPool.Enqueue(newSource);
            }
        }

        public void PlayMusic(AudioClip audioClip, float volume = 1f, bool loop = true)
        {
            if (audioClip != null)
            {
                _musicAudioSource.clip = audioClip;
                _musicAudioSource.volume = volume;
                _musicAudioSource.loop = loop;
                _musicAudioSource.Play();
            }
        }

        public void PlayGlobalSFX(AudioClip audioClip, float volume = 1f)
        {
            if (audioClip != null)
            {
                _globalSFXAudioSource.PlayOneShot(audioClip, volume);
            }
        }

        public void PlaySFXAtPoint(AudioClip audioClip, Vector3 position, float volume = 1f, bool loop = false)
        {
            if (audioClip != null)
            {
                AudioSource source = GetPooledAudioSource();
                source.transform.position = position;
                source.clip = audioClip;
                source.volume = volume;
                source.spatialBlend = 1f;
                source.loop = loop;
                source.gameObject.SetActive(true);
                source.Play();

                TrackSource(audioClip, source);

                if (!loop)
                {
                    StartCoroutine(ReturnToPoolAfterPlaying(source));
                }
            }
        }

        public void PlaySFXAttachedTo(AudioClip audioClip, Transform target, float volume = 1f, bool loop = false)
        {
            if (audioClip != null)
            {
                AudioSource source = GetPooledAudioSource();
                source.transform.SetParent(target);
                source.transform.localPosition = Vector3.zero;
                source.clip = audioClip;
                source.volume = volume;
                source.spatialBlend = 1f;
                source.loop = loop;
                source.gameObject.SetActive(true);
                source.Play();

                TrackSource(audioClip, source);

                if (!loop)
                {
                    StartCoroutine(ReturnToPoolAfterPlaying(source));
                }
            }
        }

        void TrackSource(AudioClip audioClip, AudioSource source)
        {
            if (!_activeSources.ContainsKey(audioClip))
            {
                _activeSources[audioClip] = new List<AudioSource>();
            }

            _activeSources[audioClip].Add(source);
        }

        public void StopSFX(AudioClip audioClip)
        {
            if (_activeSources.ContainsKey(audioClip))
            {
                foreach (AudioSource source in _activeSources[audioClip])
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

                _activeSources[audioClip].Clear();
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
