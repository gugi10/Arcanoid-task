using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoredData 
{
    public int currentScore;
    public int mapWidth;
    public int mapHeight;
    public int hp;
    public string seed;
    public float fillPercent;
    public bool canBeContinued;
    public int[,] coords;

}
