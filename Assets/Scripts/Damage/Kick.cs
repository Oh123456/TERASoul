using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour
{
    Collider kickCollider;
    [SerializeField]
    GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        kickCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.GetComponentInParent<Character>().damageManager.KickDamage(owner, other.gameObject))
            gameObject.SetActive(false);
    }

    public void SetEnable(bool value)
    {
        owner.SetActive(value);
    }
}
