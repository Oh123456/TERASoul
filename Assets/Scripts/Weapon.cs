using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject owner;
    protected Collider weaponCollider;
    [SerializeField]
    WeapneRay ray;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
        ray = GetComponent<WeapneRay>();
    }

    private void Update()
    {
 
        //RaycastHit hit;
        //Debug.DrawRay(ray.position, gameObject.transform.forward,Color.red, 3.0f);
        //if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 2.0f))
        //{
        //    Debug.Log( hit.point);
        //}
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
                if (ray)
                    ray.Hit();
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
