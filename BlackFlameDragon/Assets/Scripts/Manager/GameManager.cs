using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector
    #endregion

    public static GameManager Instance = null;

    private void Awake()
    {
        if (null == Instance)
            Instance = new GameManager();

        DontDestroyOnLoad(Instance);
    }

    public void Dist_Check()
    {

    }

    public bool Collision(Transform me, Transform you)
    {
        // 충돌 시 true 반환

        return false;
    }

}