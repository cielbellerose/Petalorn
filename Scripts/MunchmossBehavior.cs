using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunchmossBehavior : MonoBehaviour
{
    public float speed = 2f; 
    public int damageAmount = 5;
    public int health = 2;
    public int startRotation = 180;
    public float followDistance = 5f;
    public float stunDuration = 2f;
    public AudioClip chompSFX;
    //public AudioClip hurtSFX;

    //distance where munchmoss can chomp/attack
    public float chompDistance = 1f;

    [Range(0.001f, 0.01f)]
    public float shimmyAmount = 0.001f;
    public float yValue = -0.465f;
    public Transform player;

    Animator m_Animator;
    bool follow = false;
    bool freeze = false;
    float originalSpeed;


    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        m_Animator = GetComponent<Animator>();
        originalSpeed = speed;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < followDistance){
            //emerge
            m_Animator.SetInteger("AnimState", 1);
            follow = true;
        }

        if(distance > chompDistance && follow && !freeze)
        {
            //move towards and turn towards player
            transform.LookAt(player);
            transform.Rotate(0,startRotation,0);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            
            //shimmy movement
            transform.position = Vector3.Lerp(transform.position - new Vector3(shimmyAmount,0,shimmyAmount), 
                transform.position - new Vector3(-shimmyAmount,0,-shimmyAmount),
                (Mathf.PingPong(Time.time, 1) * 2));
            transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //apply damage to player
            var playerHealth = other.GetComponent<Playerhealth>();
            playerHealth.TakeDamage(damageAmount);
            
            //chomp sound
            AudioSource.PlayClipAtPoint(chompSFX, Camera.main.transform.position);
        }

        if(other.CompareTag("Projectile"))
        {
            if(follow)
            {
                doDamage();
                //hurt sound
                //AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position);
            }
        }
    }

    private void Stun()
    {
        if(follow)
        {
            //animate stun
            m_Animator.SetInteger("AnimState", 2);

            //freeze
            speed = 0;
            freeze = true;
            Invoke("unFreeze", stunDuration);
        }
    }

    private void unFreeze()
    {
        freeze = false;
        speed = originalSpeed;

        m_Animator.SetInteger("AnimState", 0);
    }

    private void doDamage()
    {
        health -= 1;
        
        if(health == 1)
        {
            Stun();
        }

        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
