using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance = null;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;

        DontDestroyOnLoad(Instance);

        Score = 0;
        Death = false;
    }

    public static int Score = 0;
    public static bool Death = false;
    public void AddScore(int value)
    {
        Score += value;
    }
    public void isDeath(bool _death)
    {
        Death = _death;
    }
}

[System.Serializable]
public struct characterStatus
{
    public int iHp;
    public int iMaxHp;
    public int iAttack;
    public int iDefence;
    public int iGauge;
    public int iMaxGauge;
    public float fSpeed;
    public GameCharacterHand RightHand;
    public GameCharacterHand LeftHand;

    //public characterStatus(int hp, int attack, int defence, float speed)
    //{
    //    iHp = hp;
    //    iAttack = attack;
    //    iDefence = defence;
    //    iGauge = 0;
    //    iMaxGauge = 100;
    //    fSpeed = speed;

    //    RightHand = null;
    //}
}

[System.Serializable]
public struct weaponStatus
{
    public string sName;
    public int iAttack;
    public int iDurability;
    
    //public weaponStatus(string name, int attack, int durability)
    //{
    //    sName = name;
    //    iAttack = attack;
    //    iDurability = durability;
    //}
}