using System;
using Morpeh;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{
    private EntityRef entityRef;
    
    private void Awake()
    {
        entityRef = GetComponent<EntityRef>();
    }

    private void OnCollisionStay(Collision other)
    {
        var otherEntityRef = other.gameObject.GetComponent<EntityRef>();
        if (otherEntityRef)
        {
            if ( otherEntityRef.Entity.Has<HealthComponent>()&&
                !otherEntityRef.Entity.Has<DamageComponent>())
            {
                ref var damage = ref otherEntityRef.Entity.AddComponent<DamageComponent>();
                damage.Value   = 1; //Refactor : Сделать DamageOtherComponent
            }
        }
    }
        
}
