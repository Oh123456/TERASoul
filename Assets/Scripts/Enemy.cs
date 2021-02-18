using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    CharacterMoveMent characterMoveMent;
    int originDamage;
    [SerializeField]
    List<int> damages;

    [SerializeField]
    List<int> comboAttackDamas;

    int comboAttackCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void Damage_ON()
    {
        base.WeaponeEnabled(true);

        Animator animator = GetComponent<Animator>();
        int attackKinds = animator.GetInteger("AttackKinds");
        if (attackKinds == 0)
            return;
        if (attackKinds != 3)
            base.SetDamage(damages[attackKinds - 1], ref originDamage);
        else
        {
            base.SetDamage(comboAttackDamas[comboAttackCount], ref originDamage);
            comboAttackCount++;
        }
    }

    void Damage_OFF()
    {
        base.WeaponeEnabled(true);
        base.damage = originDamage;
        comboAttackCount = 0;
    }

}
