using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ragnarok", menuName = "Skill/Weapons/Ragnarok")]
public class RagnarokSkill : Skill
{
    public static event Action<int, float, int, float, float> OnChanged;

    RagnarokUpgrade[] upgrades =
    {
        new(2, 2, 10, 15, 1, "Shoots rocks of fire at nearby enemies"),
        new(3, 2, 10, 15, 1, "Spawns 1 extra rocks"),
        new(3, 2, 10, 15, 2, "Increased damage"),
        new(4, 2, 10, 15, 2, "Spawns 1 extra rocks"),
        new(4, 2, 20, 15, 2, "Shoots further"),
        new(6, 2, 20, 15, 2, "Spawns 2 extra rocks"),
        new(6, 2, 20, 15, 4, "Increased damage"),
        new(6, 1.5f, 20, 30, 4, "Rocks spawn and fly faster"),
    };

    public class RagnarokUpgrade
    {
        public int spawnCount;
        public float spawnTime;
        public int shootDistance;
        public float flySpeed;
        public float damageMultiplier;
        public string upgradeDescription;

        public RagnarokUpgrade(int spawnCount, float spawnTime, int shootDistance, int flySpeed, float damageMultiplier, string upgradeDescription)
        {
            this.spawnCount = spawnCount;
            this.spawnTime = spawnTime;
            this.shootDistance = shootDistance;
            this.flySpeed = flySpeed;
            this.damageMultiplier = damageMultiplier;
            this.upgradeDescription = upgradeDescription;
        }
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        description = upgrades[0].upgradeDescription;
    }

    public override void Aquire()
    {
        OnChanged?.Invoke(upgrades[myLevel].spawnCount, upgrades[myLevel].spawnTime, upgrades[myLevel].shootDistance, upgrades[myLevel].flySpeed, upgrades[myLevel].damageMultiplier);

        if (myLevel + 1 < upgrades.Length)
            description = upgrades[myLevel + 1].upgradeDescription;

        base.Aquire();

        if (myLevel >= upgrades.Length) done = true;
    }
}
