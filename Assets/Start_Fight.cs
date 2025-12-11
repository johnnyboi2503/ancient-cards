using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Fight : MonoBehaviour, Interactable
{
    public int EnemyIndex;
    public Attacks AttackWon;
    public void Interacted()
    {
        SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
    }
    
}
