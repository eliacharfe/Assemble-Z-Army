using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Char.CharacterStat;

public class StatSwordman : MonoBehaviour
{
    public CharacterStat HP;
    public CharacterStat Attack;
    public CharacterStat Defense;
    public CharacterStat ReachDistance;
    public CharacterStat SpeedAttack;

    void Start()
    {
        HP.BaseValue = 100;
        Attack.BaseValue = 10;
        Defense.BaseValue = 5;
        ReachDistance.BaseValue = 10;
        SpeedAttack.BaseValue = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
