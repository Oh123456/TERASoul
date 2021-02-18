using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageManager 
{
    public abstract void KickDamage(GameObject strikObejct, GameObject hitObject);
}


public class EnemyDamage : DamageManager
{
    public override void KickDamage(GameObject strikObejct, GameObject hitObject)
    {
        if (hitObject.tag == "Player")
        {
            Character character = hitObject.gameObject.GetComponent<Character>();
            Debug.Log(strikObejct.GetComponent<Character>().damage);
            if (character.isGuard)
            {
                character.TakeStaminaDamage(strikObejct.GetComponent<Character>().damage * 5);
            }
            else
                character.TakeDamage(strikObejct.GetComponent<Character>().damage);
        }
    }
}

public class PlayerDamage : DamageManager
{
    public override void KickDamage(GameObject strikObejct, GameObject hitObject)
    {
        if (hitObject.tag == "Character")
        {
            // Next Add Code
            Character character = hitObject.gameObject.GetComponent<Character>();
            character.TakeDamage(strikObejct.GetComponent<Character>().damage);
        }
    }
}