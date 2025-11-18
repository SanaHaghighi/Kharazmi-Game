using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseHolder", menuName = "ScriptableObjects/Dataholder5", order = 1)]
public class Dataholder5 : SingletonScriptableObject<Dataholder5, ICreationMethodLocated>
{
    [System.Serializable]
    public class Level
    {
        public string Question;
        public Sprite Ans1;
        public Sprite Ans2;
        public int AnswerNum;
    }
    public Level[] Levels;
}
