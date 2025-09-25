using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperBehavior : MonoBehaviour
{
    public int health = 2;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Projectile"))
        {
            doDamage();
        }
    }

    public void doDamage()
    {
        health -= 1;

        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
