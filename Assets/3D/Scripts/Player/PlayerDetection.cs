using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    public float rayDistance = 5f;

    public Camera playerCamera; // Reference to the player's camera
    public ConectionPause Conection;

    [Header("MoveCamera")]
    public Transform PlayerPosition; // Reference to the player's position
    public Transform TargetPosition; // Reference to the target position (computer)
    public float transitionSpeed = 3.5f; // Speed of the camera transition
    private bool isAtTarget = false; // Track if the camera is at the target position
    private bool isMoving = false; // Track if the camera is currently moving

    void Start()
    {
        Conection = GameObject.Find("ScenesManager").GetComponent<ConectionPause>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isMoving) // Prevent input if the camera is moving
        {
            ShootRaycast();
        }
    }

    void ShootRaycast()
    {
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        Debug.DrawRay(origin, direction * rayDistance, Color.red, 5f);

        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Computer"))
            {
                Debug.Log("Interacting with Computer!");
                if (!Conection.isConnected)
                {
                    Conection.SetConnectionStatus(true); // Connect to the computer
                    StartCoroutine(MoveCamera(TargetPosition.position)); // Move to the computer
                   
                }
                else
                {
                     Conection.SetConnectionStatus(false); 
                    StartCoroutine(MoveCamera(PlayerPosition.position)); 
                  
                }
            }
        }
    }

    private IEnumerator MoveCamera(Vector3 target)
    {
        isMoving = true; 

        Vector3 startPosition = playerCamera.transform.position; 
        float journeyLength = Vector3.Distance(startPosition, target); 
        float startTime = Time.time; 

        while (Vector3.Distance(playerCamera.transform.position, target) > 0.01f) 
        {
            float distCovered = (Time.time - startTime) * transitionSpeed; 
            float fractionOfJourney = distCovered / journeyLength; 

            playerCamera.transform.position = Vector3.Lerp(startPosition, target, fractionOfJourney); 
            yield return null; 
        }

        // Ensure the camera ends exactly at the target position
        playerCamera.transform.position = target;

        
        isAtTarget = (target == TargetPosition.position); // Check if it's now at the computer
        isMoving = false;
    }
}