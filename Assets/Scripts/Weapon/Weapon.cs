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

    Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = GetComponent<Collider>();
        ray = GetComponent<WeapneRay>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(hitPoint, 0.01f);
        //weaponeEnd_obj
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

    protected virtual void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject != owner & other != null)
        {
            Character character = other.gameObject.GetComponent<Character>();
            if (!character.isGuard)
            {
                
                hitPoint = other.ClosestPoint(transform.position);
                
                if ((Quaternion.LookRotation(hitPoint - other.transform.position, Vector3.up).x) > -0.5f)
                    character.GetComponent<Animator>().SetBool("Mirror",false);
                else
                    character.GetComponent<Animator>().SetBool("Mirror", true);
                character.TakeDamage(owner.GetComponent<Character>().damage);
                weaponCollider.enabled = false;    
                EffectManager.instance.SpawnEffect("HitEffect", other.ClosestPoint(transform.position), Quaternion.LookRotation(gameObject.transform.position - other.transform.position, Vector3.up) /*gameObject.transform.rotation*/);

            }
            else
            {
                hitPoint = other.ClosestPoint(transform.position);
                Debug.Log(Quaternion.LookRotation(hitPoint - other.transform.position, Vector3.up).eulerAngles);
                EffectManager.instance.SpawnEffect("Star_A", other.ClosestPoint(transform.position), gameObject.transform.rotation);
                character.TakeStaminaDamage((int)((float)(owner.GetComponent<Character>().damage) / 2));
                if (character.stamina >= 0)
                    character.Blocking();
                weaponCollider.enabled = false;
            }
        }
    }


    int count = 0;
    IEnumerator Coroutine_HitRay()
    {
        if (ray)
        {
            while (true)
            {
                WeapneRayHit hit = ray.Hit();
                if (hit.isHit)
                {
                    
                    EffectManager.instance.SpawnEffect("ShockWave", hit.point,gameObject.transform.rotation);
                    count = 0;
                    StopAllCoroutines();
                 
                }

                count++;
                yield return new WaitForSeconds(0.1f);
                if (count == 10)
                {
                    count = 0;
                    StopAllCoroutines();
                }
            }
        }
    }

}
