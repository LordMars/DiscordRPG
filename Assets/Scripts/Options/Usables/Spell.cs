using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public string spell_name;
    public int damage;
    List<string> properties;
    public Spell(string name, int damage){
        this.spell_name = name;
        this.damage = damage;
    }

    public (Spell, float) applyMod(float mod){
        return (this, mod);
    }
}
