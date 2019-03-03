using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSoundDetector : MonoBehaviour {

    public AudioSource turboSound;
    public AudioSource incorrectSound;


    // Use this for initialization
    void Start () {
        var aSources = GetComponents<AudioSource>();
        turboSound = aSources[0];
        incorrectSound = aSources[1];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("CorrectAns"))
        {

            turboSound.Play();
        }

        if (other.gameObject.CompareTag("IncorrectAns"))
        {
            incorrectSound.Play();
        }
    }
    
}
