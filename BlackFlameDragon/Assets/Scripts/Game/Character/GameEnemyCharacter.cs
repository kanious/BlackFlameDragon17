using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameEnemyCharacter : Character {

    public int index;
    public float fTime;
    public Image hpBar;
    
    private void Awake()
    {
        status.iHp = 100;
        status.iMaxHp = 100;
        status.iAttack = 5;
        status.iDefence = 2;
        status.iGauge = 0;
        status.iMaxGauge = 100;
        status.fSpeed = 2;
        
        Debug.Log("hp:" + status.iHp + ", maxHp:" + status.iMaxHp);
    }

    private void Update()
    {
        this.transform.LookAt(GameManager.Instance.Player.transform);
        this.transform.position = Vector3.MoveTowards(this.transform.position
            , GameManager.Instance.Player.transform.position, Time.deltaTime * status.fSpeed);

        fTime += Time.deltaTime;
        if(6f < fTime)
        {
            SpawnManager.Instance.RemoveEnemy(gameObject);
            Destroy(this.gameObject);
        }

        hpBar.fillAmount = (float)status.iHp / (float)status.iMaxHp;
    }
}
