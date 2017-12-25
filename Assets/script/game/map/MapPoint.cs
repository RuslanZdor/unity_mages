using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapPoint {
    public int id;
    public int fightPower;
    public Vector2 position;
    public bool isEnable;
    public bool isFinal;

    public List<string> dependList = new List<string>();
}
