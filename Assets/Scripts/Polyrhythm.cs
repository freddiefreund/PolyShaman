using UnityEngine;
using System.Collections;
using static AttackBase;

public class Polyrhythm
{
    public int Id { get; }
    public float LeftHandInterval { get; }
    public float RightHandInterval { get; }
    public float LeftCountNeeded { get; }
    public float RightCountNeeded { get; }
    public bool IsPossible { set; get; }

    public Polyrhythm(int id, int countLeft, int countRight)
    {
        Id = id;

        LeftHandInterval = 1.0f;//(1.0f * countRight) / (1.0f * countLeft);
        LeftCountNeeded = countLeft;

        RightHandInterval = (1.0f * countLeft) / (1.0f * countRight);
        RightCountNeeded = countRight;
        IsPossible = true;
    }

    public string ToString()
    {
        return $"{LeftCountNeeded} / {RightCountNeeded}";
    }
}