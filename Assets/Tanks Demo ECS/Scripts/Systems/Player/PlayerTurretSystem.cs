using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerTurretSystem))]
public sealed class PlayerTurretSystem : UpdateSystem
{
    public TurretDatabase TurretDatabase;

    private Filter filterNextTurret;
    private Filter filterPrevTurret;
    private Filter filterInit;

    public override void OnAwake() {
        filterInit           = World.Filter.With<PlayerComponent>().With<CreatedComponent>();
        filterNextTurret     = World.Filter.With<PlayerComponent>().With<PlayerNextTurretCommand>();
        filterPrevTurret     = World.Filter.With<PlayerComponent>().With<PlayerPrevTurretCommand>();
    }
    
    public override void OnUpdate(float deltaTime) {

        foreach (var entity in filterInit)
        {
            ref var playerWeapon = ref entity.GetComponent<PlayerWeaponComponent>();
            ref var prefabLinks = ref entity.GetComponent<PlayerPrefabLinksComponent>();
            playerWeapon.ind = 0;
            CreateTurret(ref playerWeapon, ref prefabLinks);
        }
        
        foreach (var entity in filterNextTurret)
        {
            entity.RemoveComponent<PlayerNextTurretCommand>();
            
            ref var playerWeapon = ref entity.GetComponent<PlayerWeaponComponent>();
            ref var prefabLinks = ref entity.GetComponent<PlayerPrefabLinksComponent>();
            playerWeapon.ind++;
            ChangeTurret(ref playerWeapon, ref prefabLinks);
        }

        foreach (var entity in filterPrevTurret)
        {
            entity.RemoveComponent<PlayerPrevTurretCommand>();
            
            ref var playerWeapon = ref entity.GetComponent<PlayerWeaponComponent>();
            ref var prefabLinks = ref entity.GetComponent<PlayerPrefabLinksComponent>();
            playerWeapon.ind--;
            ChangeTurret(ref playerWeapon, ref prefabLinks);
        }
    }

    private void ChangeTurret(ref PlayerWeaponComponent playerWeapon,ref PlayerPrefabLinksComponent prefabLinks)
    {
        playerWeapon.ind = Mathf.Clamp(playerWeapon.ind, 0, TurretDatabase.Length - 1);
        Destroy(playerWeapon.gameObject);
        var newTurret = TurretDatabase.GetTurret(playerWeapon.ind);
        playerWeapon.Attack = newTurret.Atack;
        playerWeapon.gameObject = Instantiate(newTurret.TurretPrefab, prefabLinks.TurretTransform);
    }
    
    private void CreateTurret(ref PlayerWeaponComponent playerWeapon,ref PlayerPrefabLinksComponent prefabLinks)
    {
        var newTurret = TurretDatabase.GetTurret(playerWeapon.ind);
        playerWeapon.Attack = newTurret.Atack;
        playerWeapon.gameObject = Instantiate(newTurret.TurretPrefab, prefabLinks.TurretTransform);
    }
}