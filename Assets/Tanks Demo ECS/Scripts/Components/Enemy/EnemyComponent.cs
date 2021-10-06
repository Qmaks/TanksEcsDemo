using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine.AI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[System.Serializable]
public struct EnemyComponent : IComponent
{
    public Transform Transform;
    public NavMeshAgent NavMeshAgent;
}