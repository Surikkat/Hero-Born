using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Паладин - один из игровых персонажей с оружием
public class Paladin : Character
{
    public Weapon weapon;

    public Paladin(string name, Weapon weapon) : base(name)
    {
        this.weapon = weapon;
    }

    public override void PrintStatsInfo()
    {
        Debug.LogFormat("Hail {0} - take your {1}!", name, weapon.name);
    }
}
