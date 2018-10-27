using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PackageProject.SpecialHelper.CatchHand
{
    /// <summary>
    /// 잡을 수 있는 오브젝트 클래스
    /// </summary>
    public class GameCatchObject : MonoBehaviour
    {
        #region Inspector
        [Header("Component")]
        [SerializeField] private Rigidbody m_Rigidbody;
        #endregion
        #region Const
        private const int valCatchingHandListSize = 10; //m_CatchingHand 배열 사이즈
        #endregion
        #region Value
        private GameCharacterHand[] m_CatchingHand = new GameCharacterHand[valCatchingHandListSize];    //해당 오브젝트를 잡고 있는 손 리스트
        #endregion
        #region Get,Set
        /// <summary>
        /// 해당 오브젝트를 잡고 있는 손 갯수
        /// </summary>
        /// <value>해당 오브젝트를 잡고 있는 손 갯수</value>
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
        /// <summary>
        /// 사용하고 있는 리지드바디
        /// </summary>
        /// <value>사용하고 있는 리지드바디</value>
        public Rigidbody objectRigidbody
        {
            get
            {
                return m_Rigidbody;
            }
        }

        //Action
        /// <summary>
        /// 어떤 손에 의해 해당 오브젝트가 잡혔을 때 호출되는 이벤트
        /// </summary>
        public Action onCatched;
        /// <summary>
        /// 원래 잡혀있다가 놓아졌을 때 호출되는 이벤트
        /// 주의사항 : 해당 이벤트가 호출되었더라도 모든 손이 놓은것은 아닐수 있음(catchingHandCount값 참고)
        /// </summary>
        public Action onReleased;
        #endregion

        #region Event
        /// <summary>
        /// 어떤 손에 의해 해당 오브젝트가 잡혔을 때 호출되는 이벤트
        /// </summary>
        protected virtual void OnCatched()
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.velocity = Vector3.zero;

            if (onCatched != null)
                onCatched();
        }
        /// <summary>
        /// 원래 잡혀있다가 놓아졌을 때 호출되는 이벤트
        /// 주의사항 : 해당 이벤트가 호출되었더라도 모든 손이 놓은것은 아닐수 있음(catchingHandCount값 참고)
        /// </summary>
        protected virtual void OnReleased()
        {
            if (catchingHandCount == 0)
                m_Rigidbody.isKinematic = false;

            if (onReleased != null)
                onReleased();
        }
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