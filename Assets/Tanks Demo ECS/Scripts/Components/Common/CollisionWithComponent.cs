using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
public struct CollisionWithComponent : IComponent
{
    public Entity Other;
}