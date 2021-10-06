using Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
public struct PlayerPrefabLinksComponent : IComponent {
    public Transform FireTransform;
    public Transform TurretTransform;
}