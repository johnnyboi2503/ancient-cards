using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Scriptable Objects/EnemyList")]
public class EnemyList : ScriptableObject
{
    public List<GameObject> EnemyPrefabList;
    public int EnemyIndex;
}
