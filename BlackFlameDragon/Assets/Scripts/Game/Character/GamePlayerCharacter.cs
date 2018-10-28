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

    #region Event
    protected override void Awake()
    {
        base.Awake();

        status.iHp = 1000;
        status.iMaxHp = 1000;
        status.iGauge = 0;
        status.iMaxGauge = 100;
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))    //Right
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Catch();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Release();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Catch();
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Release();
        }


        if (OVRInput.Get(OVRInput.RawButton.X))    //필살기
        {
            if (!isUsingBlackDragon)
                m_AudioSource.PlayOneShot(m_BlackUseClip);
            isUsingBlackDragon = true;
            BlackDragon();
        }
        
        if(null != HPImage)
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
        m_AudioSource.PlayOneShot(m_BlackFireClip);
        status.iGauge -= 1;

        GameObject go = Instantiate(m_BlackDragonPrefab);
        Transform tr = go.transform;
        tr.position = m_BlackDragonRoot.position;
        tr.rotation = m_BlackDragonRoot.rotation;
    }

    internal override bool Damaged(int value)
    {
        effect.DamageEffect_On();

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