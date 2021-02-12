using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : CharacterMoveMent
{

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        base.CharacterMove(horizontal, vertical , Time.deltaTime);



        float jumpPower = Input.GetAxis("Jump");
        if (jumpPower > 0.0f)
            base.Jump(jumpPower);
    }
}
