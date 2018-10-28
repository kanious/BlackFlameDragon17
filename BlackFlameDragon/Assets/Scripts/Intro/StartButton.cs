using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : Character
{
    #region Inspector
    [SerializeField] private string m_SceneName;
    [SerializeField] private AnimationCurve m_Curve;
    [SerializeField] private AudioClip[] m_ClickClip;
    #endregion
    #region Value
    Coroutine coroutine;
    private float m_StartDisableTimer = 2.0f;
    #endregion

    private void Update()
    {
        m_StartDisableTimer -= Time.deltaTime;
    }
    internal override bool Damaged(int value)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Coroutine());
            m_AudioSource.PlayOneShot(m_ClickClip[Random.Range(0, m_ClickClip.Length)]);
        }
        return true;
    }

    IEnumerator Coroutine()
    {
        float timer = 0;
        while(true)
        {
            timer += Time.deltaTime * 4;
            transform.localScale = Vector3.one * (1 + m_Curve.Evaluate(timer));

            if (timer < 1.0)
                yield return null;
            else
                break;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(m_SceneName);
    }
}