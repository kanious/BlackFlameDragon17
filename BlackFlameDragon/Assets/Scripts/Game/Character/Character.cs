using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class Character : MonoBehaviour {

    #region Inspector
    [Header("Component")]
    [SerializeField] private Animator m_Animator;

    [Header("Balance")]
    [SerializeField] protected characterStatus status;
    #endregion
    #region Value
    private Weapon m_Punch;
    private Weapon m_CatchingWeapon;
    #endregion

    #region Event
    protected virtual void Awake()
    {
        m_Punch = status.RightHand.GetComponent<Weapon>();
        m_Punch.SetOwnerCharacter(this);
    }

    public void OnThrowAnimationEvent()
    {
        if(m_CatchingWeapon)
        {
            Weapon throwWeapon = m_CatchingWeapon;
            Release();
            throwWeapon.objectRigidbody.velocity = transform.TransformDirection(new Vector3(0,3,3));
        }
    }
    #endregion
    #region Function
    public void Move()
    {
    }
    public void Attack()
    {
        if (m_CatchingWeapon)
            m_Animator.Play("Attack");
        else
            m_Animator.Play("Punch");
        //무기의 속도 / 방향에 따라서 데미지 변경하기 코드 만들기
    }
    public void Throw()
    {
        if (m_CatchingWeapon)
            m_Animator.Play("Throw");
    }
    protected void Catch()
    {
        status.RightHand.Catch();
        if (status.RightHand.catchingObject)
        {
            m_CatchingWeapon = status.RightHand.catchingObject.GetComponent<Weapon>();

            if (m_CatchingWeapon)
                m_CatchingWeapon.SetOwnerCharacter(this);
        }
    }
    protected void Release()
    {
        m_CatchingWeapon = null;
        status.RightHand.Release();
    }

    /// <summary>
    /// 데미지 입을 시 수행, 남은 체력이 0이면 true 반환, 아니면 false
    /// </summary>
    internal bool Damaged(int value)
    {
        status.iHp -= value;

        if (0 >= status.iHp)
            return true;

        return false;
    }
    #endregion
}