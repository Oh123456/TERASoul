using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Animator animator;
    CharacterMoveMent moveMent;
    public bool isAttackInput = false;
    public bool isLock = false;
    int originDamage = 100;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveMent = GetComponent<CharacterMoveMent>();
        originDamage = GetComponent<Character>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLock)
            return;
        if ((!isAttackInput) & !(GetComponent<Character>().isMoveLock))
        {
            animator.applyRootMotion = false;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            moveMent.CharacterMove(horizontal, vertical, Time.deltaTime);

            float jumpPower = Input.GetAxis("Jump");
            if (jumpPower > 0.0f)
                moveMent.Jump(jumpPower);

        }
        else
            animator.applyRootMotion = true;

        if (Input.GetMouseButtonDown(0) & !isAttackInput)
        {
            animator.SetBool("Attack", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
        }
        else if (Input.GetMouseButtonDown(0) & 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("Great Sword Slash_LastAttack") &
            !animator.GetBool("Guard") & 
            !animator.GetBool("SkillAttack"))
        {
            animator.SetBool("ComboAttack", true);
            moveMent.CharacterMove(0, 0, Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) & !isAttackInput)
        {
            animator.SetBool("SkillAttack", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
            originDamage = GetComponent<Character>().damage;
            GetComponent<Character>().damage = (int)(GetComponent<Character>().damage * 1.5f); 
        }

         if (Input.GetKeyDown(KeyCode.F) & !isAttackInput)
        {
            animator.SetBool("Kick", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
            originDamage = GetComponent<Character>().damage;
            GetComponent<Character>().damage = (int)(GetComponent<Character>().damage * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.G) & !isAttackInput)
        {
            animator.SetBool("H_Kick", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
            originDamage = GetComponent<Character>().damage;
            GetComponent<Character>().damage = (int)(GetComponent<Character>().damage * 0.75f);
        }
        if (Input.GetMouseButtonDown(1))
            animator.SetBool("Guard", true);
        if (Input.GetMouseButtonUp(1))
            animator.SetBool("Guard", false);




        float x = Input.GetAxis("Mouse X");
        x += Input.GetAxis("KeyMouseMove");
        transform.Rotate(0.0f, x, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);


        if (Input.GetKeyDown(KeyCode.Alpha6))
            EffectManager.instance.SpawnEffect("ShockWave", new Vector3(0,0,0), new Quaternion(0,0,0,0));

    }

    void AttackEnd()
    {
        if (!animator.GetBool("ComboAttack"))
        {
            animator.SetBool("Attack", false);
            isAttackInput = false;
        }
        animator.SetBool("ComboAttack", false);
        animator.SetBool("SkillAttack", false);

        GetComponent<Character>().damage = originDamage;
    }

    void KickEnd()
    {
        animator.SetBool("Kick", false);
        animator.SetBool("H_Kick", false);
        isAttackInput = false;
        GetComponent<Character>().damage = originDamage;
    }



    
}
