using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamage : MonoBehaviour
{
    [SerializeField]
    Character character;
    [SerializeField]
    Text damageText;

    int damge;

    // Start is called before the first frame update
    void Start()
    {
        damageText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damageText.enabled == true)
        {
            damageText.text = damge.ToString();
        }
    }

    public void ShowDamageText(int damgae)
    {
        this.damge += damgae;
        Invoke("OffDamage", 1.5f);
        damageText.enabled = true;
    }

    void OffDamage()
    {
        damge = 0;
        damageText.enabled = false;
    }
}
