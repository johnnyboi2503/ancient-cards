using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Fight : MonoBehaviour, Interactable
{
    public int EnemyIndex;
    public EnemyList EL;
    public Attacks AttackWon;
    public Playerstats PlayerStats;
    public CombatEnums.MoveEnum WhereToAddInPlayerStats;
    public GameObject CombatMan;
    public Camera Camera;
    private bool canFight = true;
    public void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
    }
    public void Interacted()
    {
        if (canFight == true)
        {
            EL.EnemyIndex = EnemyIndex;
            SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
            Camera.enabled = false;
            canFight = false;
        }
    }
    public void Update()
    {
        if (SceneManager.GetSceneByName("CombatScene").isLoaded) 
        {
            do 
            {
                CombatMan = SceneTools.FindInScene("CombatScene", "Combat manager");
            } 
            while (CombatMan == null);
            if (CombatMan.GetComponent<Combatturnmannager>().FightOver)
            {
                switch (WhereToAddInPlayerStats)
                {
                    case CombatEnums.MoveEnum.Aerial:
                        PlayerStats.AerialAttacks.Add(AttackWon);
                        break;
                    case CombatEnums.MoveEnum.Neutral:
                        PlayerStats.NeutralAttacks.Add(AttackWon);
                        break;
                    case CombatEnums.MoveEnum.Ground:
                        PlayerStats.GroundAttacks.Add(AttackWon);
                        break;
                    case CombatEnums.MoveEnum.Far:
                        PlayerStats.FarAttacks.Add(AttackWon);
                        break;
                    case CombatEnums.MoveEnum.MidRange:
                        PlayerStats.MidRangeAttacks.Add(AttackWon);
                        break;
                    case CombatEnums.MoveEnum.Close:
                        PlayerStats.CloseAttacks.Add(AttackWon);
                        break;
                }
                Camera.enabled = true;
                SceneManager.UnloadSceneAsync("CombatScene");
                Destroy(gameObject);
            }
        }
    }
}
