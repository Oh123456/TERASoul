using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageManager 
{
    public abstract bool KickDamage(GameObject strikObejct, GameObject hitObject);
}


public class EnemyDamage : DamageManager
{
    public override bool KickDamage(GameObject strikObejct, GameObject hitObject)
    {
        if (hitObject.tag == "Player")
        {
            Character character = hitObject.gameObject.GetComponent<Character>();
            Debug.Log(strikObejct.GetComponent<Character>().damage);
            if (character.isGuard)
            {
                character.TakeStaminaDamage(strikObejct.GetComponent<Character>().damage * 5);
                if (character.stamina >= 0)
                    character.Blocking();
            }
            else
                character.TakeDamage(strikObejct.GetComponent<Character>().damage);

            return true;
        }
        return false;
    }
}

public class PlayerDamage : DamageManager
{
    public override bool KickDamage(GameObject strikObejct, GameObject hitObject)
    {
        if (hitObject.tag == "Character")
        {
            // Next Add Code
            Character character = hitObject.gameObject.GetComponent<Character>();
            character.TakeDamage(strikObejct.GetComponent<Character>().damage);
            return true;
        }
        return false;
    }
}