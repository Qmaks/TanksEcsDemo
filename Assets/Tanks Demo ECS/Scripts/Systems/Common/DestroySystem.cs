using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroySystem))]
public sealed class DestroySystem : LateUpdateSystem
{
    private Filter filter;

    [Header("Out Event")]
    [SerializeField]
    private GlobalEventInt EnemyDestroy;
    [SerializeField]
    private GlobalEvent    PlayerDestroy;

    public override void OnAwake()
    {
        filter = World.Filter.With<TransformRef>().With<DestroyComponent>().Without<TimerComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter)
        {
            if (entity.Has<EnemyComponent>()) 
                EnemyDestroy?.NextFrame(entity.ID); 
            
            if (entity.Has<PlayerComponent>()) 
                PlayerDestroy?.NextFrame(entity.ID); 
            
            ref var transform = ref entity.GetComponent<TransformRef>().Transform;
            GameObject.Destroy(transform.gameObject);
            entity.RemoveComponent<DestroyComponent>();
            World.RemoveEntity(entity); 
        }
    }
}