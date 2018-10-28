using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    #region Inspector
    [Header("Component")]
    [SerializeField] private Image m_Background;
    [SerializeField] private Image m_Text;
    #endregion

    #region Event
    public void PlayEffect(Sprite background, Sprite text)
    {
        m_Background.sprite = background;
        m_Background.SetNativeSize();
        m_Text.sprite = text;
        m_Text.SetNativeSize();
        transform.localEulerAngles = new Vector3(0, 0, Random.Range(-45, 45));
        //transform.LookAt(GameManager.Instance.Player);

        StartCoroutine(EffectCoroutine());
    }
    #endregion
    #region Function
    private IEnumerator EffectCoroutine()
    {
        float timer = 0;
        while(true)
        {
            timer += Time.deltaTime * 2;
            transform.localScale = Vector3.one * EffectManager.instance.GetEffectCurve(timer);

            if (timer < 1.0f)
                yield return null;
            else
                break;
        }

        Destroy(gameObject);
    }
    #endregion
}