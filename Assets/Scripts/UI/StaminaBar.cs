using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]
    Character character;
    [SerializeField]
    GameObject fill;
    Slider HPbar;

    float maxStamina;
    float stamina;

    // Start is called before the first frame update
    void Start()
    {
        HPbar = GetComponent<Slider>();

        maxStamina = character.maxStamina;
        stamina = character.stamina;
    }

    // Update is called once per frame
    void Update()
    {
        maxStamina = character.maxStamina;
        stamina = character.stamina;
        float value = stamina / maxStamina;
        HPbar.value = value;
    }

    public void SetTarget(Character character) { this.character = character; }
}
