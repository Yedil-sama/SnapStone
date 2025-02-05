using UnityEngine;

public enum Rarity
{
    Classic,
    Common,
    Rare,
    Epic,
    Legendary
}
public class CardSO : ScriptableObject
{
    public int id;
    public string name;
    public int cost;
    public Rarity rarity;
    public string description;
}
