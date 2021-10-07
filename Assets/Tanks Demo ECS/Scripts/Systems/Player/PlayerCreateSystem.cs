using Cinemachine;
using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerCreateSystem))]
public sealed class PlayerCreateSystem : UpdateSystem
{
    [SerializeField]
    private GameObject PlayerPrefab;
    
    [Header("Параметры игрока")]
    [SerializeField]
    private float Health;
    [SerializeField]
    private float Defence;

    private Transform  playerSpawn;
    private CinemachineVirtualCamera camera;

    private Filter filter;
    public override void OnAwake()
    {
        filter = World.Filter.With<CreatePlayerCommand>();
        playerSpawn = FindObjectOfType<SceneReference>().PlayerSpawn;
        camera      = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter)
        {
             World.Default.RemoveEntity(entity);
             
             CreatePlayerEntity();           
        }
    }
    
    public void CreatePlayerEntity()
    {
        var entity = World.CreateEntity();
        entity.AddComponent<PlayerComponent>();

        playerSpawn = FindObjectOfType<SceneReference>().PlayerSpawn;
        var player = GameObject.Instantiate(PlayerPrefab, playerSpawn.position, Quaternion.identity);
        
        camera.Follow = player.transform;

        player.AddComponent<EntityRef>().Entity = entity;
        
        entity.AddComponent<PlayerInputComponent>();
        
        var playerPrefabLinks = player.GetComponent<PlayerPrefabLinks>();
        ref var prefabData = ref entity.AddComponent<PlayerPrefabLinksComponent>();
        prefabData.FireTransform   = playerPrefabLinks.FireTransform;
        prefabData.TurretTransform = playerPrefabLinks.TurretTransform;
        
        ref var transformRef = ref entity.AddComponent<TransformRef>();
        transformRef.Transform = player.transform;

        ref var rigidBodyRef = ref entity.AddComponent<RigidBodyRef>();
        rigidBodyRef.Rigidbody = player.GetComponent<Rigidbody>();
        
        entity.AddComponent<PlayerWeaponComponent>();

        ref var health = ref entity.AddComponent<HealthComponent>();
        health.HealthCurrent = Health;
        health.HealthMax     = Health;
        
        ref var defence = ref entity.AddComponent<DefenceComponent>();
        defence.Defence = Defence;
        
        entity.AddComponent<HealthUIComponent>();
        entity.AddComponent<CreatedComponent>();
    }
}