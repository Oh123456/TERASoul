using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanPlayer : Character
{
    [SerializeField]
    GameObject L_LEG;

    [SerializeField]
    GameObject R_LEG;

    [SerializeField]
    int kickDamage;

    [SerializeField]
    int rightKickDamage;

    private void Awake()
    {
        damageManager = new PlayerDamage();
        base.Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Stamina += (int)(Time.deltaTime * 100);
        if (Stamina > Maxstamina)
            Stamina = Maxstamina;
        if (Stamina < -50)
            Stamina = -50;
        //HP += 1;
        //if (MaxHP < HP)
        //    HP = MaxHP;
    }

    void L_Kick_ON()
    {
        L_LEG.SetActive(true);
    }

    void L_Kick_OFF()
    {
        L_LEG.SetActive(false);
    }


    void R_Kick_ON()
    {
        R_LEG.SetActive(true);
    }

    void R_Kick_OFF()
    {
        R_LEG.SetActive(false);
    }

    //        
    public override void State_Reset()
    {
        base.State_Reset();
        Animator animator = GetComponent<Animator>();
        animator.SetBool("H_Kick", false);

        GetComponent<PlayerInput>().isAttackInput = false;
    }

    void Die()
    {
        GetComponent<PlayerInput>().enabled = false;
    }

    void Lock()
    {
        base.State_Reset();
        GetComponent<PlayerInput>().isLock = true;
        GetComponent<Animator>().speed = 1.0f;
    }

    protected override void Breaking_End()
    {
        base.Breaking_End();
        GetComponent<PlayerInput>().isLock = false ;
    }


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GameObject _UI =  GameObject.FindWithTag("HUD");
        HitImage hitImage = _UI.GetComponent<HitImage>();
        hitImage.OnHitSrceen();
 

    }

    void SuperArmor()
    {

    }
}



// 공통적인 질문 
/* 
 * //인성 면접//
 * 자소서 잘읽어두자 스토리 말하기
 * 모르는건 모른다고 말해두자 면접관이 당황스럽게 만들게 하는 질문들이다
 * 본인 기술문서 잘 읽어두자
 * 언어 선호도 C/C++ C#
 * 넓이 우선 탐색
 * 
 * 가상함수 소멸자에 버추얼 
 * 
 */
