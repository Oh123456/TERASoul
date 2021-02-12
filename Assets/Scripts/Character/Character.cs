using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : BaseComponent
{
    #region HP
    [SerializeField]
    protected int MaxHP { get; set; }
    [SerializeField]
    protected int HP { get; set; }
    #endregion

    #region stamina
    [SerializeField]
    protected int Maxstamina { get; set; }
    [SerializeField]
    protected int stamina { get; set; }
    #endregion

    [SerializeField]
    protected int damage;

    protected bool isDeath = false;

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
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
            isDeath = true;
    }
}
