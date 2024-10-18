using System.Collections;
using UnityEngine;
using ScriptableObjectArchitecture;

public class CameraMover : MonoBehaviour, IGameEventListener<Vector3>
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _transitionDuration = 3.5f; // Speed of the camera transition
    [SerializeField] Vector3GameEvent _onComputerInteracted = default(Vector3GameEvent);

    bool _isAtTarget = false; // Track if the camera is at the target position
    public bool IsMoving { get; private set; } = false; // Track if the camera is currently moving

    private void OnEnable()
    {
        _onComputerInteracted.AddListener(this);
    }

    public void MoveCameraTo(Vector3 target)
    {
        StartCoroutine(MoveCamera(target));
    }

    public void MoveCameraToPlayer()
    {
        StartCoroutine(MoveCamera(_playerTransform.position));
    }

    public void OnEventRaised(Vector3 value)
    {
        MoveCameraTo(value);
    }

    private IEnumerator MoveCamera(Vector3 target)
    {
        IsMoving = true;

        Vector3 startPosition = transform.position;
        float time = 0f;

        while (time < _transitionDuration)
        {
            transform.position = Vector3.Lerp(startPosition, target, time / _transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera ends exactly at the target position
        transform.position = target;


        _isAtTarget = (target == transform.position);
        IsMoving = false;
    }

    private void OnDisable()
    {
        _onComputerInteracted.RemoveListener(this);
    }
}
