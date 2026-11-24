using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Stat/Luck", fileName = "Luck Skill")]
public class UpgradeLuck : Skill
{
    //public static event Action<float> OnChanged;

    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.luckStat.ResetValues();
    }

    public override void Aquire()
    {
        //OnChanged?.Invoke(PlayerStats.luckStat.GetCalculated());

        PlayerStats.luckStat.multiplierValue += .2f;
        description = "Luck +20%";

        base.Aquire();
    }
}

[CreateAssetMenu(menuName = "Skill/Stat/Movement", fileName = "Movement Skill")]
public class UpgradeMovement : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.moveSpeedStat.ResetValues();
    }

    public override void Aquire()
    {
        PlayerStats.moveSpeedStat.multiplierValue += .05f;
        description = "Movement +05%";

        base.Aquire();
    }
}

[CreateAssetMenu(menuName = "Skill/Stat/Fire", fileName = "Fire Skill")]
public class UpgradeFire : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.fireDamage.ResetValues();
    }

    public override void Aquire()
    {
        PlayerStats.fireDamage.multiplierValue += .15f;
        description = "Fire damage +15%";

        base.Aquire();
    }
}

[CreateAssetMenu(menuName = "Skill/Stat/Reload", fileName = "Reload Skill")]
public class UpgradeReload : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.reloadSpeed.ResetValues();
    }

    public override void Aquire()
    {
        PlayerStats.reloadSpeed.multiplierValue += .1f;
        description = "Reload speed +10%";

        base.Aquire();
    }
}

[CreateAssetMenu(menuName = "Skill/Stat/ExpBonus", fileName = "ExpBonus Skill")]
public class UpgradeExpBonus : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.expBonus.ResetValues();
    }

    public override void Aquire()
    {
        PlayerStats.expBonus.multiplierValue += .1f;
        description = "Exp bonus +10%";

        base.Aquire();
    }
}

[CreateAssetMenu(menuName = "Skill/Stat/Pickup", fileName = "Pickup Skill")]
public class UpgradePickup : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.pickupRange.ResetValues();
    }

    public override void Aquire()
    {
        PlayerStats.pickupRange.multiplierValue += .1f;
        description = "Pickup range +10%";

        base.Aquire();
    }
}