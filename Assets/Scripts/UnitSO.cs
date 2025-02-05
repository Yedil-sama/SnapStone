using UnityEngine;
[CreateAssetMenu(menuName = "New Card/Unit", fileName = "New Unit")]
public class UnitSO : CardSO
{
    [Header("Base Stats")]
    public int maxHealth;
    public int health;
    public int attack;

    [Header("Attibutes")]
    public bool taunt = false;
    public bool rush = false;
    public bool charge = false;
    public bool fastAttack = false;
    public bool vamperize = false;
    public int armor = 0;
    public int bloodthirstiness = 0;

    [Header("Others")]
    public Sprite sprite;
}
