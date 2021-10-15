using System;
using Morpeh;
using UnityEngine;

public class OnCollisionStayRouter : MonoBehaviour
{
    private EntityRef entityRef;
    
    private void Start()
    {
        entityRef = GetComponent<EntityRef>();
    }

    private void OnCollisionStay(Collision other)
    {
        var otherEntityRef = other.gameObject.GetComponent<EntityRef>();
        if (otherEntityRef)
        {
            if (!otherEntityRef.Entity.Has<CollisionWithComponent>())
            {
                ref var collision = ref otherEntityRef.Entity.AddComponent<CollisionWithComponent>();
                collision.Other = entityRef.Entity;
            }
        }
    }
        
}
