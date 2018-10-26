using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public int iHp = 0;
    public int iMaxHp = 0;

    public int iAttack = 0;
    public int iDefence = 0;

    public int iGauge = 0;
    public int iMaxGauge = 0;

    /// <summary>
    /// 데미지 입을 시 수행, 남은 체력이 0이면 true 반환, 아니면 false
    /// </summary>
    public bool Damaged(int value)
    {
        iHp -= value;

        if (0 >= iHp)
            return true;

        return false;
    }

}