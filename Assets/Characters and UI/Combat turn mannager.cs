using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AYellowpaper.SerializedCollections
{
    public class Combatturnmannager : MonoBehaviour
    {
        public Playerstats CurrentPlayerStats;
        public GameObject Enemy;
        public GameObject MoveGrid;
        public GameObject AttackGrid;
        public List<GameObject> AttackButtons;
        public List<Attacks> AttackData;
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
        public int OppeningCounter;
        public Attacks PlayerAttack;
        public Attacks EnemyAttack;
        public void Awake()
        {
            CurrentTurnStep = TurnSteps.MoveStep;
            OppeningCounter = 00;
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
            Enemy.GetComponent<EnemyAI>().EnemyMovePositionCalc();
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
                    break;
                case "Neutral Far":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["Far"] = true;
                    break;
                case "Ground Far":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["Far"] = true;
                    break;
                case "Aerial Mid-Range":
                    AttackOrganizer["Aerial"] = true;
                    AttackOrganizer["MidRange"] = true;
                    break;
                case "Neutral Mid-Range":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["MidRange"] = true;
                    break;
                case "Ground Mid-Range":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["MidRange"] = true;
                    break;
                case "Aerial Close":
                    AttackOrganizer["Aerial"] = true;
                    AttackOrganizer["Close"] = true;
                    break;
                case "Neutral Close":
                    AttackOrganizer["Neutral"] = true;
                    AttackOrganizer["Close"] = true;
                    break;
                case "Ground Close":
                    AttackOrganizer["Ground"] = true;
                    AttackOrganizer["Close"] = true;
                    break;
            }
            AttackGrid.GetComponent<CanvasGroup>().interactable = true;
            SetAttackButtonInfo();
            SetEnemyAIMovePosition();
        }
        public void SetPlayerAttack(int ButtonInList)
        {
            PlayerAttack = AttackData[ButtonInList];
            Enemy.GetComponent<EnemyAI>().EnemyAttackCalc();
        }
        public void CalcAttack()
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
            } 
            else if (PlayerWeak)
            {
                Debug.Log("Player weak");
            } 
            else if (EnemyWeak)
            {
                Debug.Log("Enemy weak");
            }
            else
            {
                Debug.Log("neither weak");
            }
        }
    }
}
