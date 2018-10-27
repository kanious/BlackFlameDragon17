using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class Character : MonoBehaviour {

    #region Inspector
    [Header("Balance")]
    [SerializeField] protected characterStatus status;
    #endregion
    #region Value
    protected Weapon m_Punch
    {
        get;
        private set;
    }
    protected Weapon m_CatchingWeapon
    {
        get;
        private set;
    }
    #endregion

    #region Event
    protected virtual void Awake()
    {
        m_Punch = status.RightHand.GetComponent<Weapon>();
        m_Punch.SetOwnerCharacter(this);
    }
    #endregion
    #region Function
    protected void Catch(Weapon weapon)
    {
        status.RightHand.Catch(weapon);
        if (status.RightHand.catchingObject)
        {
            m_CatchingWeapon = status.RightHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeapon)
                m_CatchingWeapon.SetOwnerCharacter(this);
        }
    }
    protected void Right_Catch()
    {
        status.RightHand.Catch();
        if (status.RightHand.catchingObject)
        {
            m_CatchingWeapon = status.RightHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeapon)
                m_CatchingWeapon.SetOwnerCharacter(this);
        }
    }
    protected void Left_Catch()
    {
        status.LeftHand.Catch();
        if (status.LeftHand.catchingObject)
        {
            m_CatchingWeapon = status.LeftHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeapon)
                m_CatchingWeapon.SetOwnerCharacter(this);
        }
    }
    protected void Right_Release()
    {
        m_CatchingWeapon = null;
        status.RightHand.Release();
    }
    protected void Left_Release()
    {
        m_CatchingWeapon = null;
        status.LeftHand.Release();
    }

    /// <summary>
    /// 데미지 입을 시 수행, 남은 체력이 0이면 true 반환, 아니면 false
    /// </summary>
    internal virtual bool Damaged(int value)
    {
        status.iHp -= value;

        if (0 >= status.iHp)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
    #endregion
}