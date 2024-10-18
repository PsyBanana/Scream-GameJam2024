using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2D : MonoBehaviour
{

    public bool playerCanDoActions;

    void Start()
    {
        StartCoroutine(CheckGameState)
    }

    void Update()
    {
        
    }
}
