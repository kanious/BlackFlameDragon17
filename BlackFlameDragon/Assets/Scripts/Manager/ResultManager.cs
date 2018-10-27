using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    public GameObject timeoutObj;
    public GameObject deathObj;

    private void Awake()
    {
        if(DataManager.Death)
        {
            // Death로 인한 게임종료 시 처리
            timeoutObj.SetActive(false);
            deathObj.SetActive(true);

            deathObj.transform.Find("Text_no").GetComponent<Text>().text = DataManager.Score + "명";
        }
        else
        {
            // 타임종료로 인한 게임종료 시 처리
            timeoutObj.SetActive(true);
            deathObj.SetActive(false);

            timeoutObj.transform.Find("Text_no").GetComponent<Text>().text = DataManager.Score + "명";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Intro");
    }
}
