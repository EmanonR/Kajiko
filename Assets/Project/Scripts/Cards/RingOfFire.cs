using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Ring of fire", menuName = "Skill/RingOfFire")]
public class RingOfFire : Skill
{
    public static event Action<int, int, float, float, float> OnChanged;

    ROFUpgrade[] upgrades =
    {
        new(2, 2, 3, 75, 1, "Spawns embers that circle around you"),
        new(3, 2, 3, 75, 1, "Spawns 1 more ember"),
        new(5, 2, 3, 75, 1, "Spawns 2 more ember"),
        new(5, 2, 5, 150, 1, "Increased Range and speed"),
        new(5, 2, 5, 150, 2, "Increase Damage"),
        new(5, 4, 5, 150, 2, "Increased Hitcount"),
        new(5, 0, 5, 150, 4, "Remove hit cap and increase damage")
    };

    public override void ResetSkill()
    {
        myLevel = 0;
        has = false;
        done = false;
        description = upgrades[0].upgradeDescription;
    }

    public override void Aquire()
    {
        OnChanged?.Invoke(upgrades[myLevel].emberCount, upgrades[myLevel].hitCount, upgrades[myLevel].distance, upgrades[myLevel].speed, upgrades[myLevel].damageMultiplier);

        if (myLevel + 1 < upgrades.Count())
            description = upgrades[myLevel + 1].upgradeDescription;

        base.Aquire();

        if (myLevel >= upgrades.Count()) done = true;
    }

    public class ROFUpgrade
    {
        public int emberCount;
        public int hitCount;
        public float distance;
        public float speed;
        public float damageMultiplier;
        public string upgradeDescription;

        public ROFUpgrade(int emberCount, int hitCount, float distance, float speed, float damageMultiplier, string upgradeDescription)
        {
            this.emberCount = emberCount;
            this.hitCount = hitCount;
            this.distance = distance;
            this.speed = speed;
            this.damageMultiplier = damageMultiplier;
            this.upgradeDescription = upgradeDescription;
        }
    }
}
