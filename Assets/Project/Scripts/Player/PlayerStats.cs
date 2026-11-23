public static class PlayerStats
{
    public static float Luck;
    public static float moveSpeedMultiplier;
    public static float pickUpRange;
    public static float damageMultiplier;

    public static void ResetStats()
    {
        Luck = 1;
        moveSpeedMultiplier = 1;
        pickUpRange = 10;
        damageMultiplier = 1;
    }
}
