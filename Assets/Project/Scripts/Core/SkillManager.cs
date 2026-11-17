using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skills = new();

    public static SkillManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        ClearALLSkills();
    }

    public void ClearALLSkills()
    {
        foreach (var skill in skills)
        {
            skill.ResetSkill();
        }
    }

    public List<Skill> GetRan3Skills()
    {
        List<int> numsChosen = new();
        List<Skill> list = new();

        foreach (var skill in skills)
        {
            if (numsChosen.Count >= skills.Count)
                break;

            if (numsChosen.Count >= 3)
                break;

            int r = GetRanIntExcluded(numsChosen, 0, skills.Count);

            if (skills[r].done) continue;

            list.Add(skills[r]);
            numsChosen.Add(r);
        }

        return list;
    }

    int GetRanIntExcluded(List<int> ExcludeList, int low, int high)
    {
        List<int> compareList = new();

        for (int i = low; i < high; i++)
        {
            if (!ExcludeList.Contains(i))
                compareList.Add(i);
        }

        return compareList[Random.Range(0, compareList.Count)];
    }
}
