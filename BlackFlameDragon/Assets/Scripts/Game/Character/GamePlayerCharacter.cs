using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class GamePlayerCharacter : Character
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private GameCharacterHand m_RightHand;
    #endregion

    private void Update()
    {
        if (m_RightHand.catchingObject)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                m_RightHand.Release();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
                m_RightHand.Catch();
        }
    }
}