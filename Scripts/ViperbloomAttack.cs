using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperbloomAttack : MonoBehaviour
{
    public GameObject viperProjectile;
    public float shootTimeMax = 5f;
    public float shootTimeMin = 2f;
    public float speed = 50f;
    //public AudioClip viperAttackSFX;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 2f, Random.Range(shootTimeMin, shootTimeMax));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        Vector3 venomPosition = new Vector3(transform.position.x,
            transform.position.y - 0.42f, transform.position.z);
    
        //creates projectile
        GameObject projectile = Instantiate(viperProjectile, 
            venomPosition, transform.rotation) as GameObject;

        //moves projectile forward
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -1 * speed, ForceMode.VelocityChange);

        //sets projectile parent as VenomParent
        projectile.transform.SetParent(
            GameObject.FindGameObjectWithTag("VenomParent").transform);
        
        //AudioSource.PlayClipAtPoint(viperAttackSFX, transform.position);
    }
}
