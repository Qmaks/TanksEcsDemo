using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCreateSystem))]
public sealed class BulletCreateSystem : UpdateSystem
{
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private float BulletSpeed = 50f;
    
    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<PlayerInputComponent>().With<PlayerShootComand>();
    }

    public override void OnUpdate(float deltaTime)
    {
        foreach (var entityPlayer in filter)
        {
            entityPlayer.RemoveComponent<PlayerShootComand>();
            
            var tankRigidbody = entityPlayer.GetComponent<RigidBodyRef>().Rigidbody;
            var tankTransform = entityPlayer.GetComponent<TransformRef>().Transform;
            var fireTransform = entityPlayer.GetComponent<PlayerPrefabLinksComponent>().FireTransform;
            
            var shell = GameObject.Instantiate(BulletPrefab, fireTransform.position, tankTransform.rotation ).GetComponent<Rigidbody>();
            shell.velocity = tankTransform.forward * BulletSpeed;
            
 
            var entity = World.CreateEntity();
            ref var transformRef  = ref entity.AddComponent<TransformRef>();
            transformRef.Transform = shell.transform;
            
            entity.AddComponent<BulletComponent>();
            
            ref var attack  = ref entity.AddComponent<AttackComponent>();
            attack.Attack   = entityPlayer.GetComponent<PlayerWeaponComponent>().Attack;
            
            shell.GetComponent<ShellMono>().Entity = entity;
        }
    }
}