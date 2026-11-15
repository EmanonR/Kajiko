using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Skill skill;

    public TMP_Text descriptionText;
    public Image skillImage;
    public GameObject backgroundImage;

    public void EnableCard()
    {
        backgroundImage.SetActive(true);
        descriptionText.gameObject.SetActive(true);
        skillImage.gameObject.SetActive(true);
    }

    public void DisableCard()
    {
        backgroundImage.SetActive(false);
        descriptionText.gameObject.SetActive(false);
        skillImage.gameObject.SetActive(false);
    }

    public void SetCard(Skill incomingSkill)
    {
        skill = incomingSkill;
        descriptionText.text = skill.description;
        skillImage.sprite = skill.image;
    }
}
