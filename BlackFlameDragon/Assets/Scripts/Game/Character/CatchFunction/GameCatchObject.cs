using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackageProject.SpecialHelper.CatchHand
{
    /// <summary>
    /// 잡을 수 있는 오브젝트 클래스
    /// </summary>
    public abstract class GameCatchObject : MonoBehaviour
    {
        #region Inspector
        #endregion
        #region Const
        private const int valCatchingHandListSize = 10; //m_CatchingHand 배열 사이즈
        #endregion
        #region Value
        private GameCharacterHand[] m_CatchingHand = new GameCharacterHand[valCatchingHandListSize];    //해당 오브젝트를 잡고 있는 손 리스트
        #endregion
        #region Get,Set
        /// <summary>
        /// 해당 오브젝트르 잡고 있는 손 갯수
        /// </summary>
        /// <value>해당 오브젝트르 잡고 있는 손 갯수</value>
        public int catchingHandCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < m_CatchingHand.Length;++i)
                {
                    if (m_CatchingHand[i])
                        ++count;
                }

                return count;
            }
        }
        #endregion

        #region Event
        /// <summary>
        /// 잡기 되었을 때 호출되는 이벤트입니다.
        /// <paramref name="catchingHandLeft">해당 오브젝트를 잡고있는 손의 수입니다.</paramref>
        /// </summary>
        internal abstract void OnCatched();
        /// <summary>
        /// 잡기 해제되었을 때 호출되는 이벤트입니다.
        /// <paramref name="catchingHandLeft">남아있는 해당 오브젝트를 잡고있는 손의 수입니다.</paramref>
        /// </summary>
        internal abstract void OnReleased();
        #endregion
        #region Function
        //Public
        /// <summary>
        /// 오브젝트를 잡고있는 손들에서 강제로 오브젝트를 떨어뜨립니다.
        /// </summary>
        public void ReleaseAllForce()
        {
            for (int i = 0; i < m_CatchingHand.Length;++i)
            {
                if (m_CatchingHand[i])
                    m_CatchingHand[i].Release();
            }
        }

        //Internal, GameCharacterHand에서 호출해야 하는 함수
        internal bool Catch(GameCharacterHand hand)
        {
            for (int i = 0; i < m_CatchingHand.Length;++i)
            {
                if(m_CatchingHand[i] == null)
                {
                    m_CatchingHand[i] = hand;
                    OnCatched();
                    return true;
                }
            }

            return false;
        }
        internal void Release(GameCharacterHand hand)
        {
            for (int i = 0; i < m_CatchingHand.Length;++i)
            {
                if(m_CatchingHand[i] == hand)
                {
                    m_CatchingHand[i] = null;
                    OnReleased();
                    break;
                }
            }
        }
        #endregion
    }
}