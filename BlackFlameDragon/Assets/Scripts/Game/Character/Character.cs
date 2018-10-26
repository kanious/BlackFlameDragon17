using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    characterStatus status;
    
    /// <summary>
    /// 데미지 입을 시 수행, 남은 체력이 0이면 true 반환, 아니면 false
    /// </summary>
    public bool Damaged(int value)
    {
        status.iHp -= value;

        if (0 >= status.iHp)
            return true;

        return false;
    }

}