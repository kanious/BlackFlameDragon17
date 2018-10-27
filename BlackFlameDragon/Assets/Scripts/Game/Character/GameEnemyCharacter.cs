using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameEnemyCharacter : Character {

    [SerializeField] private Weapon m_Me;

    private bool m_IsMoveEnable = true;
    public int index;
    public float fTime;
    public Image hpBar;

    protected override void Awake()
    {
        m_Me.onCatched += OnCatched;
        m_Me.onReleased += OnReleased;

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
        if (m_IsMoveEnable)
        {
            this.transform.LookAt(GameManager.Instance.Player.transform);
            this.transform.position = Vector3.MoveTowards(this.transform.position
                , GameManager.Instance.Player.transform.position, Time.deltaTime * status.fSpeed);
        }
    }
    private void OnCatched()
    {
        m_IsMoveEnable = false;
    }
    private void OnReleased()
    {
        if (m_Me.catchingHandCount <= 0)
        {
            m_IsMoveEnable = true;
        }
    }
    internal override bool Damaged(int value)
    {
        if (base.Damaged(value))
        {
            SpawnManager.Instance.RemoveEnemy(gameObject);
            return true;
        }
        else
            return false;
    }
}