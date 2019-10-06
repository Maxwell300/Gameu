using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SpriteRenderer SpawnSprite;
    void Start()
    {
        SpawnSprite.GetComponent<SpriteRenderer>();
        SpawnSprite.enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void interactedWith()
    {
        SpawnSprite.enabled = true;
    }
}