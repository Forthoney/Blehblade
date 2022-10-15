using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    //receives a string representing the name of the new scene, and loads it
    public void changeScreens(string newScene)      
    {
        SceneManager.LoadScene(newScene);
    }
}
