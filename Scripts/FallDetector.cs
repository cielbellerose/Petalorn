using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the object entering the trigger zone is the player
        if (other.CompareTag("Player"))
        {
            // call the levellost method from the levelmanager
            levelManager.LevelLost();
        }
    }
}
