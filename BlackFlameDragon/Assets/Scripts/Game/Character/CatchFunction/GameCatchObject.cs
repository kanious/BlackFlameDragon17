using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackageProject.SpecialHelper.CatchHand
{
    public class GameCatchObject : MonoBehaviour
    {
        #region Inspector
        #endregion
        #region Const
        private const int valCatchingHandList = 10;
        #endregion
        #region Value
        private GameCharacterHand[] m_CatchingHand = new GameCharacterHand[valCatchingHandList];
        #endregion
    }
}