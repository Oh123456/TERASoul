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
        Debug.DrawRay(weaponeStart_obj.transform.position, weapon.transform.forward * distance, Color.red, 0.1f);

        Debug.DrawRay(hit.point, test * 100, Color.blue, 0.1f);

    }

    public void Hit()
    {

       // while (true)
        {
            hits = Physics.RaycastAll(weaponeStart_obj.transform.position, weapon.transform.forward, 20.0f);
            if (hits.Length != 0)
            {
                foreach (var items in hits)
                {
                    if (items.collider.gameObject != owner & items.collider.gameObject.tag == "Enemy")
                    {
                        hit = items;
                        break;
                    }
                }
                Vector3 Z = new Vector3(0.0f, 0.0f, weaponeStart_obj.transform.position.z);
                test = Vector3.Cross(hit.point, Z);
                blood.SetActive(true);
                blood.transform.position = hit.point;

            }
        }
    }
}
