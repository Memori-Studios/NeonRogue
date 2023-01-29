using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public struct Weights
{
    [Range(0,10)] public int intersectionWeight;
    [Range(0,10)] public int roadStraightWeight;
    [Range(0,10)] public int waterWeight;
    [Range(0,10)] public int waterBendWeight;
    public int GetWeight(Attribute a)
    {
        if(a==Attribute.Intersection)
            return intersectionWeight;
        else if(a==Attribute.RoadStraight)
            return roadStraightWeight;
        else if(a==Attribute.Water)
            return waterWeight;
        else if(a==Attribute.WaterBend)
            return waterBendWeight;

        return 0;
    }
}
public enum Attribute {Intersection, RoadStraight, Water, WaterBend};
