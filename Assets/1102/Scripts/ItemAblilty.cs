using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStack
{
    Int,
    Hp,
    Str
}

[System.Serializable]
public class ItemAblilty
{
    //캐릭터 능력치 enum
    public CharacterStack characterStack;

    //캐릭터 능력치 값
    public int valStack;

    [SerializeField]
    private int min;
    [SerializeField]
    private int max;

    public int Min => min;
    public int Max => max;

    public ItemAblilty(int min,int max)
    {
        this.min = min;
        this.max = max;

        GetStackValue();
    }

    public void GetStackValue()
    {
        valStack = UnityEngine.Random.Range(min, max);
    }

    public void AddStackVal(ref int v)
    {
        v += valStack;
    }
}