using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseHolder", menuName = "ScriptableObjects/DatabaseHolder", order = 1)]
public class DatabaseHolder : SingletonScriptableObject<DatabaseHolder,ICreationMethodLocated>
{
    public Sprite LostHeadSprite;
    public Sprite RedTail;
    public Sprite BlueTail;
    public List<Sprite> collectibleRandomSprites;
    public List<string> KeyStrings;
    public List<string> KeyPrefixes;
    public List<string> Constants;
    public string DecimalFarsiFormat;
    public string DivisionFarsiFormat;
}
