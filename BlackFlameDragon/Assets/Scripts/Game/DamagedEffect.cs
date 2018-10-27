using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedEffect : MonoBehaviour {

    float fTime = 0f;
    bool bActive = false;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void Update () {
		
        if(bActive)
        {
            fTime += Time.deltaTime;

            if(0.1f < fTime)
            {
                fTime = 0f;
                bActive = false;
                this.gameObject.SetActive(false);
            }
        }
	}

    public void DamageEffect_On()
    {
        bActive = true;
        fTime = 0;
        this.gameObject.SetActive(true);
    }
}
