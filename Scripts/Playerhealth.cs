using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerhealth : MonoBehaviour
{
    public int startingHealth = 100;
    public Slider healthSlider;
    int currentHealth;


    void Start()
    {
        currentHealth = startingHealth;    
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
        }
        if(currentHealth < 0 || currentHealth == 0)
        {
            Debug.Log("Player was killed!");
            FindObjectOfType<LevelManager>().LevelLost();
        }
    }

    public void TakeHealth(int healthAmount)
    {
        if (currentHealth < 100)
        {
            currentHealth += healthAmount;
            healthSlider.value = Mathf.Clamp(currentHealth, 0, 100);
        }

        Debug.Log("Current Health: " + currentHealth);
    }

    //add player dies
}
