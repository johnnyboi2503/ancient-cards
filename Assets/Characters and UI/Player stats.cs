using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "Scriptable Objects/Playerstats")]
public class Playerstats : ScriptableObject
{
    public int HP = 100;
    public List<Attacks> AerialAttacks;
    public List<Attacks> NeutralAttacks;
    public List<Attacks> GroundAttacks;
    public List<Attacks> FarAttacks;
    public List<Attacks> MidRangeAttacks;
    public List<Attacks> CloseAttacks;
}
