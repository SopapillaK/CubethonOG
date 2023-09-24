using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    public delegate void HitObstacle(Collision collisionInfo);
    public static event HitObstacle OnHitObstacle;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            if (OnHitObstacle != null)
            {
                OnHitObstacle(collisionInfo);
            }
            //movement.enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
