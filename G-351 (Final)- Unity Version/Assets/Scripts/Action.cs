using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{

    public string name;
    public int damage;
    public List<Effect> effects;

    public Action(string actionName, int actionDamage, List<Effect> actionEffects){
        name = actionName;
        damage = actionDamage;
        effects = new List<Effect>(actionEffects);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
