using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObjects/CubeData", order = 1)]
public class CubeData : ScriptableObject
{
    public enum Color
    {
        None, Yellow, Blue, White, Green, Red
    };
    /*
    public enum RequiredCondition
    {
        None, inAny, noRed, inLight, inDark
    };
    */
    public enum Condition
    {
        None, inLight, inDark, inRed, inLightAndDark
    };

    public Color cubeColor;
    public Condition[] winContitions;
}
