using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public delegate void HitColorChange(Collision colorChange);
    public static event HitColorChange OnHitColorChange;

    void OnCollisionEnter(Collision colorChange)
    {
        if (colorChange.collider.tag == "Obstacle")
        {
            if (OnHitColorChange != null)
            {
                OnHitColorChange(colorChange);
            }
            //movement.enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
