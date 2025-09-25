using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLossBehavior : MonoBehaviour
{
    public int healthLossAmount = 10;
    public AudioClip healthLossSFX; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 90 * Time.deltaTime);

        if (transform.position.y < Random.Range(1.0f, 3.0f))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(healthLossSFX, transform.position);

            var playerHealth = other.GetComponent<Playerhealth>();
            playerHealth.TakeDamage(healthLossAmount);

            Destroy(gameObject, 0.5f);
        }
    }
}
