using UnityEngine;

public class PlayerDetection : BaseInteractor
{

    [Tooltip("Camera Transform gets auto referenced if null.")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] float _interactRange = 5f;
    [SerializeField] LayerMask _interactionLayer;

    IInteractable _currentInteractable;

    CameraMover _cameraMover;

    private void Awake()
    {
        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
        }

        _cameraMover = GetComponentInParent<CameraMover>();
    }

    void Update()
    {
        DetectInteractable();
        
        if (Input.GetKeyDown(KeyCode.E) && !_cameraMover.IsMoving) // Prevent input if the camera is moving
        {
            Interact();
        }
    }

    public override void DetectInteractable()
    {
        if (!CanInteract)
        {
            return;
        }

        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * _interactRange, Color.red, 5f);

        if (Physics.Raycast(ray, out hit, _interactRange, _interactionLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (_currentInteractable != interactable)
                {
                    _currentInteractable?.OnFocus(this);
                    _currentInteractable = interactable;
                    _currentInteractable.OnFocus(this);
                }
            }
            else
            {
                ClearInteractable();
            }
        }
        else
        {
            ClearInteractable();
        }
    }

    public override void Interact()
    {
        _currentInteractable?.OnInteract(this);
    }

    void ClearInteractable()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.OnUnfocus(this);
            _currentInteractable = null;
        }
    }
}