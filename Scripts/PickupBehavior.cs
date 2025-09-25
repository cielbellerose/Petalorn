using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public static int pickupCount = 0;

    public AudioClip pickupSFX; 
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

            Destroy(gameObject); 

            FindObjectOfType<LevelManager>().LevelWon();
        }
    }

}
