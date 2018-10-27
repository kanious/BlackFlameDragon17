using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {

    private void Awake()
    {
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
