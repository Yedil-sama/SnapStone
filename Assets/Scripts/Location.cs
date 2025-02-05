using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public LocationSlot[] positions;
    [SerializeField] private GameObject[] sides;
    public List<Unit> GetAll()
    {
        List<Unit> result = new List<Unit>();
        for (int i=0; i < positions.Length; i++)
        {
            if (positions[i].unit != null)
            {
                result.Add(positions[i].unit);
            }
        }
        return result;
    }
    public List<Unit> GetAllies(Unit unit)
    {
        List<Unit> result = new List<Unit>();
        int length = positions.Length - 4 * (unit.owner.id == 1 ? 0 : 1);
        for(int i = 4 * unit.owner.id; i < length; i++)
        {
            if (positions[i].unit != null)
            {
                result.Add(positions[i].unit);
            }
        }
        return result;
    }
    public List<Unit> GetEnemies(Unit unit)
    {
        List<Unit> result = new List<Unit>();
        int length = positions.Length - 4 * (unit.owner.id == 0 ? 0 : 1);
        for (int i = 4 * (unit.owner.id == 1 ? 0 : 1); i < length; i++)
        {
            if (positions[i].unit != null)
            {
                result.Add(positions[i].unit);
            }
        }
        return result;
    }

    public void Summon(Unit unit, int position)
    {
        Summon(unit, position, unit.owner.id);
    }
    public void Summon(Unit unit, int position, int side)
    {
        
    }
}
