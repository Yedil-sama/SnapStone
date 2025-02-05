using UnityEngine;

public class Renecton : MonoBehaviour
{
    public Unit unit;

    [Header("Ability")]
    public int sliceDamage = 1;

    [Header("Ascend")]
    public int ascendCount = 0;
    public int bonusHealth = 3;
    public int bonusAttack = 3;
    private void Start()
    {
        unit = GetComponent<Unit>();
        GameManager.Instance.OnAscend += Ascend;
        unit.owner.OnTurnEnd += Slice;
    }
    public void Ascend()
    {
        unit.maxHealth += bonusHealth;
        unit.health += bonusHealth;
        unit.attack += bonusAttack;
        if (ascendCount == 0)
        {
            sliceDamage = unit.baseStats.attack;
            ascendCount = 1;
        }
        else if (ascendCount == 1)
        {
            sliceDamage = unit.attack;
            unit.vamperize = true;
            ascendCount = 2;
        }
    }
    public void Slice()
    {
        foreach (Unit all in GameManager.Instance.locations[unit.location].GetAll())
        {
            if (all != null && all != unit)
            {
                all.TakeDamage(sliceDamage);
            }
        }
    }
}   
