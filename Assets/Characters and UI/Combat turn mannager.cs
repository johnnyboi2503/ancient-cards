using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace AYellowpaper.SerializedCollections
{
    public class Combatturnmannager : MonoBehaviour
    {
        public Playerstats CurrentPlayerStats;
        public CombatEnums.Placement PlayerPlacement;
        public TextMeshProUGUI OpeningCounterTMP;
        public int OpeningCounter = 0;
        public TextMeshProUGUI PlayerHPTMP;
        public int PlayerHP;
        public TextMeshProUGUI EnemyHPTMP;
        public int EnemyHP;
        public GameObject Enemy;
        public GameObject MoveGrid;
        public GameObject AttackGrid;
        public List<GameObject> AttackButtons;
        public List<Attacks> AttackData;
        public Animator PlayerAnim;
        [SerializedDictionary("Move", "True or False")]
        public SerializedDictionary<string, bool> AttackOrganizer = new SerializedDictionary<string, bool>
        {
            { "Aerial", false },
            { "Neutral", false },
            { "Ground", false },
            { "Far", false },
            { "MidRange", false },
            { "Close", false }
        };
        public enum TurnSteps
        {
            MoveStep,
            AttackStep,
            CalcStep,
            PlayerCLash,
            EnemyClash,
            PlayerWhiff,
            EnemyWhiff
        }
        public TurnSteps CurrentTurnStep;
        public Attacks PlayerAttack;
        public Attacks EnemyAttack;
        private bool InCombo = false;
        private bool AttackComboPressed = false;
        public void Awake()
        {
            CurrentTurnStep = TurnSteps.MoveStep;
            OpeningCounter = 00;
            PlayerHP = CurrentPlayerStats.HP;
            EnemyHP = Enemy.GetComponent<EnemyAI>().EnemyStartHP;
            PlayerHPTMP.text = "Player\n"+ "HP: " + PlayerHP.ToString();
            EnemyHPTMP.text = "Enemy\n"+"HP: " + EnemyHP.ToString();
            OpeningCounterTMP.text = OpeningCounter.ToString();
        }
        public void updateTMP()
        {
            PlayerHPTMP.text = "Player\n" + "HP: " + PlayerHP.ToString();
            EnemyHPTMP.text = "Enemy\n"+"HP: " + EnemyHP.ToString();
            OpeningCounterTMP.text = OpeningCounter.ToString();
            CurrentPlayerStats.HP = PlayerHP;
        }
        public void ResetAttackOrganizer()
        {
            List<string> keys = new List<string>(AttackOrganizer.Keys);
            foreach (string key in keys)
            {
                AttackOrganizer[key] = false;
            }
        }
        public void SetAttackButtonInfo()
        {
            int CurrentSlot = 0;
            if (AttackOrganizer["Aerial"])
            {
                for (int i = 0; i < CurrentPlayerStats.AerialAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.AerialAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.AerialAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.AerialAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.AerialAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.AerialAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.AerialAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.AerialAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.AerialAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            if (AttackOrganizer["Neutral"])
            {
                for (int i = 0; i < CurrentPlayerStats.NeutralAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.NeutralAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.NeutralAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.NeutralAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.NeutralAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.NeutralAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.NeutralAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.NeutralAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.NeutralAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            if (AttackOrganizer["Ground"])
            {
                for (int i = 0; i < CurrentPlayerStats.GroundAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.GroundAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.GroundAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.GroundAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.GroundAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.GroundAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.GroundAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.GroundAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.GroundAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            if (AttackOrganizer["Far"])
            {
                for (int i = 0; i < CurrentPlayerStats.FarAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.FarAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.FarAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.FarAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.FarAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.FarAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.FarAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.FarAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.FarAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            if (AttackOrganizer["MidRange"])
            {
                for (int i = 0; i < CurrentPlayerStats.MidRangeAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.MidRangeAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.MidRangeAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.MidRangeAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.MidRangeAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.MidRangeAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.MidRangeAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.MidRangeAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.MidRangeAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            if (AttackOrganizer["Close"])
            {
                for (int i = 0; i < CurrentPlayerStats.CloseAttacks.Count; i++)
                {
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Name (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.CloseAttacks[i].AttackName + "\nDamage: " + CurrentPlayerStats.CloseAttacks[i].Damage;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Type (TMP)").GetComponent<TextMeshProUGUI>().text = CurrentPlayerStats.CloseAttacks[i].ThisAttackType.ToString();
                    string WeaknessText = null;
                    for (int I = 0; I < CurrentPlayerStats.CloseAttacks[i].MoveWeakness.Count; I++)
                    {
                        WeaknessText += CurrentPlayerStats.CloseAttacks[i].MoveWeakness[I].ToString() + ",\n";
                    }
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Move Weak (TMP)").GetComponent<TextMeshProUGUI>().text = WeaknessText;
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("Start Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "Start Lag: " + CurrentPlayerStats.CloseAttacks[i].Start_Lag.ToString();
                    AttackButtons[CurrentSlot].transform.Find("Attack Button").transform.Find("End Lag (TMP)").GetComponent<TextMeshProUGUI>().text = "End Lag: " + CurrentPlayerStats.CloseAttacks[i].End_Lag.ToString();
                    AttackData.Add(CurrentPlayerStats.CloseAttacks[i]);
                    CurrentSlot += 1;
                }
            }
            for (int i = 0; i < 6-CurrentSlot; i++)
            {
                AttackButtons[5 - i].transform.Find("Attack Button").GetComponent<Button>().interactable = false;
            }
        }
        public void SetEnemyAIMovePosition()
        {
            Enemy.GetComponent<EnemyAI>().EnemyMovePositionCalc(CombatEnums.EnemyMovePositionCalcType.Random);
        }
        public void SetPlayerMovePosition(GameObject Button)
        {
            MoveGrid.GetComponent<CanvasGroup>().interactable = false;
            CurrentTurnStep = TurnSteps.AttackStep;
            ResetAttackOrganizer();
            switch (Button.name)
            {
                case "Aerial Far":
                    AttackOrganizer["Aerial"] = true;
                    AttackOrganizer["Far"] = true;
                    PlayerPlacement = CombatEnums.Placement.Aerial_Far;
                    break;
                case "Neutral Far":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["Far"] = true;
                    PlayerPlacement = CombatEnums.Placement.Neutral_Far;
                    break;
                case "Ground Far":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["Far"] = true;
                    PlayerPlacement = CombatEnums.Placement.Ground_Far;
                    break;
                case "Aerial Mid-Range":
                    AttackOrganizer["Aerial"] = true;
                    AttackOrganizer["MidRange"] = true;
                    PlayerPlacement = CombatEnums.Placement.Aerial_MidRange;
                    break;
                case "Neutral Mid-Range":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["MidRange"] = true;
                    PlayerPlacement = CombatEnums.Placement.Neutral_MidRange;
                    break;
                case "Ground Mid-Range":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["MidRange"] = true;
                    PlayerPlacement = CombatEnums.Placement.Ground_MidRange;
                    break;
                case "Aerial Close":
                    AttackOrganizer["Aerial"] = true;
                    AttackOrganizer["Close"] = true;
                    PlayerPlacement = CombatEnums.Placement.Aerial_Close;
                    break;
                case "Neutral Close":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["Close"] = true;
                    PlayerPlacement = CombatEnums.Placement.Neutral_Close;
                    break;
                case "Ground Close":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["Close"] = true;
                    PlayerPlacement = CombatEnums.Placement.Ground_Close;
                    break;
            }
            AttackGrid.GetComponent<CanvasGroup>().interactable = true;
            SetAttackButtonInfo();
            SetEnemyAIMovePosition();
        }
        public void SetPlayerAttack(int ButtonInList)
        {
            PlayerAttack = AttackData[ButtonInList];
            if (InCombo == false)
            {
                Enemy.GetComponent<EnemyAI>().EnemyAttackCalc();
            }
        }
        public IEnumerator CalcAttack()
        {
            AttackGrid.GetComponent<CanvasGroup>().interactable = false;
            bool PlayerWeak = false;
            bool EnemyWeak = false;
            for (int i = 0; i < EnemyAttack.MoveWeakness.Count; i++)
            {
                if (EnemyAttack.MoveWeakness[i] == PlayerAttack.ThisAttackType)
                {
                    EnemyWeak = true;
                }
            }
            for (int i = 0; i < PlayerAttack.MoveWeakness.Count; i++)
            {
                if (PlayerAttack.MoveWeakness[i] == EnemyAttack.ThisAttackType)
                {
                    PlayerWeak = true;
                }
            }
            if (PlayerWeak && EnemyWeak)
            {
                Debug.Log("Enemy and Player weak");
                if (PlayerAttack.End_Lag < EnemyAttack.End_Lag)
                {
                    Debug.Log("Enemy Wiff player can attack");
                    Restart();
                } 
                else if (EnemyAttack.End_Lag < PlayerAttack.End_Lag)
                {
                    Debug.Log("Player Wiff Enemy can attack");
                    Restart();
                } 
                else
                {
                    Debug.Log("both Wiff reset combat");
                    Restart();
                }
            } 
            else if (PlayerWeak)
            {
                Debug.Log("Player weak To " + EnemyAttack);
                Enemy.GetComponent<EnemyAI>().EnemyMovePositionCalc(CombatEnums.EnemyMovePositionCalcType.Combo);
                PlayerHP = PlayerHP - EnemyAttack.Damage;
                OpeningCounter = EnemyAttack.Start_Lag;
                updateTMP();
                Enemy.GetComponent<EnemyAI>().Anim.Play(Enemy.GetComponent<EnemyAI>().CurrentAttack.AttackAnimation.name, 0, 0.0f);
                Enemy.GetComponent<EnemyAI>().EnemyPlacement = Enemy.GetComponent<EnemyAI>().CurrentAttack.EnemyPlacementAfterHit;
                yield return new WaitUntil(() => Enemy.GetComponent<EnemyAI>().Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
                StartCoroutine(Enemy.GetComponent<EnemyAI>().EnemyCombo());
                Restart();
            } 
            else if (EnemyWeak)
            {
                Debug.Log("Enemy weak");
                InCombo = true;
                PlayerAnim.Play(PlayerAttack.AttackAnimation.name, 0, 0.0f);
                EnemyHP = EnemyHP - PlayerAttack.Damage;
                OpeningCounter = PlayerAttack.Start_Lag;
                updateTMP();
                yield return new WaitUntil(() => PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
                StartCoroutine(PlayerCombo());
            }
            else
            {
                Debug.Log("neither weak");
                Enemy.GetComponent<EnemyAI>().CombatAIReset();
                Restart();
            }
        }
        public IEnumerator PlayerCombo()
        {
            ResetAttackOrganizer();
            PlayerPlacement = PlayerAttack.PlayerPlacementAfterHit;
            AttackComboPressed = false;
            AttackGrid.GetComponent<CanvasGroup>().interactable = true;
            yield return new WaitUntil(() => AttackComboPressed == true);
            while (EnemyHP > 0 && OpeningCounter > 0) 
            {
                AttackComboPressed = false;
                switch (PlayerPlacement)
                {
                    case CombatEnums.Placement.Aerial_Far:
                        AttackOrganizer["Aerial"] = true;
                        AttackOrganizer["Far"] = true;
                        //Anim.Play(Animations["Aerial_Far"].name);
                        break;
                    case CombatEnums.Placement.Neutral_Far:
                        AttackOrganizer["Neutral"] = true;
                        AttackOrganizer["Far"] = true;
                        //Anim.Play(Animations["Neutral_Far"].name);
                        break;
                    case CombatEnums.Placement.Ground_Far:
                        AttackOrganizer["Ground"] = true;
                        AttackOrganizer["Far"] = true;
                        //Anim.Play(Animations["Ground_Far"].name);
                        break;
                    case CombatEnums.Placement.Aerial_MidRange:
                        AttackOrganizer["Aerial"] = true;
                        AttackOrganizer["MidRange"] = true;
                        //Anim.Play(Animations["Aerial_MidRange"].name);
                        break;
                    case CombatEnums.Placement.Neutral_MidRange:
                        AttackOrganizer["Neutral"] = true;
                        AttackOrganizer["MidRange"] = true;
                        //Anim.Play(Animations["Neutral_MidRange"].name);
                        break;
                    case CombatEnums.Placement.Ground_MidRange:
                        AttackOrganizer["Ground"] = true;
                        AttackOrganizer["MidRange"] = true;
                        //Anim.Play(Animations["Ground_MidRange"].name);
                        break;
                    case CombatEnums.Placement.Aerial_Close:
                        AttackOrganizer["Aerial"] = true;
                        AttackOrganizer["Close"] = true;
                        //Anim.Play(Animations["Aerial_Close"].name);
                        break;
                    case CombatEnums.Placement.Neutral_Close:
                        AttackOrganizer["Neutral"] = true;
                        AttackOrganizer["Close"] = true;
                        //Anim.Play(Animations["Neutral_Close"].name);
                        break;
                    case CombatEnums.Placement.Ground_Close:
                        AttackOrganizer["Ground"] = true;
                        AttackOrganizer["Close"] = true;
                        //Anim.Play(Animations["Ground_Close"].name);
                        break;
                };
                SetAttackButtonInfo();
                PlayerAnim.Play(PlayerAttack.AttackAnimation.name, 0, 0.0f);
                EnemyHP = EnemyHP - PlayerAttack.Damage;
                OpeningCounter = OpeningCounter - PlayerAttack.Start_Lag;
                updateTMP();
                yield return new WaitUntil(() => PlayerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            }
            if (EnemyHP <= 0)
            {
                InCombo = false;
                Debug.Log("Fight over killed enemy");
            }
            else 
            {
                Debug.Log("Combo over");
                Restart();
                updateTMP();
                InCombo = false;
                AttackComboPressed = false;
                AttackGrid.GetComponent<CanvasGroup>().interactable = false;
                Enemy.GetComponent<EnemyAI>().CombatAIReset();
            }
        }
        public void AttackButtonPressedInCombo()
        {
            AttackComboPressed = true;
        }
        public void Restart()
        {
            OpeningCounter = 0;
            CurrentTurnStep = TurnSteps.MoveStep;
            MoveGrid.GetComponent<CanvasGroup>().interactable = true;
            ResetAttackOrganizer();
            updateTMP();
        }
    }
}
