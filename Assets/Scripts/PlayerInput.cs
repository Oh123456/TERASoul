using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Animator animator;
    CharacterMoveMent moveMent;
    public bool isAttackInput = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveMent = GetComponent<CharacterMoveMent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttackInput)
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
            !animator.GetBool("Guard"))
        {
            animator.SetBool("ComboAttack", true);
            moveMent.CharacterMove(0, 0, Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.F) & !isAttackInput)
        {
            animator.SetBool("Kick", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.G) & !isAttackInput)
        {
            animator.SetBool("H_Kick", true);
            isAttackInput = true;
            moveMent.CharacterMove(0, 0, Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(1))
            animator.SetBool("Guard", true);
        if (Input.GetMouseButtonUp(1))
            animator.SetBool("Guard", false);




        float x = Input.GetAxis("Mouse X");
        x += Input.GetAxis("KeyMouseMove");
        transform.Rotate(0.0f, x, 0.0f);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);

    }

    void AttackEnd()
    {
        if (!animator.GetBool("ComboAttack"))
        {
            animator.SetBool("Attack", false);
            isAttackInput = false;
        }
        animator.SetBool("ComboAttack", false);
    }

    void KickEnd()
    {
        animator.SetBool("Kick", false);
        animator.SetBool("H_Kick", false);
        isAttackInput = false;
    }

}
