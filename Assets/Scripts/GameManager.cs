using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int turn = 0;

    public event Action OnAscend;
    public event Action OnEachTurnStart;
    public event Action OnEachTurnEnd;

    public Card selectedCard;
    public Unit selectedUnit;
    public GameObject observingCardHolder;
    public Location[] locations;

    [SerializeField] private Player[] players;
    [SerializeField] private List<Card> cards;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        players[0].hand.gameObject.SetActive(true);
        players[1].hand.gameObject.SetActive(false);
    }
    public void StartTurn()
    {
        players[turn].hand.gameObject.SetActive(true);

        if (players[turn].mana < players[turn].maxMana) {
            players[turn].mana = ++players[turn].turnMana;
            
        }
        for (int i = 0; i < players[turn].ascended.Length; i++)
        {
            if (!players[turn].isFullyAscended && players[turn].mana == players[turn].ascended[i])
            {
                OnAscend?.Invoke();
                if (players[turn].ascended[i] == players[turn].maxMana)
                {
                    players[turn].isFullyAscended = true;
                }
                Debug.Log("Ascended for player No " + turn);
            }
        }

        players[turn].StartTurn();
        OnEachTurnStart?.Invoke();
    }
    public void EndTurn()
    {
        if (selectedCard != null) return;
        players[turn].EndTurn();
        OnEachTurnEnd?.Invoke();
        players[turn].hand.gameObject.SetActive(false);
        turn = (turn == 1 ? 0 : 1);
        StartTurn();

    }
    public void OnSelectCard(Card card)
    {
        if (selectedCard != null && selectedCard != card)
        {
            selectedCard.transform.SetParent(players[turn].hand.transform);
            selectedCard.transform.localScale = new Vector3(1f, 1f, 1f);
            selectedCard.selectedImage.enabled = false;
            selectedCard.gameState = 0;
        }

        if (card.gameState == 0 && card.transform.parent.gameObject == players[turn].hand.gameObject)
        {
            card.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            card.selectedImage.enabled = true;
            card.gameState = 1;
            selectedCard = card;
        }

        else if (card.gameState == 1)
        {
            card.transform.SetParent(observingCardHolder.transform);
            card.transform.position = new Vector3(0f, 0f, 0f);
            card.transform.localScale = new Vector3(2f, 2f, 1f);
            card.selectedImage.enabled = true;
            card.gameState = 2;
            selectedCard = card;
        }
        else if (card.gameState == 2 && card.transform.parent.gameObject == observingCardHolder)
        {
            card.transform.SetParent(players[turn].hand.transform);
            card.transform.localScale = new Vector3(1f, 1f, 1f);
            card.selectedImage.enabled = false;
            card.gameState = 0;
            selectedCard = null;
            
        }
    }
}
