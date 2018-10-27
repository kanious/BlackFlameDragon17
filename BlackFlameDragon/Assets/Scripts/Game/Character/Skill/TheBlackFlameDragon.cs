using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBlackFlameDragon : MonoBehaviour
{
    #region Inspector
    [Header("Balance")]
    [SerializeField] private int m_Damage;
    [SerializeField] private float m_Speed;
    [SerializeField] private float m_MaxMove;
    #endregion
    #region Value
    private float m_Move;
    #endregion

    void Update()
    {
        Vector3 move = transform.forward * m_Speed * Time.deltaTime;
        transform.position += move;
        m_Move += move.magnitude;

        if (m_MaxMove <= m_Move)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.attachedRigidbody)
        {
            Character character = other.attachedRigidbody.GetComponent<GameEnemyCharacter>();
            character.Damaged(m_Damage);
        }
    }
}