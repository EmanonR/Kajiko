using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New card", menuName = "Card")]
public class UpgradeCard : ScriptableObject
{
    public string cardName;
    public Sprite Icon;
    public List<string> descriptions;
}
