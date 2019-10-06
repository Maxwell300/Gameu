using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject sprite;
    void Start()
    {
        sprite.GetComponent<SpriteRenderer>();
        // sprite.disabled;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void interactedWith()
    {
        // sprite.enabled;
    }
}