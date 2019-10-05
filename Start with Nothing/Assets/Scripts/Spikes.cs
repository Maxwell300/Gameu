using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
  void OnTriggerStay2D(Collider2D other)
  {
    PlayerMovement controller = other.GetComponent<PlayerMovement>();
    if(controller != null) {
         controller.Died();
    }
  }
}
