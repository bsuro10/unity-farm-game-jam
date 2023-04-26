using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadFirstScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
