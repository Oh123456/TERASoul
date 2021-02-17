using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject owner;
    Collider weaponCollider;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public void Damage_ON()
    {
        weaponCollider.enabled = true;
    }

    public void Damage_OFF()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != owner & other != null)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (!character.isGuard)
                character.TakeDamage(owner.GetComponent<Character>().damage);
            else
            {
                character.TakeStaminaDamage((int)((float)(owner.GetComponent<Character>().damage) / 0.5f));
                if (character.stamina >= 0)
                    character.Blocking();
            }
        }
    }
}
