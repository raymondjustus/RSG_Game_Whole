using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectIncorrect : MonoBehaviour {

    public AudioSource incorrectSound;

    // Use this for initialization
    void Start () {

        incorrectSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            incorrectSound.Play();
        }
    }
}
