using UnityEngine;

public class ladderCode : MonoBehaviour, Interactable
{
    public Transform TP_Up;
    public Transform TP_Down;
    public GameObject Player;
    public int countdown;
    public float currenttime;
    public bool canClimb;

    public void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        if (currenttime >= countdown)
        {
            canClimb = true;
        }
        else
        {
            currenttime += Time.deltaTime;
        }
    }
    public void Interacted()
    {
        if (canClimb == true)
        {
            if (Vector3.Distance(Player.transform.position, TP_Up.position) > Vector3.Distance(Player.transform.position, TP_Down.position))
            {
                Player.transform.position = TP_Up.position;
                currenttime = 0;
                canClimb = false;
            }
            else
            {
                Player.transform.position = TP_Down.position;
                currenttime = 0;
                canClimb = false;
            }
        }
    }
}
