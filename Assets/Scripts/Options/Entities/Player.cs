using System.Collections.Generic;
using static UnityEngine.Debug;
public class Player : Entity
{

    protected override void Start()
    {
        base.Start();
        spells.Add("fire", 2.3f);
        spells.Add("ice", 1f);
    }

    //called when a player sends a message starting with .act
    public void act(string msg){

        //populates spell queue with spells from message string and their multiplier for the attacking entity
        SpellList spell_queue = new SpellList();
        foreach(var spell in spells){
            var type = spell.Key;
            var mod = spell.Value;
            if(msg.Contains(type)){
                Spell curspell = AllSpells.dict[type];
                spell_queue.Add(curspell.applyMod(mod));
            }
        }

        //checks if the keyword on is present in the string
        var is_on_present = msg.IndexOf("on ");
        if(is_on_present == -1){
            Log("Failed");
        }else{
            var val = msg.Substring(is_on_present + 3);
            attack(val, spell_queue);
        }

    }

}
