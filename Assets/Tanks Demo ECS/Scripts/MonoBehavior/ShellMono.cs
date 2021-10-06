using System;
using System.Collections;
using System.Collections.Generic;
using Morpeh;
using UnityEngine;

public class ShellMono : MonoBehaviour
{
    public Entity Entity;
    private void OnTriggerEnter(Collider other)
    {
        // Destroy the shell.
        if (!Entity.Has<DestroyComponent>())
            Entity.AddComponent<DestroyComponent>();

        var entityRef = other.gameObject.GetComponent<EntityRef>();
        if (entityRef)
        {
            if (entityRef.Entity.Has<EnemyComponent>())
            {
                if (!entityRef.Entity.Has<DamageComponent>())
                {
                   ref var damage = ref entityRef.Entity.AddComponent<DamageComponent>();
                   damage.Value = Entity.GetComponent<AttackComponent>().Attack;
                }
            }
        }
    }
}
