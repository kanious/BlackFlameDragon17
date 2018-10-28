using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Inspector
    [Header("Balance - Attack")]    //공격 관련 밸런스 변수
    [SerializeField] private float m_WeaponDamageMinSpeed;          //해당 값 이하의 속도인 무기는 공격력 배율이 0이다.
    [SerializeField] private float m_WeaponDamageStandardSpeed;     //해당 값의 속도 이상인 무기는 공격력 배율이 1이다.

    [Header("AudioEffect")]
    [SerializeField] public AudioClip[] m_AttackEffect;    //타격시 효과음(적/플레이어 공유)
    #endregion

    public float fProgressTime = 0f;
    public Transform Player;
    public Text CountingText;
    float remainTime = 60f;
    
    public static GameManager Instance = null;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;

        remainTime = 60f;
        CountingText.text = remainTime.ToString();
    }

    private void Update()
    {
        fProgressTime += Time.deltaTime;
        remainTime -= Time.deltaTime;

        CountingText.text = remainTime.ToString("00");

        if(0f >= remainTime)
        {
            DataManager.Instance.isDeath(false);
            SceneManager.LoadScene("Result");
        }
    }

    public void Dist_Check()
    {

    }

    public bool Collision(Transform me, Transform you)
    {
        // 충돌 시 true 반환

        return false;
    }

    #region Function
    public float GetDamageFactor(float speed)
    {
        return Mathf.InverseLerp(m_WeaponDamageMinSpeed, m_WeaponDamageStandardSpeed, speed);
    }
    #endregion
}