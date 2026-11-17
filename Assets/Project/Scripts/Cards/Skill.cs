using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public Sprite image;
    public string displayName;
    

    [HideInInspector] public bool has;
    [HideInInspector] public bool done;
    [HideInInspector] public int myLevel;
    [HideInInspector] public string description = "Aquire the skill";

    public virtual void Aquire()
    {
        myLevel++;
        has = true;
    }

    public virtual void ResetSkill()
    {
        myLevel = 0;
        has = false;
        done = false;
        description = "Aquire the skill";
    }
}
