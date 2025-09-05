using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Playerstats", menuName = "Scriptable Objects/Playerstats")]
public class Playerstats : ScriptableObject
{
    public static int HP;
    public List<Attacks> AerialAttacks;
    public List<Attacks> NeutralAttacks;
    public List<Attacks> GroundAttacks;
    public List<Attacks> FarAttacks;
    public List<Attacks> MidRangeAttacks;
    public List<Attacks> CloseAttacks;
}
