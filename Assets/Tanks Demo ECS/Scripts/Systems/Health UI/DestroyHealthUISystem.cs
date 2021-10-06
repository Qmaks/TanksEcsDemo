
using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DestroyHealthUISystem))]
public sealed class DestroyHealthUISystem : LateUpdateSystem
{
    private Filter filter;
    
    public override void OnAwake() {
        filter = World.Filter.With<HealthUIComponent>().With<DestroyComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        
        foreach (var entity in filter)
        {
            ref var healthUI = ref entity.GetComponent<HealthUIComponent>();
            Destroy(healthUI.View.gameObject);
        }
    }
}