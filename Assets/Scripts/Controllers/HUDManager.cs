using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject m_MenuGameOver;

    private void Start()
    {
        ResumeGame();
        m_MenuGameOver.SetActive(false);
    }

    public void ShowGameOver()
    {
        PauseGame();
        m_MenuGameOver.SetActive(true);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
