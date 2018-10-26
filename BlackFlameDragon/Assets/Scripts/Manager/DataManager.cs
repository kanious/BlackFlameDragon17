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

public class characterStatus
{
    public int iHp = 0;
    public int iAttack = 0;
    public int iDefence = 0;
    public int iGauge = 0;
    public int iMaxGauge = 0;
    
    public characterStatus() { }
    public characterStatus(int hp, int attack, int defence)
    {
        iHp = hp;
        iAttack = attack;
        iDefence = defence;
        iGauge = 0;
        iMaxGauge = 100;
    }
}

public class weaponStatus
{
    public string sName = "";
    public int iAttack = 0;
    public int iDurability = 0;

    public weaponStatus() { }
    public weaponStatus(string name, int attack, int durability)
    {
        sName = name;
        iAttack = attack;
        iDurability = durability;
    }
}