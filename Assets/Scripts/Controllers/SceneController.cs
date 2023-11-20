using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string m_SceneActuel;
    public void OnChangeToNextScene()
    {
        switch (m_SceneActuel)
        {
            case GameParametres.SceneName.SCENE_1:
                OnChangeScene(GameParametres.SceneName.SCENE_2);
                break;
            case GameParametres.SceneName.SCENE_2:
                OnChangeScene(GameParametres.SceneName.SCENE_3);
                break;
            case GameParametres.SceneName.SCENE_3:
                OnChangeScene(GameParametres.SceneName.SCENE_4);
                break;
            case GameParametres.SceneName.SCENE_4:
                OnChangeScene(GameParametres.SceneName.SCENE_5);
                break;
            case GameParametres.SceneName.SCENE_5:
            default:
                OnChangeScene(GameParametres.SceneName.SCENE_1);
                break;
        }
    }

    public void OnChangeToPreviusScene()
    {
        switch (m_SceneActuel)
        {
            case GameParametres.SceneName.SCENE_1:
                OnChangeScene(GameParametres.SceneName.SCENE_5);
                break;
            case GameParametres.SceneName.SCENE_2:
                OnChangeScene(GameParametres.SceneName.SCENE_1);
                break;
            case GameParametres.SceneName.SCENE_3:
                OnChangeScene(GameParametres.SceneName.SCENE_2);
                break;
            case GameParametres.SceneName.SCENE_4:
                OnChangeScene(GameParametres.SceneName.SCENE_3);
                break;
            case GameParametres.SceneName.SCENE_5:
            default:
                OnChangeScene(GameParametres.SceneName.SCENE_4);
                break;
        }
    }

    public void OnRefersh()
    {
        OnChangeScene(m_SceneActuel);
    }

    private void OnChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
