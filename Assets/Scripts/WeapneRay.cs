using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapneRay : MonoBehaviour
{
    [SerializeField]
    GameObject weapon;
    [SerializeField]
    GameObject weaponeStart_obj;
    [SerializeField]
    GameObject weaponeEnd_obj;
    [SerializeField]
    float distance;
    [SerializeField]
    GameObject owner;
    [SerializeField]
    GameObject blood;

    Vector3 test;
    RaycastHit[] hits;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        blood.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(weaponeStart_obj.transform.position, weapon.transform.forward * distance, Color.red, 0.1f);

        Debug.DrawRay(hit.point, test * 100, Color.blue, 0.1f);
        

    }

    private void OnDrawGizmos()
    {
        //Gizmos.color =  Color.red;
        //Gizmos.DrawWireSphere(weaponeStart_obj.transform.position, distance);
        //Gizmos.DrawWireSphere(weaponeEnd_obj.transform.position, distance);
        //weaponeEnd_obj
    }

    public WeapneRayHit Hit()
    {
        WeapneRayHit rayhit;
        rayhit.isHit = false;
        rayhit.point = new Vector3(0,0,0);
        // while (true)
        {
            Debug.DrawRay(weaponeStart_obj.transform.position, weapon.transform.forward * distance, Color.red, 0.1f);
            hits = Physics.CapsuleCastAll(weaponeStart_obj.transform.position, weaponeEnd_obj.transform.position , distance , weapon.transform.forward, distance);
            if (hits.Length != 0)
            {
                foreach (var items in hits)
                {
                    if (items.collider.gameObject != owner & items.collider.gameObject.tag == "Enemy")
                    {
                        hit = items;
                        rayhit.isHit = true;
                        break;
                    }
                }
                if (rayhit.isHit)
                {
                    Vector3 Z = new Vector3(0.0f, weaponeStart_obj.transform.position.y, 0.0f);
                    test = Vector3.Cross(hit.point, Z);
                    blood.SetActive(true);
                    blood.transform.position = hit.point;
                    rayhit.point = hit.point;
                    return rayhit;
                }
            }
            return rayhit;
        }
    }

}


    public struct WeapneRayHit
    {
        public bool isHit;
        public Vector3 point;
    }