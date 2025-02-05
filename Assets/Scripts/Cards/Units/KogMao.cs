using UnityEngine;

public class KogMao : MonoBehaviour
{
    public Unit unit;
    [Header("Ability")]
    public int explodeDamage = 1;
    public float damageMultiplier = 2f;
    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.OnDeath += OnDeath;
    }
    public void OnDeath()
    {
        int damageLeft = (int)(unit.attack * damageMultiplier);
        while (damageLeft > 0)
        {
            foreach (Unit enemy in GameManager.Instance.locations[unit.location].GetEnemies(unit))
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(explodeDamage);
                    damageLeft -= explodeDamage;
                }
            }
        }
    } 
}
