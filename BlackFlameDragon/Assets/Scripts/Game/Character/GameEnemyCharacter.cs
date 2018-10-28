using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class GameEnemyCharacter : Character
{
    [Header("Component")]
    [SerializeField] private Rigidbody m_Rigidbody;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Weapon m_Me;   //자기자신도 플레이어한테는 무기임
    [SerializeField] private Transform m_AniModel;
    [SerializeField] private Transform m_DieAnchor;

    [Header("Animation")]
    [SerializeField] private AnimationCurve m_DiePositionCurve;

    [Header("Prefab")]
    [SerializeField] private GameObject[] m_StartingWeaponPrefab;   //처음에 가지고 시작할 무기 프리팹

    [Header("Balance")]
    [SerializeField] private float m_AttackDelay;

    [Header("Color")]
    [SerializeField] private Renderer m_BodyRenderer;
    [SerializeField] private Material[] m_BodyMaterial;
    [SerializeField] private MeshRenderer[] m_StyleRenderer;
    [SerializeField] private Material[] m_StyleMaterial;

    private float m_MoveDelay = 1.0f;
    private bool m_IsMoveEnable = true;
    public int index;
    public float fTime;
    public GameObject hpBarObj;
    public Image hpBar;
    private Coroutine m_DieCoroutine;
    private Coroutine m_HittingCoroutine;
    private Coroutine m_AttackingCoroutine;
    private float m_AttackDelayTimer;
    public GamePlayerCharacter player;
    #region Get,Set
    private bool isDied
    {
        get
        {
            return (m_DieCoroutine != null);
        }
    }
    private bool isHitting
    {
        get
        {
            return m_HittingCoroutine != null;
        }
    }
    #endregion

    #region Event
    protected override void Awake()
    {
        base.Awake();
        m_Me.onCatched += OnCatched;
        m_Me.onReleased += OnReleased;

        //status.iHp = 100;
        //status.iMaxHp = 100;
        //status.iAttack = 5;
        status.iDefence = 2;
        status.iGauge = 0;
        status.iMaxGauge = 100;
        status.fSpeed = 2;

        player = GameObject.Find("PlayerCharacter").GetComponent<GamePlayerCharacter>();
        
        //시작무기가있을 경우 생성/잡기
        if (Random.Range(0,3) == 1)
        {
            GameObject go = Instantiate(m_StartingWeaponPrefab[Random.Range(0, m_StartingWeaponPrefab.Length)]);
            Right_Catch(go.GetComponent<Weapon>());
        }

        //Idle애니 플레이
        if (m_CatchingWeaponRight)
            m_Animator.Play("Idle_Weapon");
        else
            m_Animator.Play("Idle_Hand");

        m_Me.SetAttackEnable(false);

        hpBarObj.SetActive(false);
        /*
        if (m_PunchLeft)
            m_PunchLeft.SetMinSpeed(1000);
        if (m_PunchRight)
            m_PunchRight.SetMinSpeed(1000);*/

        //스타일 변경 
        m_BodyRenderer.material = m_BodyMaterial[Random.Range(0, m_BodyMaterial.Length)];
        int index = Random.Range(0, m_StyleRenderer.Length);
        for (int i = 0; i < m_StyleRenderer.Length; ++i)
        {
            if(i == index)
            {
                m_StyleRenderer[i].enabled = true;
                m_StyleRenderer[i].material = m_StyleMaterial[Random.Range(0, m_StyleMaterial.Length)];
            }
            else
                m_StyleRenderer[i].enabled = false;
        }
    }
    private void Update()
    {
        if (!isDied && !isHitting && m_AttackingCoroutine == null)
        {
            if (0 < m_AttackDelayTimer)
                m_AttackDelayTimer -= Time.deltaTime;
            if (0 < m_MoveDelay)
            {
                m_MoveDelay -= Time.deltaTime;
                return;
            }

            if (m_IsMoveEnable)
            {
                this.transform.LookAt(GameManager.Instance.Player.transform);
                this.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                this.transform.position = Vector3.MoveTowards(this.transform.position
                    , GameManager.Instance.Player.transform.position, Time.deltaTime * status.fSpeed);

                if (m_CatchingWeaponRight)
                    m_Animator.Play("Run_Weapon");
                else
                    m_Animator.Play("Run_Hand");
            }

            if (status.iHp == status.iMaxHp)
                hpBarObj.SetActive(false);
            else if (status.iHp < status.iMaxHp)
                hpBarObj.SetActive(true);
            
            hpBar.fillAmount = (float)status.iHp / (float)status.iMaxHp;
        }
    }
    private void OnCatched()
    {
        m_IsMoveEnable = false;
        m_Me.SetAttackEnable(true);
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
        if (m_CatchingWeaponRight)
        {
            Weapon throwWeapon = m_CatchingWeaponRight;
            Right_Release();
            throwWeapon.objectRigidbody.velocity = transform.TransformDirection(new Vector3(0, 3, 10));
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.name == "Ground" && m_Me.catchingHandCount <= 0)
            m_Me.SetAttackEnable(false);

        if (collision.collider.attachedRigidbody)
        {
            Debug.Log(collision.collider.attachedRigidbody.name);
            GamePlayerCharacter cha = collision.collider.attachedRigidbody.GetComponent<GamePlayerCharacter>();
            if(cha)
            {
                Attack();
                Debug.Log("ASDF");
            }
        }
    }
    #endregion
    #region Function
    internal override bool Damaged(int value)
    {
        player.AddGauge((int)(value * 0.33f));

        if (!isDied && !isHitting)
        {
            if (m_AttackingCoroutine != null)
                StopCoroutine(m_AttackingCoroutine);
            m_AttackingCoroutine = null;

            if (base.Damaged(value))
            {
                Right_Release();
                m_Animator.Play("Die");
                m_Rigidbody.velocity = Vector3.zero;
                m_Rigidbody.isKinematic = true;
                Collider[] col = m_Rigidbody.GetComponentsInChildren<Collider>();
                for (int i = 0; i < col.Length; ++i)
                    col[i].enabled = false;
                m_DieCoroutine = StartCoroutine(DieCoroutine());
            }
            else
            {
                m_Rigidbody.velocity = (transform.forward * -1 + transform.up) * 3;
                if (m_CatchingWeaponRight)
                    m_Animator.Play("Hit_Weapon");
                else
                    m_Animator.Play("Hit_Hand");

                m_HittingCoroutine = StartCoroutine(HitCoroutine(1.0f));
            }
            return true;
        }
        else
            return false;
    }
    IEnumerator DieCoroutine()
    {
        float timer = 0;
        Vector3 start = m_AniModel.localPosition;
        while(true)
        {
            timer += Time.deltaTime * 0.5f;

            m_AniModel.localPosition = Vector3.LerpUnclamped(start, m_DieAnchor.localPosition, m_DiePositionCurve.Evaluate(timer));
            if (1.0f <= timer)
                break;
            else
                yield return null;
        }
        SpawnManager.Instance.RemoveEnemy(gameObject);
    }
    public void Attack()
    {
        Debug.Log("Attack");
        if (m_AttackDelayTimer <= 0 && !isDied && !isHitting && m_AttackingCoroutine == null)
        {
            if (m_PunchLeft)
                m_PunchLeft.SetAttackEnable(true);
            if (m_PunchRight)
                m_PunchRight.SetAttackEnable(true);
            if (m_CatchingWeaponRight)
                m_CatchingWeaponRight.SetAttackEnable(true);

            if (m_CatchingWeaponRight)
            {
                m_AttackingCoroutine = StartCoroutine(AttackCoroutine(2.5f));
                m_Animator.Play("Attack_Weapon");
            }
            else
            {
                m_AttackingCoroutine = StartCoroutine(AttackCoroutine(3.5f));
                m_Animator.Play("Attack_Hand");
            }
            m_AttackDelayTimer = m_AttackDelay;
        }
    }
    public void Throw()
    {
        if (m_CatchingWeaponRight)
            m_Animator.Play("Throw");
    }
    IEnumerator AttackCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        m_AttackingCoroutine = null;

        if (m_PunchLeft)
            m_PunchLeft.SetAttackEnable(false);
        if (m_PunchRight)
            m_PunchRight.SetAttackEnable(false);
        if (m_CatchingWeaponRight)
            m_CatchingWeaponRight.SetAttackEnable(false);
    }
    IEnumerator HitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        m_HittingCoroutine = null;
    }
    #endregion
}