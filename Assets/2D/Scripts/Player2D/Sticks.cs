using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticks : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colided");
        CollectItems2D CollectItems = other.GetComponent<CollectItems2D>();
        Debug.Log("Found the Collect SCript");

        if (CollectItems != null)
        {
            Debug.Log("Found the Collect Script on: " + other.name); // Log if the CollectItems2D script was found
            CollectItems.StickCollected();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("CollectItems2D script not found on: " + other.name); // Log if the script was not found
        }
    }

}
