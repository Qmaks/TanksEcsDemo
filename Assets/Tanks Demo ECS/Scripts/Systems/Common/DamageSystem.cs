using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DamageSystem))]
public sealed class DamageSystem : UpdateSystem
{
    private Filter filter;
    public override void OnAwake()
    {
        filter = World.Filter.With<HealthComponent>().With<DamageComponent>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            ref var health = ref entity.GetComponent<HealthComponent>();
            ref var damage = ref entity.GetComponent<DamageComponent>();
            var defence = entity.Has<DefenceComponent>() ? entity.GetComponent<DefenceComponent>().Defence : 1;
            
            health.HealthCurrent -= damage.Value * defence;
            
            entity.RemoveComponent<DamageComponent>();
            
            if (health.HealthCurrent <= 0)
            {
                entity.AddComponent<DestroyComponent>();
            }
        }
    }
}