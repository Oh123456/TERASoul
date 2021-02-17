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

    [SerializeField]
    Weapon defaultWeapon;
 
    Weapon weapon { get; set; }
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


    private void Awake()
    {
        this.Init();
        weapon = defaultWeapon;
    }

    protected override void Init()
    {
        base.Init();
        HP = MaxHP;
        stamina = Maxstamina;
        isDeath = false;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            isDeath = true;
    }

    void Damage_ON()
    {
        weapon.Damage_ON();
    }

    void Damage_OFF()
    {
        weapon.Damage_OFF();
    }


}
