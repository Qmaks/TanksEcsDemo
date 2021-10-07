using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[CreateAssetMenu()]
public class TurretDatabase : ScriptableObject
{
    [SerializeField]
    private TurretDefinitions[] Turrets;
    
    [Serializable]
    public class TurretDefinitions
    {
        public GameObject TurretPrefab;
        public int Attack;
    }

    public int Length => Turrets.Length;

    public TurretDefinitions GetTurret(int index)
    {
        return Turrets[index];
    }
}
