using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Monster",fileName ="Monster_")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;
    public int attack;
    public int hp;
    public int mp;
}
