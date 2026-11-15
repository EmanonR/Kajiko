using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Skill> availableCards = new();
    public List<Card> cards = new();
    public GameObject skipButton;

    private void Start()
    {
        LevelingManager.OnLeveUp += EnableCards;
        DisableCards();
    }

    private void OnDestroy()
    {
        LevelingManager.OnLeveUp -= EnableCards;
    }

    public void CardChosen(int i)
    {
        //Do the upgrade
        DisableCards();
    }

    public void Cancel()
    {
        DisableCards();
    }

    void EnableCards()
    {
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

        GameManager.Instance.InvokeResume();
    }
}
