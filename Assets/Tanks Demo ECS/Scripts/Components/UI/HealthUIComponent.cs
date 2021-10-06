using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
public struct HealthUIComponent : IComponent
{
    public Entity TargetEntity;
    public HealthBarUI View;
}