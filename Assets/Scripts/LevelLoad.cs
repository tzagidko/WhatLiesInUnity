using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }public void LoadLevel(){
        SceneManager.LoadScene("FirstLevel");
        Time.timeScale = 1.0f;
    }

}
