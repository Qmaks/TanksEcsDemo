using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMoveSystem))]
public sealed class PlayerMoveSystem : FixedUpdateSystem
{
    public float PlayerMoveSpeed;
    public float PlayerRotateSpeed;

    
    private Filter filter;
    
    public override void OnAwake()
    {
        filter = World.Filter.With<PlayerComponent>().With<PlayerInputComponent>();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in filter) {
            ref var input = ref entity.GetComponent<PlayerInputComponent>();
            ref var transformRef = ref entity.GetComponent<TransformRef>();
            ref var rigidbodyRef = ref entity.GetComponent<RigidBodyRef>();

            Move(transformRef.Transform,rigidbodyRef.Rigidbody,input.moveInput.z,deltaTime);
            Turn(rigidbodyRef.Rigidbody,input.moveInput.x,deltaTime);
        }
    }
    
    private void Move (Transform transform,Rigidbody rigidbody, float input, float deltaTime)
    {
        var movement = transform.forward * input * PlayerMoveSpeed * deltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
    }


    private void Turn (Rigidbody rigidbody, float input,float deltaTime)
    {
        var turn = input * PlayerRotateSpeed * deltaTime;
        var turnRotation = Quaternion.Euler (0f, turn, 0f);
        rigidbody.MoveRotation (rigidbody.rotation * turnRotation);
    }
}