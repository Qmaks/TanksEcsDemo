using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCollideSystem))]
public sealed class BulletCollideSystem : UpdateSystem {

    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<BulletComponent>().With<CollisionWithComponent>().Without<DestroyComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        
        foreach (var entity in filter)
        {
            entity.AddComponent<DestroyComponent>();
            entity.RemoveComponent<CollisionWithComponent>();
        }
    }
}