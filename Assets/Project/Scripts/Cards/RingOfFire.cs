using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Ring of fire", menuName = "Skill/RingOfFire")]
public class RingOfFire : Skill
{
    public static event Action<int, int, float, float, float> OnChanged;

    private void OnEnable()
    {
        myLevel = 0;
        description = "Spawns embers that circle around you";
    }

    public override void Aquire()
    {
        switch (myLevel)
        {
            case 0:
                OnChanged?.Invoke(2, 2, 3, 75, .25f);
                description = "Spawns 1 more ember";
                break;
            case 1:
                OnChanged?.Invoke(3, 2, 3, 75, .25f);
                description = "Spawns 1 more ember";
                break;
            case 2:
                OnChanged?.Invoke(4, 2, 3, 75, .25f);
                description = "Increased Range";
                break;
            case 3:
                OnChanged?.Invoke(4, 2, 5, 75, .25f);
                description = "Increase ember Size and hitcount";
                break;
            case 4:
                OnChanged?.Invoke(4, 4, 5, 75, .5f);
                description = "Increased Speed";
                break;
            case 5:
                OnChanged?.Invoke(4, 4, 5, 150, .5f);
                description = "Remove hit cap";
                break;
            case 6:
                OnChanged?.Invoke(4, 0, 5, 150, .5f);
                break;
            default:
                break;
        }

        base.Aquire();
    }
}
