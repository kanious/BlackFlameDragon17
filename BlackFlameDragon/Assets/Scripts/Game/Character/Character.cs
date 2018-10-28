using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class Character : MonoBehaviour {

    #region Inspector
    [Header("Component")]
    [SerializeField] protected AudioSource m_AudioSource;
    
    [Header("Balance")]
    [SerializeField] protected characterStatus status;
    #endregion
    #region Value
    protected Weapon m_PunchRight
    {
        get;
        private set;
    }
    protected Weapon m_PunchLeft
    {
        get;
        private set;
    }

    protected Weapon m_CatchingWeaponRight
    {
        get;
        private set;
    }
    protected Weapon m_CatchingWeaponLeft
    {
        get;
        private set;
    }
    #endregion

    #region Event
    protected virtual void Awake()
    {
        if (status.RightHand)
        {
            m_PunchRight = status.RightHand.GetComponent<Weapon>();
            if (status.RightHand)
            {
                m_PunchRight.SetOwnerCharacter(this);
                m_PunchRight.m_Damage = status.iAttack;
            }
        }

        if (status.LeftHand)
        {
            m_PunchLeft = status.LeftHand.GetComponent<Weapon>();
            if (m_PunchLeft)
            {
                m_PunchLeft.SetOwnerCharacter(this);
                m_PunchLeft.m_Damage = status.iAttack;
            }
        }
    }
    #endregion
    #region Function
    protected void Right_Catch(Weapon weapon)
    {
        status.RightHand.Catch(weapon);
        if (status.RightHand.catchingObject)
        {
            m_CatchingWeaponRight = status.RightHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeaponRight)
                m_CatchingWeaponRight.SetOwnerCharacter(this);
        }
    }
    protected void Right_Catch()
    {
        status.RightHand.Catch();
        if (status.RightHand.catchingObject)
        {
            m_CatchingWeaponRight = status.RightHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeaponRight)
                m_CatchingWeaponRight.SetOwnerCharacter(this);
        }
    }
    protected void Left_Catch()
    {
        status.LeftHand.Catch();
        if (status.LeftHand.catchingObject)
        {
            m_CatchingWeaponLeft = status.LeftHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeaponLeft)
                m_CatchingWeaponLeft.SetOwnerCharacter(this);
        }
    }
    protected void Right_Release()
    {
        m_CatchingWeaponRight = null;
        status.RightHand.Release();
    }
    protected void Left_Release()
    {
        m_CatchingWeaponLeft = null;
        status.LeftHand.Release();
    }

    /// <summary>
    /// 데미지 입을 시 수행, 남은 체력이 0이면 true 반환, 아니면 false
    /// </summary>
    internal virtual bool Damaged(int value)
    {
        if(GameManager.Instance)
        m_AudioSource.PlayOneShot(GameManager.Instance.m_AttackEffect[Random.Range(0, GameManager.Instance.m_AttackEffect.Length)]);
        status.iHp -= value;

        if (0 >= status.iHp)
        {
            return true;
        }

        return false;
    }
    #endregion
}