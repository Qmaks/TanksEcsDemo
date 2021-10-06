using Cinemachine;
using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerInitSystem))]
public sealed class PlayerInitSystem : Initializer
{
    public override void OnAwake() {
        var entity = World.Default.CreateEntity();
        entity.AddComponent<CreatePlayerCommand>();
    }

    
    public override void Dispose() {
    }
}