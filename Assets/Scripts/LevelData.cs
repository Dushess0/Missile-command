using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelData", menuName ="Data/Level Data")]
public class LevelData : ScriptableObject
{
    public int initialWave = 2;
    public int launchInterval = 5;
    public int additional_missiles = 3;
    public bool restockAmmo = false;
    public bool repair = false; 
}
