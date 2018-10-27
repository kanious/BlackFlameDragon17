using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class Weapon : GameCatchObject
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private Collider m_CatchCollider;  //잡을때 사용하는 콜라이더
    [SerializeField] private Collider[] m_AttackTrigger;  //공격할 때 사용하는 트리거

    [Header("Balance")]
    [SerializeField] public int m_Damage;
    #endregion
    #region Const
    private const int valDamagedCharacterListSize = 10; //데미지를 이미 입은 상태의 캐릭터 리스트의 크기
    #endregion
    #region Value
    private bool m_IsPlayerWeapon;
    private Vector3 m_LastedPos;
    private bool m_AttackEnable = true;
    float minSpeed;
    #endregion

    #region Event
    private void Awake()
    {
        m_LastedPos = transform.position;
        if(m_CatchCollider)
            m_CatchCollider.enabled = true;
        for (int i = 0; i < m_AttackTrigger.Length; ++i)
            m_AttackTrigger[i].enabled = false;
    }
    private void Update()
    {
        m_LastedPos = transform.position;
    }
    /*
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.attachedRigidbody)
        {
            Character target = collision.collider.attachedRigidbody.GetComponent<Character>();

            if (target)
            {
                //Debug.Log("ASDFASDFASDF");
                float speed = (transform.position - m_LastedPos).magnitude;
                OnWeaponCollisionEnter(target, speed, m_Damage);
            }
        }
    }
    */
    void OnTriggerEnter(Collider other)
    {
        if (m_AttackEnable && other.attachedRigidbody)
        {
            Character target = other.attachedRigidbody.GetComponent<Character>();

            if (target)
            {
                float speed = (transform.position - m_LastedPos).magnitude;
                if (speed < minSpeed)
                    speed = minSpeed;
                OnWeaponCollisionEnter(target, speed, m_Damage);
            }
        }
    }

    //WeaponTriggerEvent
    internal void OnWeaponCollisionEnter(Character target, float triggerSpeed, int baseDamage)
    {
        if (m_IsPlayerWeapon != GetIsPlayerCharacter(target))
        {
            //공격하기(속력에 따른 데미지 배율등을 생각해서 공격, 이미 공격한 캐릭터 리스트에 추가)
            int realDamage = baseDamage;
            realDamage = (int)(realDamage * GameManager.Instance.GetDamageFactor(Mathf.Sqrt(triggerSpeed)));

            //데미지가 0 이상이면 실제 데미지를 입힌다.
            if (0 < realDamage)
            {
                target.Damaged(realDamage);
                Vector3 effectPos = (target.transform.position + transform.position) * 0.5f;
                EffectManager.instance.SpawnEffect(effectPos);
            }
        }
    }

    //GameCatchObject
    protected override void OnCatched()
    {
        base.OnCatched();
        m_CatchCollider.enabled = false;
        for (int i = 0; i < m_AttackTrigger.Length; ++i)
            m_AttackTrigger[i].enabled = true;
    }
    protected override void OnReleased()
    {
        base.OnReleased();
        if (catchingHandCount <= 0)
        {
            m_CatchCollider.enabled = true;
            for (int i = 0; i < m_AttackTrigger.Length; ++i)
                m_AttackTrigger[i].enabled = false;

            transform.SetParent(null);
        }

        m_Rigidbody.velocity = transform.position - m_LastedPos;
    }
    #endregion
    #region Function
    //Public
    public void SetMinSpeed(float speed)
    {
        minSpeed = speed;
    }
    /// <summary>
    /// 주운 캐릭터를 설정한다.
    /// </summary>
    /// <param name="character">캐릭터</param>
    public void SetOwnerCharacter(Character character)
    {
        m_IsPlayerWeapon = GetIsPlayerCharacter(character);
    }
    public void SetAttackEnable(bool isEnable)
    {
        m_AttackEnable = isEnable;
        for (int i = 0; i < m_AttackTrigger.Length; ++i)
            m_AttackTrigger[i].enabled = isEnable;
    }

    //Private
    private bool GetIsPlayerCharacter(Character character)
    {
        return (character.GetComponent<GamePlayerCharacter>() != null) ? true : false;
    }
    #endregion
}