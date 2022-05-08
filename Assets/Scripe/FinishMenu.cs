using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishMenu : MonoBehaviour
{
    public GameObject finishMenuUI;

    void Start()
    {
        finishMenuUI.SetActive(false);
    }

    void Update() 
    {
        
    }

   public void MenuFinish()
    {
        finishMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
