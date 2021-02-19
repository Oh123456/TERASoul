using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : BaseComponent
{
    #region HP
    [SerializeField]
    protected int MaxHP;
    [SerializeField]
    protected int HP;
    #endregion

    #region stamina
    [SerializeField]
    protected int Maxstamina;
    [SerializeField]
    protected int Stamina;
    #endregion

    [SerializeField]
    protected int Damage;
   
    protected bool isDeath = false;
    protected bool isBreaking = false;

    [SerializeField]
    Weapon defaultWeapon;
 
    public Weapon weapon { get; set; }
    #region getset
    public int maxHP
    {
        get { return MaxHP; }
        set { MaxHP = value; }
    }
    public int hp
    {
        get { return HP; }
        set { HP = value; }
    }
    public int maxStamina
    {
        get { return Maxstamina; }
        set { Maxstamina = value; }
    }
    public int stamina
    {
        get { return Stamina; }
        set { Stamina = value; }
    }
    public int damage
    {
        get { return Damage; }
        set { Damage = value; }
    }
    #endregion

    public DamageManager damageManager;
    int hitDamage;

    private void Update()
    {
        Stamina += (int)(Time.deltaTime * 100);
        if (Stamina < Maxstamina)
            Stamina = Maxstamina;
    }

    public bool isBlocking
    {
        get
        {
            Animator animator = GetComponent<Animator>();
            if (animator == null)
                return false;
            return animator.GetBool("Blocking");
        }
        set
        {
            isBlocking = value;
        }
    }

    public bool isGuard
    {
        get
        {
            Animator animator = GetComponent<Animator>();
            
            if (!animator.GetBool("Guard"))
                return false;
            return animator.GetBool("Guard");
        }
        set
        {
            isGuard = value;
        }
    }

    private void Awake()
    {
        this.Init();

    }


    protected override void Init()
    {
        base.Init();
        HP = MaxHP;
        stamina = Maxstamina;
        isDeath = false;
        weapon = defaultWeapon;
    }

    public void TakeDamage(int damage)
    {
        hitDamage = damage;
        HP -= damage;
        if (HP <= 0)
            isDeath = true;
        GetComponent<Animator>().SetBool("Hit",true);
        GetComponent<Animator>().SetFloat("damage", damage);

    }

    public void TakeStaminaDamage(int damage)
    {
        stamina -= damage;
        if (stamina <= 0)
            isBreaking = true;
    }

    void Damage_ON()
    {
        weapon.Damage_ON();
    }

    void Damage_OFF()
    {
        weapon.Damage_OFF();
    }

    public void Blocking()
    {
        Animator animator = GetComponent<Animator>();
       animator.SetBool("Blocking", true);
    }

    void GuardEnd()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Blocking", false);
    }

    public void SetDamage(int damage, ref int origin)
    {
        origin = this.damage;
        this.damage = damage;
    }

    protected void WeaponeEnabled(bool value)
    {
        if (value)
            weapon.Damage_ON();
        else
            weapon.Damage_OFF();
    }

    void HitEnd()
    {
        GetComponent<Animator>().SetBool("Hit", false);
        GetComponent<Animator>().speed = 1.0f;
    }

    void HIt_Slow()
    {
        float speed = 0.75f - (float)(hitDamage - 250) / 500;
        if (speed <= 0.5f)
            speed = 0.5f;
        GetComponent<Animator>().speed = speed;
        hitDamage = 0;
        
    }

    void Reset()
    {
        this.State_Reset();
    }

    public virtual void State_Reset()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Attack",false);
        animator.SetBool("ComboAttack", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Hit", false);
        animator.speed = 1.0f;
    }
}
