using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance
    {
        get;
        private set;
    }
    #region Inspector
    [Header("Component")]
    [SerializeField] private Canvas m_Canvas;

    [Header("Prefab")]
    [SerializeField] private GameObject m_EffectPrefab;

    [Header("Animation")]
    [SerializeField] private Sprite[] m_BackgroundSprite;
    [SerializeField] private Sprite[] m_TextSprite;
    [SerializeField] private AnimationCurve m_EffectCurve;
    #endregion

    #region Event
    private void Awake()
    {
        instance = this;
    }
    #endregion
    #region Function
    public void SpawnEffect(Vector3 worldPos)
    {
        GameObject go = Instantiate(m_EffectPrefab);
        go.transform.SetParent(transform);
        go.transform.localScale = Vector3.one;
        go.transform.position = worldPos;

        go.GetComponent<Effect>().PlayEffect(m_BackgroundSprite[Random.Range(0, m_BackgroundSprite.Length)], m_TextSprite[Random.Range(0, m_BackgroundSprite.Length)]);
    }
    public float GetEffectCurve(float time)
    {
        return m_EffectCurve.Evaluate(time);
    }
    #endregion
}