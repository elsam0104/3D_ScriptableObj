using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class EditorCSVtoSO : MonoBehaviour
{
    private static string csvFilePath = "/Resources/monsterCSV.csv";

    [MenuItem("Utill/CSVtoSO")]
    public static void SetMonster()
    {
        string[] strData = File.ReadAllLines(Application.dataPath + csvFilePath);

        foreach(string sData in strData)
        {
            string[] data = sData.Split(',');

            MonsterSO monster = ScriptableObject.CreateInstance<MonsterSO>();
            monster.monsterName = data[0];
            monster.attack = int.Parse(data[1]);
            monster.hp = int.Parse(data[2]);
            monster.mp = int.Parse(data[3]);

            AssetDatabase.CreateAsset(monster, $"Assets/DataAsset/{monster.monsterName}.asset");
        }

        AssetDatabase.SaveAssets();
    }
}
