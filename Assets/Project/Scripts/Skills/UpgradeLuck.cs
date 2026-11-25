using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Luck", fileName = "Luck Skill")]
public class UpgradeLuck : Skill
{
    //public static event Action<float> OnChanged;
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.luckStat.ResetValues();
        description = "Luck +20%";
    }

    public override void Aquire()
    {
        //OnChanged?.Invoke(PlayerStats.luckStat.GetCalculated());

        PlayerStats.luckStat.multiplierValue += .2f;

        base.Aquire();
    }
}