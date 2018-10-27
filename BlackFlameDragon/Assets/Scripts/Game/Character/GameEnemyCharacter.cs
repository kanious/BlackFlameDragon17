using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameEnemyCharacter : Character
{
    [Header("Component")]
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Weapon m_Me;   //자기자신도 플레이어한테는 무기임

    [Header("Prefab")]
    [SerializeField] private GameObject m_StartingWeaponPrefab;   //처음에 가지고 시작할 무기 프리팹

    private float m_MoveDelay = 2.0f;
    private bool m_IsMoveEnable = true;
    public int index;
    public float fTime;
    public Image hpBar;

    #region Event
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

        //시작무기가있을 경우 생성/잡기
        if (m_StartingWeaponPrefab)
        {
            GameObject go = Instantiate(m_StartingWeaponPrefab);
            Catch(go.GetComponent<Weapon>());
        }

        //Idle애니 플레이
        if (m_CatchingWeapon)
            m_Animator.Play("Idle_Weapon");
        else
            m_Animator.Play("Idle_Hand");
    }
    private void Update()
    {
        if(0 < m_MoveDelay)
        {
            m_MoveDelay -= Time.deltaTime;
            return;
        }

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
    public void OnThrowAnimationEvent()
    {
        if (m_CatchingWeapon)
        {
            Weapon throwWeapon = m_CatchingWeapon;
            Release();
            throwWeapon.objectRigidbody.velocity = transform.TransformDirection(new Vector3(0, 3, 10));
        }
    }
    #endregion
    #region Function
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
    public void Move(Vector3 velocity)
    {
        transform.position += transform.TransformDirection(velocity) * status.fSpeed * Time.deltaTime;

    }
    public void Attack()
    {
        if (m_CatchingWeapon)
            m_Animator.Play("Attack_Weapon");
        else
            m_Animator.Play("Attack_Hand");
        //무기의 속도 / 방향에 따라서 데미지 변경하기 코드 만들기
    }
    public void Throw()
    {
        if (m_CatchingWeapon)
            m_Animator.Play("Throw");
    }
    #endregion
}