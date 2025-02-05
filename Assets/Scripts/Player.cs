using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int id;
    public int maxHealth;
    public int health;
    public int armor;
    public int maxMana;
    public int turnMana;
    public int mana;

    [Space]
    [Header("Game Mechanics")]
    public int[] ascended = { 7, 10};
    public bool isFullyAscended = false;
    public event Action OnTurnStart;
    public event Action OnTurnEnd;
    //public int[] 

    [Space]
    [Header("GameObjects")]
    public Unit[] units;

    public Deck deck;
    public Hand hand;

    [Space]
    [Header("Others")]
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image manaImage;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private Image armorImage;
    [SerializeField] private TMP_Text armorText;
    private void Update()
    {
        healthText.text = health + "";
        manaText.text = mana + "";
        armorText.text = armor + "";
    }
    public void StartTurn()
    {
        OnTurnStart?.Invoke();

    }
    public void EndTurn()
    {
        OnTurnEnd?.Invoke();

    }
    public int TakeDamage(int damage)
    {
        if (armor > 0)
        {
            (armor, damage) = (armor - damage, damage - armor);
            if (armor < 0) armor = 0;
            if (damage < 0) damage = 0;
        }
        health -= damage;
        if (health <= 0)
        {
            (health, damage) = (0, damage + health);
            Die();
        }
        healthText.text = health + "";
        return damage;
    }
    public int HealUp(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            (health, heal) = (maxHealth, heal - (health - maxHealth));
        }
        healthText.text = health + "";
        return heal;
    }
    public void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player " + id + " has lost");
    }
}
