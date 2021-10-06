using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[CreateAssetMenu()]
public class EnemyDatabase : ScriptableObject
{
    [SerializeField]
    private EnemyDefinitions[] Enemies;
    
    [Serializable]
    public class EnemyDefinitions
    {
        public GameObject Prefab;
        public int Speed;
        public int Atack;
        [Range(0f,1f)]
        public float Defence;
        public int Health;
    }

    public int Length => Enemies.Length;

    public GameObject GetEnemyPrefab(int index)
    {
        return Enemies[index].Prefab;
    }

    public EnemyDefinitions GetRandomEnemyDefenition()
    {
        return Enemies[Random.Range(0, Enemies.Length)];
    }
}
