using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraHolder.transform.Rotate(0,10 * Time.deltaTime,0);
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
