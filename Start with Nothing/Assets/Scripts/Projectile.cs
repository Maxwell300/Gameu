using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  Rigidbody2D rigidBody2D;
  public ParticleSystem hitEffect;
    public PlayerMovement player;
    PlayerMovement test;


    void Awake()
  {
    rigidBody2D = GetComponent<Rigidbody2D>();
        test = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
  void Update()
  {
    if (transform.position.magnitude > 1000f)
    {
      Destroy(gameObject);
    }
  }

  public void Shoot(bool facingRight, float force)
  {
    if (facingRight)
    {
      Vector2 direction = new Vector2(1, 0);
      rigidBody2D.AddForce(direction * force);
    }
    else
    {
      Vector2 direction = new Vector2(-1, 0);
      transform.Rotate(0f, 180f, 0f);
      rigidBody2D.AddForce(direction * force);
    }
  }

  void OnTriggerEnter2D(Collision2D other)
  {

  }

  void OnTriggerStay2D(Collider2D other)
  {
        
        Target target = other.GetComponent<Target>();
        if(target != null)
        {
            test.TargetCount();
        }
    hitEffect = Instantiate(hitEffect, rigidBody2D.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
