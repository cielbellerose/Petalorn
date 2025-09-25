using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class shootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 50;
    public float shootRate = 0.5f;
    public AudioClip slimeAttackSFX;
    public GameObject arm;
    public GameObject shootArm;
    public Slider rechargeSlider;
    float elapsedTime = 0f;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        rechargeSlider.gameObject.SetActive(false);
        rechargeSlider.maxValue = shootRate;
        rechargeSlider.value = shootRate;

        m_Animator = shootArm.GetComponent<Animator>();
        m_Animator.SetInteger("AnimState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else
    
        elapsedTime += Time.deltaTime;
        if(elapsedTime <= shootRate)
        {
            rechargeSlider.value = elapsedTime;
        }
        else 
        {
            rechargeSlider.gameObject.SetActive(false);
        }
    }

    void Shoot()
    {
        if(elapsedTime >= shootRate)
        {
            m_Animator.SetInteger("AnimState", 1);
            Debug.Log("Shot!");

            Invoke("AnimationOver", shootRate);

            Invoke("ShootProjectile", 0.1f);

            elapsedTime = 0.0f;
            rechargeSlider.gameObject.SetActive(true);
            rechargeSlider.value = elapsedTime;
        }
    }

    void ShootProjectile()
    {
        //create projectile
        GameObject projectile = Instantiate
            (projectilePrefab, transform.position + transform.forward, 
            transform.rotation) as GameObject;

        //add forward motion
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

        //set parent object
        projectile.transform.SetParent(
                GameObject.FindGameObjectWithTag("ProjectileParent").transform);

        //slime sound effect
        AudioSource.PlayClipAtPoint(slimeAttackSFX, transform.position);
    }

    void AnimationOver()
    {
        m_Animator.SetInteger("AnimState", 0);
    }

}
