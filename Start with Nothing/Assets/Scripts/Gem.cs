using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
  public Animator animator;
  public Collider2D collider;
  // void OnTriggerEnter2D(Collider2D other)
  // {
  //   PlayerMovement controller = other.GetComponent<PlayerMovement>();
  //   if (controller != null) {
  //       controller.ChangeGems();
  //       animator.SetBool("Collected", true);
  //       collider.enabled = false;
  //       Destroy(gameObject, 1.0f);
  //   }
  // }
}
