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
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Move(move);

        if (Input.GetKey(KeyCode.Q))
            transform.localEulerAngles += Vector3.up * -50 * Time.deltaTime;
        else if (Input.GetKey(KeyCode.E))
            transform.localEulerAngles += Vector3.up * 50 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z))
            Attack();
        else if (Input.GetKeyDown(KeyCode.Space))            
        {
            if (status.RightHand.catchingObject)
                Release();
            else
                Catch();
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            Throw();
        }
        else if(Input.GetKeyDown(KeyCode.C))
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