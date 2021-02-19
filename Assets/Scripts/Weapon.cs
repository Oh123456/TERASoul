using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject owner;
    protected Collider weaponCollider;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public virtual void Damage_ON()
    {
        weaponCollider.enabled = true;
    }

    public virtual void Damage_OFF()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != owner & other != null)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (!character.isGuard)
            {
                character.TakeDamage(owner.GetComponent<Character>().damage);
                weaponCollider.enabled = false;
            }
            else
            {
                character.TakeStaminaDamage((int)((float)(owner.GetComponent<Character>().damage) / 2));
                if (character.stamina >= 0)
                    character.Blocking();
                weaponCollider.enabled = false;
            }
        }
    }

    
}
