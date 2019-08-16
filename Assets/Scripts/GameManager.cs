using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public void NextScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(activeScene.buildIndex + 1);

    }
        

    public void ResetLevel()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

}
