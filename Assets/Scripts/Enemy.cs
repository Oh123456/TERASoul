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

    [SerializeField]
    GameObject R_LEG;
    int comboAttackCount = 0;

    private void Awake()
    {
        damageManager = (new EnemyDamage());
        base.Init();
    }

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
        else if (attackKinds == 99)
            base.SetDamage(damages[2], ref originDamage);
        else
        {
            base.SetDamage(comboAttackDamas[comboAttackCount], ref originDamage);
            comboAttackCount++;
        }
    }

    void Damage_OFF()
    {
        base.WeaponeEnabled(false);
        base.damage = originDamage;
        comboAttackCount = 0;
    }


    void Kick_ON()
    {
        R_LEG.SetActive(true);
        base.SetDamage(damages[6], ref originDamage);
    }

    void Kick_OFF()
    {
        R_LEG.SetActive(false);
        base.damage = originDamage;
    }
}
