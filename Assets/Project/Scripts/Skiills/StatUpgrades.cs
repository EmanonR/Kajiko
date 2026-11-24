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

[CreateAssetMenu(menuName = "Skill/Stat/Movement", fileName = "Movement Skill")]
public class UpgradeMovement : Skill
{
    public override void ResetSkill()
    {
        base.ResetSkill();
        PlayerStats.moveSpeedStat.ResetValues();
        description = "Movement +10%";
    }

    public override void Aquire()
    {
        PlayerStats.moveSpeedStat.multiplierValue += .1f;

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
        description = "Fire damage +15%";
    }

    public override void Aquire()
    {
        PlayerStats.fireDamage.multiplierValue += .15f;

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
        description = "Reload speed +10%";
    }

    public override void Aquire()
    {
        PlayerStats.reloadSpeed.multiplierValue += .1f;

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
        description = "Exp bonus +10%";
    }

    public override void Aquire()
    {
        PlayerStats.expBonus.multiplierValue += .1f;

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
        description = "Pickup range +10%";
    }

    public override void Aquire()
    {
        PlayerStats.pickupRange.multiplierValue += .1f;

        base.Aquire();
    }
}