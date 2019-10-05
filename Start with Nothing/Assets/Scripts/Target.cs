using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  void OnCollisionEnter2D(Collision2D other)
  {
    Projectile projectile = other.gameObject.GetComponent<Projectile>();
    if (projectile)
    {
      PlayerMovement player = GetComponent<PlayerMovement>();
      player.TargetCount();
      Destroy(gameObject);
    }
  }
}
