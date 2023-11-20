using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnChangeScene1()
    {
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_1);
    }

    public void OnChangeScene2()
    {
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_2);
    }

    public void OnChangeScene3()
    {
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_3);
    }

    public void OnChangeScene4()
    {
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_4);
    }

    public void OnChangeScene5()
    {
        SceneManager.LoadScene(GameParametres.SceneName.SCENE_5);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
