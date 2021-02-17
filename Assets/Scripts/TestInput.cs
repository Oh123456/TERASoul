using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    Animator animator;
    CharacterMoveMent moveMent;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        moveMent = GetComponent<CharacterMoveMent>();
    }


    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //moveMent.CharacterMove(horizontal, vertical, Time.deltaTime);
        //animator.SetInteger("AnimTest", 0);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            animator.SetInteger("AttackKinds", 1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            animator.SetInteger("AttackKinds", 2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            animator.SetInteger("AttackKinds", 3);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            animator.SetInteger("AttackKinds", 4);
    }
}
