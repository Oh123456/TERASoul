using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



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

    public int takeDamage { set; get; }

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

    public bool isMoveLock = false;

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

    public virtual void TakeDamage(int damage)
    {
        hitDamage = damage;
        HP -= damage;
        if (HP <= 0)
        {
            GetComponent<Animator>().SetBool("Die", true);
            isDeath = true;
            Invoke("ReGame", 10.0f);
        }
        GetComponent<Animator>().SetBool("Hit",true);
        GetComponent<Animator>().SetFloat("damage", damage);

        takeDamage = damage;

    }

    public void TakeStaminaDamage(int damage)
    {
        stamina -= damage;
        if (stamina <= 0)
        {
            Vector3 vector3 = gameObject.transform.position;
            vector3.y += 1.0f;
            EffectManager.instance.SpawnEffect("BreakingEffect", vector3, gameObject.transform.rotation, gameObject);
            GetComponent<Animator>().SetBool("Breaking", true);
            isBreaking = true;
        }
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
        GetComponent<Animator>().SetBool("Mirror", false);
        GetComponent<Animator>().speed = 1.0f;
        isMoveLock = false;
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
        Debug.Log("HitEnd");
        this.State_Reset();
        isMoveLock = true;
        GetComponent<Animator>().speed = 1.0f;
    }



    void MoveLock()
    {
        
    }

    void Slow(float slow)
    {
        GetComponent<Animator>().speed = slow;
    }

    void Slow_End()
    {
        GetComponent<Animator>().speed = 1.0f;
    }

    protected virtual void Breaking_End()
    {
        isMoveLock = false;
        GetComponent<Animator>().SetBool("Breaking", false);
    }

    public virtual void State_Reset()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Attack",false);
        animator.SetBool("Guard", false);
        animator.SetBool("ComboAttack", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Hit", false);
        animator.speed = 1.0f;
    }

    void ReGame()
    {
       SceneManager.LoadScene("Main");
    }
}
