using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            changeScreens("PrototypeStage");
        } else if (Input.GetKeyDown("f"))
        {
            changeScreens("SampleScene");
        }
        
    }

    public void changeScreens(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
