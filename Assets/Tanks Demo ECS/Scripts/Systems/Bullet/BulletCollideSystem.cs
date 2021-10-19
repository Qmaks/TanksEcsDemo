using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCollideSystem))]
public sealed class BulletCollideSystem : UpdateSystem {

    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<BulletComponent>().With<CollisionWithComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        
        foreach (var entity in filter)
        {
            entity.RemoveComponent<TimerComponent>();
            entity.RemoveComponent<CollisionWithComponent>();
        }
    }
}