using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    public bool eyesLegs;
    public bool hasGun;
    public bool canFly;
    public Door door;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement controller = other.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            gm.lastCheckPointPos = transform.position;
            if(eyesLegs)
            {
                controller.SecondEyesLegs();
            }
            if(hasGun == true)
            {
                controller.SecondGunCollected();
                door.Destroy();
            }
            if(canFly)
            {
               controller.SecondCanFly();
            }
            Destroy(gameObject);
        }
    }
}
