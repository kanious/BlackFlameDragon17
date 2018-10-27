using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PackageProject.SpecialHelper.CatchHand
{
    /// <summary>
    /// 캐릭터의 손, 잡기/잡기 감지 기능 관련 클래스
    /// </summary>
    public class GameCharacterHand : MonoBehaviour
    {
        #region Type
        public enum CatchOptionEnum
        {
            /// <summary>
            /// 기본값, 다른손이 잡고있을 시 잡을수 없음
            /// </summary>
            Normal,
            /// <summary>
            /// 다른손이 잡고있을 시 뺏어서 잡음
            /// </summary>
            Steal,
            /// <summary>
            /// 다른손이 잡고있더라도 잡음(같이잡음)
            /// </summary>
            With
        }
        #endregion

        #region Inspector
        [Header("Component")]
        [SerializeField] private Transform m_HandTransform;
        #endregion
        #region Const
        private const int valCatchEnableListMax = 10;   //m_CatchEnableList 배열 사이즈
        #endregion
        #region Value
        private GameCatchObject[] m_CatchEnableList = new GameCatchObject[valCatchEnableListMax];   //잡을 수 있는 오브젝트 리스트
        #endregion
        #region Get,Set
        /// <summary>
        /// 현재 해당 손이 잡을 수 있는 오브젝트
        /// </summary>
        /// <value>잡을 수 있는 오브젝트</value>
        public GameCatchObject catchEnableObject
        {
            get
            {
                for (int i = 0; i < m_CatchEnableList.Length; ++i)
                {
                    if (m_CatchEnableList[i] != null)
                        return m_CatchEnableList[i];
                }

                return null;
            }
        }
        /// <summary>
        /// 현재 해당 손이 잡고 있는 오브젝트
        /// </summary>
        /// <value>잡</value>
        public GameCatchObject catchingObject
        {
            get;
            private set;
        }
        #endregion

        #region Event
        private void OnTriggerEnter(Collider other)
        {
            //잡을 수 있는 오브젝트 리스트에 등록
            if (other.attachedRigidbody)
            {
                GameCatchObject catchObject = other.attachedRigidbody.GetComponent<GameCatchObject>();
                if (catchObject)
                {
                    for (int i = 0; i < m_CatchEnableList.Length; ++i)
                    {
                        if (m_CatchEnableList[i] == null)
                        {
                            m_CatchEnableList[i] = catchObject;
                            break;
                        }
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            //잡을 수 있는 오브젝트 리스트에서 삭제
            if (other.attachedRigidbody)
            {
                GameCatchObject catchObject = other.attachedRigidbody.GetComponent<GameCatchObject>();
                if (catchObject == catchEnableObject)
                {
                    for (int i = 0; i < m_CatchEnableList.Length; ++i)
                    {
                        if (m_CatchEnableList[i] == catchEnableObject)
                        {
                            m_CatchEnableList[i] = null;
                            break;
                        }
                    }
                }
            }
        }
        #endregion
        #region Function
        //Public
        /// <summary>
        /// 현재 잡을 수 있는 오브젝트를 잡습니다.
        /// </summary>
        /// <param name="isParentChange">잡은 오브젝트의 transform.parent를 손의 transform으로 변경할지 여부</param>
        /// <returns>잡기에 성공했는지 여부</returns>
        public bool Catch(bool isParentChange = true, CatchOptionEnum catchOption = CatchOptionEnum.Normal)
        {
            if (catchEnableObject)
                return Catch(catchEnableObject, isParentChange, catchOption);
            else
                return false;
        }
        public bool Catch(GameCatchObject catchObject, bool isParentChange = true, CatchOptionEnum catchOption = CatchOptionEnum.Normal)
        {
            //옵션에 따른 처리
            switch (catchOption)
            {
                case CatchOptionEnum.Normal:
                    if (0 < catchObject.catchingHandCount)
                        return false;
                    break;
                case CatchOptionEnum.Steal:
                    catchObject.ReleaseAllForce();
                    break;
            }

            //오브젝트 실제 잡기
            if (catchObject.Catch(this))
            {
                catchingObject = catchObject;

                if (isParentChange)
                {
                    Transform tr = catchingObject.transform;
                    tr.parent = m_HandTransform;
                    tr.localPosition = Vector3.zero;
                    tr.localRotation = Quaternion.identity;
                }

                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// 현재 잡고 있는 오브젝트를 떨어뜨립니다.
        /// </summary>
        public void Release()
        {
            if(catchingObject)
            {
                catchingObject.Release(this);
                catchingObject = null;
            }
        }
        #endregion
    }
}