using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public Sprite image;
    public string displayName;
    public string description = "Aquire the skill";

    public bool has;
    public int myLevel;

    public virtual void Aquire()
    {
        myLevel++;
        has = true;
    }

    public virtual void ResetSkill()
    {
        myLevel = 0;
        has = false;
        description = "Aquire the skill";
    }
}
