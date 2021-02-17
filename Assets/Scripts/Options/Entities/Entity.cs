using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class Entity : MonoBehaviour
{
    public delegate void AttackHandler(Entity attacker, string target, SpellList queue);
    public static event AttackHandler performAttackEvent;
    public string ent_name;

    public int health;
    public int order_number;

    //dict of attack types and proficiencies with each one. Float multiplied by damage output
    public Dictionary<string, float> spells;
    public Dictionary<string, float> weapons;
    //dict of attack types and entities resistances or vulnerabilities to it as a float. The float will be multipied against the incoming damage
    public Dictionary<string, float> resistances;

    public SpellList test;

    protected virtual void Start(){
        performAttackEvent += checkIfTarget;
        spells = new Dictionary<string, float>();
        weapons = new Dictionary<string, float>();
        resistances = new Dictionary<string, float>();
    }

    //should fire off event when this entity attacks something
    protected virtual void attack(string target, SpellList queue){
        Log(ent_name + " is attacking " + target);
        performAttackEvent?.Invoke(this, target, queue);
    }

    //whenever an entity attacks, check if this whas the target
    protected virtual void checkIfTarget(Entity attacker, string target, SpellList queue){
        if(ent_name == target){
            float totaldmg = 0;
            foreach(var item in queue){
                totaldmg += applyModAndResist(item);
            }
            health -= (int)totaldmg;
            Log(attacker.ent_name + " has attacked " + ent_name);
        }
    }

    //Applies attacking enemies mods and targeted enemies resistances to spell
    protected virtual float applyModAndResist((Spell, float) tup){
        float res = 0f;
        Spell spell = tup.Item1;

        //retrieves entities resistance modifiers. if one isnt set for the given spell, default to 1
        if(resistances.ContainsKey(spell.spell_name)){
            res = resistances[spell.spell_name];
        }else{
            res = 1f;
        }

        return spell.damage * tup.Item2 * res;
    }

}
