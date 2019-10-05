using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    float moveSpeed = 3f;
    bool moveRight = true;
    public bool up = false;

    // Update is called once per frame
    void Update()
    {
        if (!up)
        {
            if (transform.position.x > 2f)
            {
                moveRight = false;
            }
            if (transform.position.x < -2f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
            }
        } else
        {
            if (transform.position.y > 3f)
            {
                moveRight = false;
            }
            if (transform.position.y < -3f)
            {
                moveRight = true;
            }

            if (moveRight)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
            }
        }
    }
}
