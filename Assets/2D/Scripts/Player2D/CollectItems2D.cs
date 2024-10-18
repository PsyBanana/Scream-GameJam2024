using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems2D : MonoBehaviour
{

    public int NumberOfSticks { get; private set; }


    public void StickCollected()
    {
        NumberOfSticks++;
    }
}
