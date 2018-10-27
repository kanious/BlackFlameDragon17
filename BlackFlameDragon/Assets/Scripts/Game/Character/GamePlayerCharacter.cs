using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class GamePlayerCharacter : Character
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private Transform m_BlackDragonRoot;
    [SerializeField] private GameCharacterHand m_LeftHand;
    [SerializeField] private GameCharacterHand m_RightHand;
    [Header("Prefab")]
    [SerializeField] private GameObject m_BlackDragonPrefab;
    #endregion

    #region Event
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))    //Right
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Catch();
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            Left_Release();
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger) ||
            OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Catch();
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            Right_Release();
        }


        if (OVRInput.Get(OVRInput.RawButton.X))    //필살기
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