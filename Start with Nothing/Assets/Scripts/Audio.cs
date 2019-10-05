using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = Resources.Load<AudioClip>("rose petals");
        audio.Play();
    }
}
