using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanPlayer : Character
{
    [SerializeField]
    GameObject L_LEG;

    [SerializeField]
    GameObject R_LEG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp -= 1;
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
}
