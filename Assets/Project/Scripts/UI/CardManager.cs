using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Skill> availableCards = new();
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
        GameManager.Instance.InvokeResume();
    }

    public void Cancel()
    {
        DisableCards();
        GameManager.Instance.InvokeResume();
    }

    void EnableCards()
    {
        if (availableCards.Count == 0) return;

        foreach (var card in cards)
        {
            card.SetCard(availableCards[Random.Range(0, availableCards.Count)]);
            card.EnableCard();
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
