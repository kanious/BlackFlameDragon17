using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    public GameObject timeoutObj;
    public GameObject deathObj;

    [Header("Audio")]
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_TimeOutClip;
    [SerializeField] private AudioClip m_DeathClip;

    private void Awake()
    {
        if(DataManager.Death)
        {
            // Death로 인한 게임종료 시 처리
            m_AudioSource.clip = m_DeathClip;
            timeoutObj.SetActive(false);
            deathObj.SetActive(true);

            deathObj.transform.Find("Text_no").GetComponent<Text>().text = DataManager.Score + "명";
        }
        else
        {
            // 타임종료로 인한 게임종료 시 처리
            m_AudioSource.clip = m_TimeOutClip;
            timeoutObj.SetActive(true);
            deathObj.SetActive(false);

            timeoutObj.transform.Find("Text_no").GetComponent<Text>().text = DataManager.Score + "명";
        }
        m_AudioSource.Play();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Intro");
    }
}
