using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject CombatTurnMannager;
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
    public void EnemyMovePositionCalc()
    {
        EnemyPlacement = GetRandomEnumValue<CombatEnums.Placement>();
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
        if (!EnemyAttacks.Any())
        {
            CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
            Debug.Log("No Enemy Attacks");
            return;
        }
        bool CanAttack = false;
        for (int i = 0; i < EnemyAttacks.Count; i++)
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
        while (AttackCheck == false)
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
    public void CombatAIReset()
    {
        ResetAttackOrganizer();
    }
}
