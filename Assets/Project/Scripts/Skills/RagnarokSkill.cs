using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ragnarok", menuName = "Skill/Weapons/Ragnarok")]
public class RagnarokSkill : Skill
{
    //public static event Action<int, int, float, float, float> OnChanged;

    RagnarokUpgrade[] upgrades =
    {
        new(2, 15, 2, 1, "Shoots rocks of fire at nearby enemies")
    };

    public override void ResetSkill()
    {
        base.ResetSkill();
        description = upgrades[0].upgradeDescription;
    }

    public override void Aquire()
    {
        //OnChanged?.Invoke(upgrades[myLevel].emberCount, upgrades[myLevel].hitCount, upgrades[myLevel].distance, upgrades[myLevel].speed, upgrades[myLevel].damageMultiplier);

        if (myLevel + 1 < upgrades.Length)
            description = upgrades[myLevel + 1].upgradeDescription;

        base.Aquire();

        if (myLevel >= upgrades.Length) done = true;
    }

    public class RagnarokUpgrade
    {
        public int spawnCount;
        public float flySpeed;
        public float spawnTime;
        public float damageMultiplier;
        public string upgradeDescription;

        public RagnarokUpgrade(int spawnCount, int flySpeed, float spawnTime, float damageMultiplier, string upgradeDescription)
        {
            this.spawnCount = spawnCount;
            this.flySpeed = flySpeed;
            this.spawnTime = spawnTime;
            this.damageMultiplier = damageMultiplier;
            this.upgradeDescription = upgradeDescription;
        }
    }
}
