using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class DataManager {

    //public static DataManager Instance = null;

    //private void Awake()
    //{
    //    if (null == Instance)
    //        Instance = new DataManager();

    //    DontDestroyOnLoad(Instance);
    //}

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