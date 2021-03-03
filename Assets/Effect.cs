using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField]
    string tag;
    [SerializeField]
    float time;

    private void OnEnable()
    {
        Invoke("Delay", time);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Delay()
    {
        EffectManager.instance.ReturnEffect(tag, this.gameObject);

    }
}

