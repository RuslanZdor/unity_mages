using System.Collections.Generic;
using UnityEngine;

public class MapPoint {
    public int id;
    public int fightPower;
    public Vector2 position;
    public bool isEnable;
    public bool isFinal;
    public int count;
    public MapPointType type;

    public List<string> dependList = new List<string>();
}
