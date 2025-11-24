using AYellowpaper.SerializedCollections;
using System.Collections;
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
    public Animator Anim;
    [SerializedDictionary("Position", "True or False")]
    public SerializedDictionary<string, AnimationClip> Animations = new SerializedDictionary<string, AnimationClip>
        {
            { "Aerial_Far", null},
            { "Aerial_MidRange", null},
            { "Aerial_Close", null},
            { "Neutral_Far", null},
            { "Neutral_MidRange", null},
            { "Neutral_Close", null},
            { "Ground_Far", null},
            { "Ground_MidRange", null},
            { "Ground_Close", null},
            { "Aerial_Far_Reset", null},
            { "Aerial_MidRange_Reset", null},
            { "Aerial_Close_Reset", null},
            { "Neutral_Far_Reset", null},
            { "Neutral_MidRange_Reset", null},
            { "Neutral_Close_Reset", null},
            { "Ground_Far_Reset", null},
            { "Ground_MidRange_Reset", null},
            { "Ground_Close_Reset", null},
        };
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
    public void Start()
    {
        Anim = GetComponent<Animator>();
    }
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
                Anim.Play(Animations["Aerial_Far"].name);
                break;
            case CombatEnums.Placement.Neutral_Far:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["Far"] = true;
                Anim.Play(Animations["Neutral_Far"].name);
                break;
            case CombatEnums.Placement.Ground_Far:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["Far"] = true;
                Anim.Play(Animations["Ground_Far"].name);
                break;
            case CombatEnums.Placement.Aerial_MidRange:
                AttackOrganizer["Aerial"] = true;
                AttackOrganizer["MidRange"] = true;
                Anim.Play(Animations["Aerial_MidRange"].name);
                break;
            case CombatEnums.Placement.Neutral_MidRange:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["MidRange"] = true;
                Anim.Play(Animations["Neutral_MidRange"].name);
                break;
            case CombatEnums.Placement.Ground_MidRange:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["MidRange"] = true;
                Anim.Play(Animations["Ground_MidRange"].name);
                break;
            case CombatEnums.Placement.Aerial_Close:
                AttackOrganizer["Aerial"] = true;
                AttackOrganizer["Close"] = true;
                Anim.Play(Animations["Aerial_Close"].name);
                break;
            case CombatEnums.Placement.Neutral_Close:
                AttackOrganizer["Neutral"] = true;
                AttackOrganizer["Close"] = true;
                Anim.Play(Animations["Neutral_Close"].name);
                break;
            case CombatEnums.Placement.Ground_Close:
                AttackOrganizer["Ground"] = true;
                AttackOrganizer["Close"] = true;
                Anim.Play(Animations["Ground_Close"].name);
                break;
        }
    }
    public void EnemyAttackCalc()
    {
        if (!EnemyAttacks.Any()) // if there are any attacks at all
        {
            CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack = NoAttacksAvalablePlaceHolderObject;
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
            CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack = NoAttacksAvalablePlaceHolderObject;
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
                Anim.Play(CurrentAttack.AttackAnimation.name, 0, 0.0f);
                CombatTurnMannager.GetComponent<Combatturnmannager>().CalcAttack();
                Debug.Log(CurrentAttack);
                Debug.Log(CombatTurnMannager.GetComponent<Combatturnmannager>().OpeningCounter);
            }
        }
    }
    public IEnumerator EnemyCombo()
    {
        EnemyMovePositionCalc(CombatEnums.EnemyMovePositionCalcType.Combo);
        Debug.Log("running combo");
        while (CombatTurnMannager.GetComponent<Combatturnmannager>().PlayerHP > 0 && CombatTurnMannager.GetComponent<Combatturnmannager>().OpeningCounter > 0)
        {
            Debug.Log("running combo2");
            EnemyPlacement = CurrentAttack.EnemyPlacementAfterHit;
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
                CombatAIReset();
                Debug.Log("No avalable Attacks");
                break;
            }
            bool AttackCheck = false;
            while (AttackCheck == false) // out of the avallable attacks pick one
            {
                CurrentAttack = EnemyAttacks[Random.Range(0, EnemyAttacks.Count)];
                if (AttackOrganizer[CurrentAttack.ThisMovePlacement.ToString()])
                {
                    AttackCheck = true;
                    CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack = CurrentAttack;
                    CombatTurnMannager.GetComponent<Combatturnmannager>().PlayerHP -= CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack.Damage;
                    CombatTurnMannager.GetComponent<Combatturnmannager>().OpeningCounter -= CombatTurnMannager.GetComponent<Combatturnmannager>().EnemyAttack.Start_Lag;
                    Anim.Play(CurrentAttack.AttackAnimation.name,0,0.0f);
                    yield return new WaitUntil(() => Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
                }
            }
            CombatTurnMannager.GetComponent<Combatturnmannager>().updateTMP();
        }
        CombatAIReset();
    }
    public void CombatAIReset()
    {
        ResetAttackOrganizer();
        switch (EnemyPlacement)
        {
            case CombatEnums.Placement.Aerial_Far:
                Anim.Play(Animations["Aerial_Far_Reset"].name);
                break;
            case CombatEnums.Placement.Neutral_Far:
                Anim.Play(Animations["Neutral_Far_Reset"].name);
                break;
            case CombatEnums.Placement.Ground_Far:
                Anim.Play(Animations["Ground_Far_Reset"].name);
                break;
            case CombatEnums.Placement.Aerial_MidRange:
                Anim.Play(Animations["Aerial_MidRange_Reset"].name);
                break;
            case CombatEnums.Placement.Neutral_MidRange:
                Anim.Play(Animations["Neutral_MidRange_Reset"].name);
                break;
            case CombatEnums.Placement.Ground_MidRange:
                Anim.Play(Animations["Ground_MidRange_Reset"].name);
                break;
            case CombatEnums.Placement.Aerial_Close:
                Anim.Play(Animations["Aerial_Close_Reset"].name);
                break;
            case CombatEnums.Placement.Neutral_Close:
                Anim.Play(Animations["Neutral_Close_Reset"].name);
                break;
            case CombatEnums.Placement.Ground_Close:
                Anim.Play(Animations["Ground_Close_Reset"].name);
                break;
        }
    }
}
