using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{

    public int life = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
    }
}
