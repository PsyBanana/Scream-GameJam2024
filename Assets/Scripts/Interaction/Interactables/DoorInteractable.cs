using System.Collections;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform _doorPivotTransform;
    [SerializeField] float _doorOpenAngle = 120f;
    [SerializeField] float _openCloseSpeed;

    bool _isOpen;
    bool _isMoving;
    Quaternion _closedRotation;
    Quaternion _openRotation;

    private void Start()
    {
        _closedRotation = _doorPivotTransform.localRotation;
        _openRotation = _closedRotation * Quaternion.Euler(0f, _doorOpenAngle, 0f);
    }

    public void OnFocus(IInteractor interactor)
    {
        
    }

    public void OnInteract(IInteractor interactor)
    {
        OpenCloseDoor();
    }

    public void OnUnfocus(IInteractor interactor)
    {
        
    }

    void OpenCloseDoor()
    {
        if (_isMoving)
        {
            return;
        }

        if (_isOpen)
        {
            StartCoroutine(RotateDoor(_openRotation, _closedRotation));
        }
        else
        {
            StartCoroutine(RotateDoor(_closedRotation, _openRotation));
        }

        _isOpen = !_isOpen;
    }

    IEnumerator RotateDoor(Quaternion fromRotation, Quaternion toRotation)
    {
        float elapsedTime = 0f;
        _isMoving = true;

        while (elapsedTime < _openCloseSpeed)
        {
            _doorPivotTransform.localRotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / _openCloseSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _doorPivotTransform.localRotation = toRotation;
        _isMoving = false;
    }
}
