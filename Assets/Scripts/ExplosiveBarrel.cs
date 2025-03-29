using System;
using UnityEngine;

[ExecuteAlways]
public class ExplosiveBarrel : MonoBehaviour
{
    [Header( "Barrel Damage" )]
    [Tooltip("Radius of the damage field")]
    [Range( 0.2f, 4f )]
    public float barrelRadius = 2f;
    [Tooltip("Maximum Damage dealt")]
    [Range( 0.0f, 15f )]
    public float BarrelDamage = 10f;
    [Tooltip("How to decrease damage over distance")]
    public AnimationCurve DamageOverDistance;
    
    [Header( "Barrel Physical effects" )]
    [Tooltip("Radius of the push field")]
    [Range( 0.2f, 4f )]
    public float barrelPhysicalRadius = 2f;
    [Tooltip("Maximum push force")]
    [Range( 0.0f, 5f )]
    public float pushForce = 3f;
    [Tooltip("How to decrease push force over distance")]
    public AnimationCurve pushForceOverDistance;

    private void OnEnable() => BarrelsManager.barrels.Add(this);
    private void OnDisable() => BarrelsManager.barrels.Remove(this);


    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, barrelRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, barrelPhysicalRadius);
    }
}
