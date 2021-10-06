using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyMoveSystem))]
public sealed class EnemyMoveSystem : FixedUpdateSystem
{
    private Filter filter;
    private Filter filterPlayer;
    public override void OnAwake()
    {
        filter       = World.Filter.With<EnemyComponent>().With<EnemyMoveComponent>();
        filterPlayer = World.Filter.With<PlayerComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        if (filterPlayer.Length==0) return;
        var player = filterPlayer.First().GetComponent<TransformRef>().Transform;
        
        foreach (var entity in filter) {
            ref var enemy                   = ref entity.GetComponent<EnemyComponent>();
            ref var enemyMove = ref entity.GetComponent<EnemyMoveComponent>();
            enemy.NavMeshAgent.speed = enemyMove.Speed;
            enemy.NavMeshAgent.SetDestination(player.position);
        }
    }
}