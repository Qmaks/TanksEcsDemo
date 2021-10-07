using Morpeh;
using Morpeh.Globals;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputSystem))]
public sealed class PlayerInputSystem : UpdateSystem {

    [Header("Input")]
    [SerializeField]
    private string FireButton;
    [SerializeField]
    private string Horizontal;
    [SerializeField]
    private string Vertical;
    [SerializeField]
    private string NextTurretButton;
    [SerializeField]
    private string PreviousTurretButton;
    
    private Filter filter;
    
    public override void OnAwake() {
        filter = World.Filter.With<PlayerInputComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        
        if (Input.GetButtonDown(FireButton))
        {
            foreach (var entity in filter)
            {
                entity.AddComponent<PlayerShootComand>();
            }
        }
        
        if (Input.GetButtonDown(NextTurretButton))
        {
            foreach (var entity in filter)
            {
                entity.AddComponent<PlayerNextTurretCommand>();
            }
        }
        
        if (Input.GetButtonDown(PreviousTurretButton))
        {
            foreach (var entity in filter)
            {
                entity.AddComponent<PlayerPrevTurretCommand>();
            }
        }
        
        
        foreach (var entity in filter) {
            ref var input = ref entity.GetComponent<PlayerInputComponent>();
            input.moveInput = new Vector3(Input.GetAxis(Horizontal), 0f, Input.GetAxis(Vertical)); 
        }
    }
}