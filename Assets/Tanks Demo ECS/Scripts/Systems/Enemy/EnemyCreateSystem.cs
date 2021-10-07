using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EnemyCreateSystem))]
public sealed class EnemyCreateSystem : UpdateSystem
{
    [Header("Static data")]
    [SerializeField]
    private EnemyDatabase EnemyDatabase;
    [Header("Parametrs")]
    [SerializeField]
    private int MaxEnemyCount;
    
    [Header("Global In Events")]
    [SerializeField]
    private GlobalEventInt EventDestroyEnemy;

    [Header("Global Out Events")]
    [SerializeField]
    private GlobalEventInt EventSpawnEnemy;
    
    private Transform SpawnRoot;
    private int enemyCount = 0;

    public override void OnAwake()
    {
        SpawnRoot = FindObjectOfType<SceneReference>().EnemySpawns;
    }

    public override void OnUpdate(float deltaTime) {

        if (EventDestroyEnemy.IsPublished)
        {
            enemyCount--;
        }
        
        if (enemyCount < MaxEnemyCount)
        {
            enemyCount++;
            var rnd = Random.Range(0,SpawnRoot.childCount-1);
            var enemy = EnemyDatabase.GetRandomEnemyDefenition();
            
            var enemyTransform =  Instantiate(enemy.Prefab, SpawnRoot.GetChild(rnd).transform).transform ;
            
            var entity = World.CreateEntity();
            ref var enemyComponent  = ref entity.AddComponent<EnemyComponent>();
            enemyComponent.NavMeshAgent = enemyTransform.GetComponent<NavMeshAgent>();
            
            ref var transformRef  = ref entity.AddComponent<TransformRef>();
            transformRef.Transform = enemyTransform;
            
            ref var enemyMoveComponent  = ref entity.AddComponent<EnemyMoveComponent>();
            enemyMoveComponent.Speed = enemy.Speed;
            
            ref var healthComponent       = ref entity.AddComponent<HealthComponent>();
            healthComponent.HealthCurrent = enemy.Health;
            healthComponent.HealthMax     = enemy.Health;
            
            ref var defenceComponent       = ref entity.AddComponent<DefenceComponent>();
            defenceComponent.Defence       = enemy.Defence;
            
            ref var attackComponent       = ref entity.AddComponent<AttackComponent>();
            attackComponent.Attack        = enemy.Attack;

            entity.AddComponent<HealthUIComponent>();
            entity.AddComponent<CreatedComponent>();
            
            enemyTransform.gameObject.AddComponent<EntityRef>().Entity = entity;
            enemyTransform.gameObject.AddComponent<EnemyMono>();
            
            EventSpawnEnemy?.NextFrame(entity.ID);
        }
    }
    
    public override void Dispose()
    {
        enemyCount = 0;
    }
}