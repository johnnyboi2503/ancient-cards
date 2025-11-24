using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "Scriptable Objects/Attacks")]
public class Attacks : ScriptableObject
{
    public string AttackName;
    public CombatEnums.MoveEnum ThisMovePlacement;
    public CombatEnums.AttackType ThisAttackType;
    public List<CombatEnums.AttackType> MoveWeakness;
    public int Start_Lag;
    public int End_Lag;
    public int Damage;
    public CombatEnums.Placement EnemyPlacementAfterHit;
    public CombatEnums.Placement PlayerPlacementAfterHit;
    public AnimationClip AttackAnimation;
}
