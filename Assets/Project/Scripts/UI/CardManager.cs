using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new();
    public GameObject skipButton;

    private void Start()
    {
        LevelingManager.OnLevelUp += EnableCards;
        DisableCards();
    }

    private void OnDestroy()
    {
        LevelingManager.OnLevelUp -= EnableCards;
    }

    public void CardChosen(int i)
    {
        //Do the upgrade
        DisableCards();
        cards[i].skill.Aquire();
        GameManager.Instance.InvokeResume();
    }

    public void Cancel()
    {
        DisableCards();
        GameManager.Instance.InvokeResume();
    }

    void EnableCards()
    {
        List<Skill> chosen = SkillManager.Instance.GetRan3Skills();

        if (chosen.Count == 0) return;

        for (int i = 0; i < cards.Count; i++)
        {
            if (i >= chosen.Count)
            {
                cards[i].DisableCard();
                cards[i].gameObject.SetActive(false);
            }
            else
            {
                cards[i].gameObject.SetActive(true);
                cards[i].SetCard(chosen[i]); 
                cards[i].EnableCard();
            }
        }

        skipButton.SetActive(true);

        GameManager.Instance.SetCardSelecting();
    }

    void DisableCards()
    {
        foreach (var card in cards)
        {
            card.DisableCard();
        }

        skipButton.SetActive(false);
    }
}
