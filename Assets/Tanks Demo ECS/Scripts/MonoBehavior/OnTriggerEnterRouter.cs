using System;
using System.Collections;
using System.Collections.Generic;
using Morpeh;
using UnityEngine;

public class OnTriggerEnterRouter : MonoBehaviour
{
    //public Entity Entity;
    private Entity entity;
    
    private void Start()
    {
        entity = GetComponent<EntityRef>().Entity;
    }

    private void OnTriggerEnter(Collider other)
    {
        var entityRef = other.gameObject.GetComponent<EntityRef>();
        if (entityRef)
        {
            if (!entity.Has<CollisionWithComponent>())
            {
                ref var collision1 = ref entity.AddComponent<CollisionWithComponent>();
                collision1.Other = entityRef.Entity;
            }

            if (!entityRef.Entity.Has<CollisionWithComponent>())
            {
                ref var collision2 = ref entityRef.Entity.AddComponent<CollisionWithComponent>();
                collision2.Other = entity;
            }
        }
    }
}
