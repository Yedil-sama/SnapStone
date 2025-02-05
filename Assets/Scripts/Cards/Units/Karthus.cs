using UnityEngine;

public class Karthus : MonoBehaviour
{
    public Unit unit;

    [Header("Ability")]
    public int explodeDamage = 2;
    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.OnDeath += OnDeath;
    }
    private void OnDeath()
    {
        for (int i = 0; i < GameManager.Instance.locations.Length; i++) 
        {
            foreach (Unit enemy in GameManager.Instance.locations[i].GetEnemies(unit))
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(explodeDamage);
                }
            }
        }
    }
}
