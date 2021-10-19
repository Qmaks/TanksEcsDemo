using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TimerSystem))]
public sealed class TimerSystem : LateUpdateSystem {
    
    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<TimerComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter)
        {
            ref var timer = ref entity.GetComponent<TimerComponent>();
            timer.Timer -= deltaTime;
            if (timer.Timer <= 0f)
            {
                entity.RemoveComponent<TimerComponent>();
            }
        }
    }
}