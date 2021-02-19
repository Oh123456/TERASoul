using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    [SerializeField]
    Collider defaultCollider;
    [SerializeField]
    List<GameObject> collider_Objects;

    Dictionary<string, GameObject> colliders;  
    
    // Start is called before the first frame update
    void Start()
    {
        colliders = new Dictionary<string, GameObject>(collider_Objects.Count);
        weaponCollider = defaultCollider; 
        foreach (var items in collider_Objects)
        {
            colliders.Add(items.tag, items);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Damage_ON()
    {
        defaultCollider.enabled = true;
    }

    public override void Damage_OFF()
    {
        defaultCollider.enabled = false;
    }

    public void WeaponColliderEnabled(string tag, bool value)
    {
        colliders[tag].SetActive(value);
    }

    public void WeaponColliderEnabled_ALL()
    {
        foreach (var items in colliders)
        {
            items.Value.SetActive(false);
        }
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
                WeaponColliderEnabled_ALL();
            }
            else
            {
                character.TakeStaminaDamage((int)((float)(owner.GetComponent<Character>().damage) / 2));
                if (character.stamina >= 0)
                    character.Blocking();
                weaponCollider.enabled = false;
                WeaponColliderEnabled_ALL();
            }
        }
    }
}
