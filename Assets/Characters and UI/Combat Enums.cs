using UnityEngine;

public class CombatEnums
{
    public enum MoveEnum
    {
        Aerial,
        Neutral,
        Ground,
        Far,
        MidRange,
        Close
    }
    public enum Placement
    {
        Aerial_Far,
        Aerial_MidRange,
        Aerial_Close,
        Neutral_Far,
        Neutral_MidRange,
        Neutral_Close,
        Ground_Far,
        Ground_MidRange,
        Ground_Close
    }
    public enum AttackType
    {
        None,
        Neutral,
        Attack,
        Grab,
        Block
    }
    public enum EnemyMovePositionCalcType
    {
        Random,
        Combo
    }
}
