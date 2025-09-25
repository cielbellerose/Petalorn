using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class robotBehavior : MonoBehaviour
{
    public enum FSMStates
    {
        Idle,
        Speak,
        Spin,
        Celebrate
    }

    public FSMStates currentState;
    
    public float speakTime = 5f;
    public Text robotText; 

    public AudioClip speakSFX;
    public AudioClip happySFX;
    public AudioClip sadSFX;

    public string message = " ... ";

    Animator anim;
    float randNum;
    float elapsedTime = 0.0f;
    bool spin = true;

    void Start()
    {
        currentState = FSMStates.Speak;

        anim = GetComponent<Animator>();

        randNum = Random.Range(5.0f, 15.0f);
        Debug.Log(randNum);
    }

    void Update()
    {
        switch(currentState)
        {
            case FSMStates.Idle:
                break;
            case FSMStates.Speak:
                UpdateSpeakState();
                break;
            case FSMStates.Spin:
                UpdateSpinState();
                break;
            case FSMStates.Celebrate:
                UpdateCelebrateState();
                break;
        }

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= randNum)
        {
            currentState = FSMStates.Spin;
        }

        if(!spin)
        {
            elapsedTime = 0.0f;
        }
    }

    void UpdateSpeakState()
    {
        anim.SetInteger("AnimState", 1);
        spin = false;

        Invoke("SpeakMessage", 0.5f);

        currentState = FSMStates.Idle;
    }

    void UpdateSpinState()
    {
        spin = false;
        randNum = Random.Range(5.0f, 15.0f);
        Debug.Log(randNum);

        anim.SetInteger("AnimState", 2);

        Invoke("StopSpin", 1f);

        currentState = FSMStates.Idle;
    }

    void UpdateCelebrateState()
    {
        anim.SetInteger("AnimState", 3);
        AudioSource.PlayClipAtPoint(happySFX, transform.position);

        currentState = FSMStates.Idle;
    }

    void SpeakMessage()
    {
        robotText.text = message;
        robotText.gameObject.SetActive(true);

        AudioSource.PlayClipAtPoint(speakSFX, transform.position);

        Invoke("SpeakAnimStop", speakTime - (speakTime - speakTime/5));
        Invoke("StopSpeaking", speakTime - (speakTime - speakTime/10));
    }

    void StopSpeaking()
    {
        robotText.gameObject.SetActive(false);
        spin = true;
    }

    void SpeakAnimStop()
    {
        anim.SetInteger("AnimState", 0);
    }

    void StopSpin()
    {
        anim.SetInteger("AnimState", 0);
        spin = true;
    }

    public void Win()
    {
        currentState = FSMStates.Celebrate;
    }

    public void Speak()
    {
        currentState = FSMStates.Speak;
    }

    // public void Lose()
    // {
    //     AudioSource.PlayClipAtPoint(sadSFX, transform.position);
    // }

}
