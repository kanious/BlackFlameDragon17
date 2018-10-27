using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class GamePlayerCharacter : Character
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private Transform m_BlackDragonRoot;
    [Header("Prefab")]
    [SerializeField] private GameObject m_BlackDragonPrefab;
    #endregion

    #region Event
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Attack();
        else if (Input.GetKeyDown(KeyCode.Space))            
        {
            if (status.RightHand.catchingObject)
                Release();
            else
                Catch();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Throw();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            BlackDragon();
        }
    }
    #endregion
    #region Function
    void BlackDragon()
    {
        GameObject go = Instantiate(m_BlackDragonPrefab);
        Transform tr = go.transform;
        tr.position = m_BlackDragonRoot.position;
        tr.rotation = m_BlackDragonRoot.rotation;
    }
    #endregion
}