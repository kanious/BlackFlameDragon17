using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PackageProject.SpecialHelper.CatchHand;
using UnityEngine.SceneManagement;

public class GamePlayerCharacter : Character
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private Transform m_BlackDragonRoot;
    [SerializeField] private GameCharacterHand m_LeftHand;
    [SerializeField] private GameCharacterHand m_RightHand;
    [Header("Prefab")]
    [SerializeField] private GameObject m_BlackDragonPrefab;

    [Header("SoundEFfect")]
    [SerializeField] private AudioClip m_BlackUseClip;
    [SerializeField] private AudioClip m_BlackFireClip;
    #endregion
    
    public DamagedEffect effect;
    public Image HPImage;
    public Image SkillImage;
    public Text scoreText;
    bool isUsingBlackDragon;

    public Animator leftHand;
    public Animator rightHand;

    float fTimeDelay = 0f;
    bool dragonDelay = false;

    #region Event
    protected override void Awake()
    {
        base.Awake();

        status.iHp = 1000;
        status.iMaxHp = 1000;
        status.iGauge = 0;
        status.iMaxGauge = 100;

        if(leftHand)
            leftHand.Play("Open");
        if (rightHand)
            rightHand.Play("Open");
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Catch();
            if (leftHand)
                leftHand.Play("Grab");
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Release();
            if (leftHand)
                leftHand.Play("Open");
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Catch();
            if (rightHand)
                rightHand.Play("Grab");
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Release();
            if (rightHand)
                rightHand.Play("Open");
        }

        if(dragonDelay)
        {
            fTimeDelay += Time.deltaTime;
            if(fTimeDelay > 0.2f)
            {
                fTimeDelay = 0f;
                dragonDelay = false;
            }
        }

        if (OVRInput.Get(OVRInput.RawButton.X))    //필살기
        {
            if (!dragonDelay)
            {
                if (!isUsingBlackDragon)
                    m_AudioSource.PlayOneShot(m_BlackUseClip);
                isUsingBlackDragon = true;
                BlackDragon();
            }
        }
        else
            isUsingBlackDragon = false;


        if (null != HPImage)
            HPImage.fillAmount = (float)status.iHp / (float)status.iMaxHp;
        if(null != SkillImage)
            SkillImage.fillAmount = (float)status.iGauge / (float)status.iMaxGauge;
        if(null != scoreText)
            scoreText.text = DataManager.Score.ToString();
    }
    #endregion
    #region Function
    void BlackDragon()
    {
        if (0 >= status.iGauge)
            return;

        dragonDelay = true;
        m_AudioSource.PlayOneShot(m_BlackFireClip);
        status.iGauge -= 10;

        GameObject go = Instantiate(m_BlackDragonPrefab);
        Transform tr = go.transform;
        tr.position = m_BlackDragonRoot.position;
        tr.rotation = m_BlackDragonRoot.rotation;
    }

    internal override bool Damaged(int value)
    {
        if (effect != null)
            effect.DamageEffect_On();
        AddGauge((int)(value * 0.33f));

        if (base.Damaged(value))
        {
            DataManager.Instance.isDeath(true);
            SceneManager.LoadScene("Result");
        }

        return true;
    }

    public void AddGauge(int value)
    {
        status.iGauge += value;

        if (100 < status.iGauge)
            status.iGauge = 100;
    }
    #endregion
}