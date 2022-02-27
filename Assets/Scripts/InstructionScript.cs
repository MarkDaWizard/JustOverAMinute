using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InstructionScript : MonoBehaviour
{
    public GameObject[] pages;
    public int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPreviousButtonClick()
    {
        if (index == 0) return;
        index--;
        pages[index].SetActive(true);
        pages[index+1].SetActive(false);
    }

    public void OnNextButtonClick()
    {
        if (index == 5) return;
        index++;
        pages[index].SetActive(true);
        pages[index - 1].SetActive(false);
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
