using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitImage : MonoBehaviour
{
    
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHitSrceen()
    {
        StartCoroutine("Coroutine_OnHitSrceen");
    }

    IEnumerator Coroutine_OnHitSrceen()
    {
        image.enabled = true;
        yield return new WaitForSeconds(1.0f);
        image.enabled = false;
        StopCoroutine("Coroutine_OnHitSrceen");
    }
}
