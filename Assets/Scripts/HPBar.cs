using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    Character character;
    [SerializeField]
    GameObject fill;
    Slider HPbar;

    float maxHP;
    float hp; 

    float PreviousHp;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponent<Slider>();

        maxHP = character.maxHP;
        hp = character.hp;
    }

    // Update is called once per frame
    void Update()
    {
        
        hp = character.hp;
        float value = hp / maxHP;
        HPbar.value = value;

        if (value <= 0.001f)
            fill.SetActive(false);
    }

    public void SetTarget(Character character) { this.character = character; }


}
