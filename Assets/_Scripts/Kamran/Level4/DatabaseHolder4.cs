using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseHolder4", menuName = "ScriptableObjects/DatabaseHolder4", order = 1)]
public class DatabaseHolder4 : SingletonScriptableObject<DatabaseHolder4, ICreationMethodLocated>
{
    public List<Sprite> RainSprites;
    public List<string> Constants;
    public List<string> Prefixes;
    public List<string> EquationPrefix;
    public List<string> EquationSuffix;
}
