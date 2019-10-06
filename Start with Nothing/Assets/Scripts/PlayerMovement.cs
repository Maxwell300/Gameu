using System.Collections;
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
  public Sprite crownSprite;
  public Cinemachine.CinemachineVirtualCamera playerCamera;

  AudioSource audio;
  public AudioClip loop1;
  public AudioClip loop2;
  public AudioClip loop3;
  public AudioClip loop4;
  public AudioClip loop5;
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
    audio = GetComponent<AudioSource>();
    audio.clip = loop1;
    audio.Play();

    controller = this.GetComponent<CharacterController2D>();
  }

  // Update is called once per frame
  void Update()
  {
    rigidBody2D = controller.GetRigidBody2D();
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

    if (controller.disabled)
    {
        // add animator trigger here for item get animation
        animator.SetFloat("Speed", 0f);
    }

    if (!controller.disabled)
    {
      animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

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

    hitTarget = Timer(ref hitTarget, ref targetHitTimer);
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
    MovingPlatform platform = other.gameObject.GetComponent<MovingPlatform>();
    if (platform != null)
    {
        this.transform.parent = null;
    }
 }

  void OnCollisionStay2D(Collision2D other)
  {
    if (other.gameObject.tag == "Ground")
    {
      animator.SetBool("isJumping", false);
    }
    MovingPlatform platform = other.gameObject.GetComponent<MovingPlatform>();
    if (platform != null)
    {
        this.transform.parent = other.transform;
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
        Target target = other.gameObject.GetComponent<Target>();
        if (target)
        {
            TargetCount();
        }
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
    audio.clip = loop2;
    audio.Play();
    controller.disabled = true;
    controller.animationTimer = 2.0f;
    animator.SetTrigger("eyeAnimation");
  }

    public void GunCollected()
    {
      this.GetComponent<SpriteRenderer>().sprite = gunSprite;
      hasGun = true;
      audio.clip = loop3;
      audio.Play();
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
        targetHitTimer = 0.1f;
        hitTarget = true;
    }

    public void CanFly()
    {
        this.GetComponent<SpriteRenderer>().sprite = wingsSprite;
        abilityToFly = true;
        abilityToJump = false;
        audio.clip = loop4;
        audio.Play();
    }
    public void Crown()
    {
        this.GetComponent<SpriteRenderer>().sprite = crownSprite;
        audio.clip = loop5;
        audio.Play();
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
    public int getTargetsHit()
    {
        return targetsHit;
    }
    public void resetTargetsHit()
    {
        targetsHit = 0;
    }
}
