using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    [Header("Data")]
    public int cost;
    public string name;
    public string description;
    public Player owner;

    [Space]
    [Header("Controllers")]
    public Image selectedImage;
    public int gameState = 0;
    public bool isSelected = false;

    [Space]
    [Header("UI")]
    [SerializeField] protected Image descriptionImage;
    [SerializeField] protected TMP_Text descriptionText;
    [SerializeField] protected Image rarityImage;
    [SerializeField] protected TMP_Text rarityText;
    [SerializeField] protected Image costImage;
    [SerializeField] protected TMP_Text costText;
    public void OnClick()
    {
        GameManager.Instance.OnSelectCard(this);
    }
    public virtual void OnBurn() { }
    public virtual void OnDraw() { }
    public event Action OnPlay;

}
