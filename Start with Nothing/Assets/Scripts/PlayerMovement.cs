﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

  public CharacterController2D controller;
  public Animator animator;
  public Sprite eyesLegsSprite;
    public Sprite gunSprite;
    public Sprite wingsSprite;
    public Cinemachine.CinemachineVirtualCamera playerCamera;

  Rigidbody2D rigidBody2D;

  public float runSpeed = 40f;

  float horizontalMove = 0f;
  bool abilityToJump = false;
  bool hasGun = false;
  bool abilityToFly = false;
  bool jump = false;
  bool flying = false;
  bool dead = false;

  int numberOfTargets = 2;
  int targetsHit = 0;
  bool hitTarget = false;
  float targetHitTimer;

  bool shooting;
  float shootTimer;

  public GameObject projectilePreFab;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    rigidBody2D = controller.GetRigidBody2D();
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    shooting = Timer(ref shooting, ref shootTimer);

    if (Input.GetButtonDown("Jump") && abilityToJump == true)
    {
      jump = true;
    }
    if (Input.GetButton("Jump") && abilityToFly == true)
    {
        flying = true;
    } else
    {
        flying = false;
    }

    if (dead)
    { 
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    if (Input.GetButtonDown("Fire1") && hasGun == true)
    {
      Shoot();
    }

    Timer(ref hitTarget, ref targetHitTimer);
    if (targetsHit == numberOfTargets)
    {
        Debug.Log("TargetsHit reached");
    }
        Debug.Log(rigidBody2D.gravityScale);
 }

  public void OnLanding()
  {
    animator.SetBool("isJumping", false);
  }

  void OnCollisionExit2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      animator.SetBool("isJumping", true);
    }
  }

  void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      animator.SetBool("isJumping", false);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {

  }

  void FixedUpdate()
  {
    controller.Move(horizontalMove * Time.deltaTime, false, jump, flying, abilityToFly);
    jump = false;
  }

  void LateUpdate()
  {

  }

  public void EyesLegs()
  {
    this.GetComponent<SpriteRenderer>().sprite = eyesLegsSprite;
    abilityToJump = true;
    playerCamera.Priority = 100;
  }

    public void GunCollected()
    {
        this.GetComponent<SpriteRenderer>().sprite = gunSprite;
        hasGun = true;
    }
  void Shoot()
  {
    GameObject projectileObject;
    Projectile projectile;
    if (shooting)
    {
      return;
    }
    if (controller.m_FacingRight)
    {
      projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * 0.3f + Vector2.right * 1f, Quaternion.identity);
    }
    else
    {
      projectileObject = Instantiate(projectilePreFab, rigidBody2D.position + Vector2.up * 0.3f + Vector2.right * -1f, Quaternion.identity);
    }
    projectile = projectileObject.GetComponent<Projectile>();
    projectile.Shoot(controller.m_FacingRight, 800);  //second number is speed of projectile
    shooting = true;
    shootTimer = 0.25f;

    animator.SetTrigger("Shooting");
  }
    public void TargetCount()
    {
        if(hitTarget)
        {
            return;
        }
        targetsHit++;
        targetHitTimer = 0.5f;
        hitTarget = true;
    }

    public void CanFly()
    {
        this.GetComponent<SpriteRenderer>().sprite = wingsSprite;
        abilityToFly = true;
        abilityToJump = false;
    }

    public void Died()
    {
        dead = true;
        Debug.Log("died");
    }

    public bool Timer(ref bool isChanging, ref float timer)
  {
    if (isChanging)
    {
      timer -= Time.deltaTime;
      if (timer < 0)
      {
        isChanging = false;
      }
    }
    return isChanging;
  }
}
