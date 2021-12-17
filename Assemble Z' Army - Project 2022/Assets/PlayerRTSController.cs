using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRTSController : MonoBehaviour
{
    public List<Unit> m_units = new List<Unit>();


    // Start is called before the first frame update
    void Start()
    {
        Unit.OnUnitSpawned += addUnit;
        Unit.OnDeUnitSpawned += removeUnit;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addUnit(Unit unit)
    {
        m_units.Add(unit);
    }

    void removeUnit(Unit unit)
    {
        m_units.Remove(unit);
    }

}
