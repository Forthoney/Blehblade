using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame (){
        SceneManager.LoadScene(1);

    }
    public void QuitGame(){
        Application.Quit();

    }
    public void Credits(){
        SceneManager.LoadScene("credits");
    }
    // Start is called before the first frame update
    void Start()
    {
        // Play main menu music
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
