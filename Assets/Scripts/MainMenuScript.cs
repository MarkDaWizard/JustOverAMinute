using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnInstructionButtonClick()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    public void OnCreditsButtonClick()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
