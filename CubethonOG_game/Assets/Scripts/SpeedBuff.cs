using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ColorChange")]
public class SpeedBuff : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        MeshRenderer obstacleMR = target.GetComponent<MeshRenderer>();
        Material material = obstacleMR.material;
        obstacleMR.material = Instantiate(material);
        obstacleMR.material.color = Color.blue;
    }
}
