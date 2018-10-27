using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector
    [Header("Balance - Attack")]    //공격 관련 밸런스 변수
    [SerializeField] private float m_WeaponDamageMinSpeed;          //해당 값 이하의 속도인 무기는 공격력 배율이 0이다.
    [SerializeField] private float m_WeaponDamageStandardSpeed;     //해당 값의 속도 이상인 무기는 공격력 배율이 1이다.
    #endregion

    public float fProgressTime = 0f;
    public GamePlayerCharacter Player;

    public static GameManager Instance = null;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;
    }

    private void Update()
    {
        fProgressTime += Time.deltaTime;
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