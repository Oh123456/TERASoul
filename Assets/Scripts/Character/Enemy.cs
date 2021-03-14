using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
  
    EnemyWeapon eWeapon;

    [SerializeField]
    GameObject ray;

    RaycastHit hit;


    private void Awake()
    {
        damageManager = (new EnemyDamage());
        base.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        eWeapon = (EnemyWeapon)weapon;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.DrawRay(ray.transform.position, -eWeapon.transform.right /*Vector3.Cross(ray.transform.position , eWeapon.transform.position )*/,  Color.green);
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

    void Damage_ON(string attack_tag)
    {

        //base.WeaponeEnabled(true);
        eWeapon.WeaponColliderEnabled(attack_tag,true);
        Animator animator = GetComponent<Animator>();
        int attackKinds = animator.GetInteger("AttackKinds");
        if (attackKinds == 0)
            return;
        if (attackKinds != 3)
        {
           if (attackKinds == 99)
                base.SetDamage(damages[2], ref originDamage);
           else
                base.SetDamage(damages[attackKinds - 1], ref originDamage);
        }
        else
        {
            base.SetDamage(comboAttackDamas[comboAttackCount], ref originDamage);
            comboAttackCount++;
        }
    }

    void Damage_OFF(string attack_tag)
    {
        eWeapon.WeaponColliderEnabled(attack_tag, false);
        //base.WeaponeEnabled(false);
        base.damage = originDamage;

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

    void ComboAttackEnd()
    {
        comboAttackCount = 0;
    }

    void ShockWave()
    {
        if (Physics.Raycast(ray.transform.position, -eWeapon.transform.right, out hit, 1.5f))
        {
            EffectManager.instance.SpawnEffect("ShockWave", hit.point , new Quaternion(0,0,0,0));
            
        }

    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameObject[] finds = GameObject.FindGameObjectsWithTag("HPBar");
        UIDamage uiDamage = null;
        foreach (var gameObject in finds)
        {
            if (gameObject.GetComponent<UIDamage>() != null)
            {
                uiDamage = gameObject.GetComponent<UIDamage>();
                break;
            }

        }
        if (uiDamage)
            uiDamage.ShowDamageText(damage);

        if (isDeath)
            GetComponent<FreeManAI>().enabled = false;
    }


}
