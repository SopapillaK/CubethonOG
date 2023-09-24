using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerCollision;

public class PlaySound : MonoBehaviour
{
    public delegate void HitPlaySound(Collision playSound);
    public static event HitPlaySound OnHitPlaySound;

    void OnCollisionEnter(Collision playSound)
    {
        if (playSound.collider.tag == "Obstacle")
        {
            if (OnHitPlaySound != null)
            {
                OnHitPlaySound(playSound);
            }
            //movement.enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
