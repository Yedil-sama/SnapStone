using System;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Unit : Card, IDamagable
{
    [Space]
    [Header("Base Stats")]
    public UnitSO baseStats;
    public string state;// 0 sleep; 1 ready; 2 rush; 3 freeze 
    public int location;
    public int position;

    [Space]
    [Header("Stats")]
    public int maxHealth;
    public int health;
    public int attack;

    [Header("Attributes")]
    public bool taunt = false;
    public bool rush = false;
    public bool charge = false;
    public bool fastAttack = false;
    public bool vamperize = false;
    public int armor = 0;
    public int bloodthirstiness = 0;

    [Space]
    [Header("UI")]
    [SerializeField] private Image sprite;
    [SerializeField] private Image nameImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image attackImage;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text healthText;

    public event Action OnDeath;
    public event Action OnSummon;
    public event Action OnAttack;
    public event Action OnKill;
    
    private void Awake()
    {
        name = baseStats.name;
        description = baseStats.description;
        cost = baseStats.cost;
        maxHealth = baseStats.maxHealth;
        health = baseStats.health;
        attack = baseStats.attack;
        taunt = baseStats.taunt;
        rush = baseStats.rush;
        charge = baseStats.charge;
        fastAttack = baseStats.fastAttack;
        vamperize = baseStats.vamperize;
        armor = baseStats.armor;
        bloodthirstiness = baseStats.bloodthirstiness;
    }

    private void Start()
    {
        rarityText.text = baseStats.rarity + "";
        sprite.sprite = baseStats.sprite;
        costText.text = cost + "";
        nameText.text = name;
        descriptionText.text = description;
        attackText.text = attack + "";
        healthText.text = health + "";

        selectedImage.enabled = false;
    }
    public void Attack(Unit target)
    {
        OnAttack?.Invoke();
        if (fastAttack)
        {
            target.TakeDamage(attack);
            if (target.health <= 0)
            {
                OnKill?.Invoke();
            }
            else
            {
                TakeDamage(target.attack);
            }
        }

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
        return damage;
    }
    public int HealUp(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            (health, heal) = (maxHealth, heal - (health - maxHealth));
        }
        return heal;
    }
    public void Die()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

}
