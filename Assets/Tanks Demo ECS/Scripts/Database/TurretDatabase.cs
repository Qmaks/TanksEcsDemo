using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu()]
public class TurretDatabase : ScriptableObject
{
    public TurretDefinitions[] Turrets;
    
    [Serializable]
    public class TurretDefinitions
    {
        public GameObject TurretPrefab;
        public int Atack;
    }

    public int Length => Turrets.Length;

    public TurretDefinitions GetTurret(int index)
    {
        return Turrets[index];
    }
}
