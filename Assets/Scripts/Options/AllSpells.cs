using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSpells{
    public static readonly Dictionary<string, Spell> dict  = new Dictionary<string, Spell>()
    {
        ["fire"] = new Spell("fire", 10),
        ["ice"] = new Spell("ice", 10)
    };
    public Spell this[string x]{
        get {return dict[x];}
        set {dict[x] = value;}
    }


}