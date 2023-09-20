using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public string sceneToLoad;

    private void Start()
    {




    }


    public void OnStartButtonClick()
    {

    

    SceneManager.LoadScene(1);
    }
}