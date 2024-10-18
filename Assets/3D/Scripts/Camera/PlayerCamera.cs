using UnityEngine;
using StimpakEssentials;

public class PlayerCamera : SingletonBehaviour<PlayerCamera>
{
    public float sensibility;

    public Transform orientation;

    float _minVerticalAngle = -90f;
    float _maxVerticalAngle = 90f;

    float _minHorizontalAngle = float.NegativeInfinity;
    float _maxHorizontalAngle = float.PositiveInfinity;

    float xRotation;
    float yRotation;

    float _defaultMinVerticalAngle;
    float _defaultMaxVerticalAngle;
    float _defaultMinHorizontalAngle;
    float _defaultMaxHorizontalAngle;


    void Start()
    {
        _defaultMinVerticalAngle = _minVerticalAngle;
        _defaultMaxVerticalAngle = _maxVerticalAngle;
        _defaultMinHorizontalAngle = _minHorizontalAngle;
        _defaultMaxHorizontalAngle = _maxHorizontalAngle;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

 
    void Update()
    {
        HandleCameraMovement();
    }

    void HandleCameraMovement()
    {
        //Mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensibility;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensibility;

        // Camera rotation & orientation
        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, _minVerticalAngle, _maxVerticalAngle);
        yRotation = Mathf.Clamp(yRotation, _minHorizontalAngle, _maxHorizontalAngle);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetRotationConstraints(float minPitch = float.NaN, float maxPitch = float.NaN, float minYaw = float.NaN, float maxYaw = float.NaN)
    {
        // Only update the pitch constraints if values are provided (not NaN)
        if (!float.IsNaN(minPitch)) _minVerticalAngle = minPitch;
        if (!float.IsNaN(maxPitch)) _maxVerticalAngle = maxPitch;

        // Only update the yaw constraints if values are provided (not NaN)
        if (!float.IsNaN(minYaw)) _minHorizontalAngle = minYaw;
        if (!float.IsNaN(maxYaw)) _maxHorizontalAngle = maxYaw;
    }

    public void ResetRotationConstraints()
    {
        _minVerticalAngle = _defaultMinVerticalAngle;
        _maxVerticalAngle = _defaultMaxVerticalAngle;
        _minHorizontalAngle = _defaultMinHorizontalAngle;
        _maxHorizontalAngle = _defaultMaxHorizontalAngle;
    }
}
