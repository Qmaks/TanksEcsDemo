using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CollideSystem))]
public sealed class CollideSystem : UpdateSystem {
    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<CollisionWithComponent>().Without<DamageComponent>();
    }

    public override void OnUpdate(float deltaTime) {

        foreach (var entity in filter)
        {
            ref var collision = ref entity.GetComponent<CollisionWithComponent>();
            if (collision.Other.Has<AttackComponent>())
            {
                ref var attack = ref collision.Other.GetComponent<AttackComponent>();
                ref var damage = ref entity.AddComponent<DamageComponent>();
                damage.Value = attack.Attack;
                entity.RemoveComponent<CollisionWithComponent>();
            }
        }
    }
}