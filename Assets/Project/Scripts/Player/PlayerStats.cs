using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStat luckStat = new() { initialValue = 1 };
    public static PlayerStat moveSpeedStat = new() { initialValue = 12 };
    public static PlayerStat fireDamage = new() { initialValue = 1, multiplierValue = 1 };
    public static PlayerStat reloadSpeed = new() { initialValue = 1, multiplierValue = 1 };
    public static PlayerStat expBonus = new() { initialValue = 1, multiplierValue = 1 };
    public static PlayerStat pickupRange = new() { initialValue = 1, multiplierValue = 1 };


    public static void ResetStats()
    {
        luckStat.ResetValues();
        moveSpeedStat.ResetValues();
        fireDamage.ResetValues();
        reloadSpeed.ResetValues();
        expBonus.ResetValues();
        pickupRange.ResetValues();
    }
}

public class PlayerStat
{
    public float initialValue = 1;
    public float multiplierValue = 1;
    public float addValue = 0;

    public float GetCalculated()
    {
        return initialValue * multiplierValue + addValue;
    }

    public void ResetValues()
    {
        multiplierValue = 1;
        addValue = 0;
    }
}