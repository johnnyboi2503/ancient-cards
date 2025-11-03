using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject CombatTurnMannager;
    public int EnemyStartHP;
    public CombatEnums.Placement EnemyPlacement;
    public List<Attacks> EnemyAttacks;
    public Attacks CurrentAttack;
    public Attacks NoAttacksAvalablePlaceHolderObject;
    [SerializedDictionary("Position", "True or False")]
    public SerializedDictionary<string, bool> AttackOrganizer = new SerializedDictionary<string, bool>
        {
            { "Aerial", false },
            { "Neutral", false },
            { "Ground", false },
            { "Far", false },
            { "MidRange", false },
            { "Close", false }
        };
    public void ResetAttackOrganizer()
    {
        List<string> keys = new List<string>(AttackOrganizer.Keys);
        foreach (string key in keys)
        {
            AttackOrganizer[key] = false;
        }
    }
    public T GetRandomEnumValue<T>()//T is the return vareable type <T> is simmilar to putting a variable in the perethases
    {
        System.Array values = System.Enum.GetValues(typeof(T));//this creates a list of of the enum values
        return (T)values.GetValue(Random.Range(0, values.Length));//this gets it from the Array type list
    }
    public void Awake()
    {
        EnemyPlacement = CombatEnums.Placement.Neutral_MidRange;
    }
    public void EnemyMovePositionCalc(CombatEnums.EnemyMovePositionCalcType Type)
    {
        switch (Type)
        {
            case CombatEnums.EnemyMovePositionCalcType.Random:
                EnemyPlacement = GetRandomEnumValue<CombatEnums.Placement>();
                break;
            case CombatEnums.EnemyMovePositionCalcType.Combo:
                EnemyPlacement = CurrentAttack.EnemyPlacementAfterHit;
                break;
        }
        switch (EnemyPlacement)
        {
            case CombatEnums.Placement.Aerial_Far:
                AttackOrganizer["Aerial"] = true;
                AttackOrganizer["Far"] = true;
                break;
            case CombatEnums.Placement.Neutral_Far:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["Far"] = true;
                break;
            case CombatEnums.Placement.Ground_Far:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["Far"] = true;
                break;
            case CombatEnums.Placement.Aerial_MidRange:
                AttackOrganizer["Aerial"] = true;
                AttackOrganizer["MidRange"] = true;
                break;
            case CombatEnums.Placement.Neutral_MidRange:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["MidRange"] = true;
                break;
            case CombatEnums.Placement.Ground_MidRange:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["MidRange"] = true;
                break;
            case CombatEnums.Placement.Aerial_Close:
                AttackOrganizer["Aerial"] = true;
                AttackOrganizer["Close"] = true;
                break;
            case CombatEnums.Placement.Neutral_Close:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["Close"] = true;
                break;
            case CombatEnums.Placement.Ground_Close:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["Close"] = true;
                break;
        }
    }
    public void EnemyAttackCalc()
    {
        if (!EnemyAttacks.Any()) // if there are any attacks at all
        {
            CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
            Debug.Log("No Enemy Attacks");
            return;
        }
        bool CanAttack = false;
        for (int i = 0; i < EnemyAttacks.Count; i++) // if there are any avalable attacks in the current pos
        {
            if (AttackOrganizer[EnemyAttacks[i].ThisMovePlacement.ToString()])
            {
                CanAttack = true;
                break;
            }
        }
        if (CanAttack == false)
        {
            CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
            Debug.Log("No avalable Attacks");
            return;
        }
        bool AttackCheck = false;
        while (AttackCheck == false) // out of the avallable attacks pick one
        {
            CurrentAttack = EnemyAttacks[Random.Range(0, EnemyAttacks.Count)];
            if (AttackOrganizer[CurrentAttack.ThisMovePlacement.ToString()])
            {
                AttackCheck = true;
                CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack = CurrentAttack;
                CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
            }
        }
        Debug.Log(CurrentAttack);
    }
    public void EnemyCombo()
    {
        EnemyMovePositionCalc(CombatEnums.EnemyMovePositionCalcType.Combo);
        while (CombatTurnMannager.GetComponent<Combatturnmannager>().PlayerHP > 0 && CombatTurnMannager.GetComponent<Combatturnmannager>().OppeningCounter > 0)
        {
            if (!EnemyAttacks.Any()) // if there are any attacks at all
            {
                CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
                Debug.Log("No Enemy Attacks");
                return;
            }
            bool CanAttack = false;
            for (int i = 0; i < EnemyAttacks.Count; i++) // if there are any avalable attacks in the current pos
            {
                if (AttackOrganizer[EnemyAttacks[i].ThisMovePlacement.ToString()])
                {
                    CanAttack = true;
                    break;
                }
            }
            if (CanAttack == false)
            {
                CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
                Debug.Log("No avalable Attacks");
                return;
            }
            bool AttackCheck = false;
            while (AttackCheck == false) // out of the avallable attacks pick one
            {
                CurrentAttack = EnemyAttacks[Random.Range(0, EnemyAttacks.Count)];
                if (AttackOrganizer[CurrentAttack.ThisMovePlacement.ToString()])
                {
                    AttackCheck = true;
                    CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack = CurrentAttack;
                }
            }
            CombatTurnMannager.GetComponent<Combatturnmannager>().updateTMP();
        }
    }
    public void CombatAIReset()
    {
        ResetAttackOrganizer();
    }
}
