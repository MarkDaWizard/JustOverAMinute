using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public PlayerMovementScript player;
    public Image[] image;
    public GameObject keyImage, invFullText;
    public GameObject pauseUI, gameUI, gameOverUI;
    public Sprite foodTex, waterTex, noneTex;
    public TextMeshProUGUI timer;
    public float curTimeMinute, curTimeSeconds;



    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        if(curTimeSeconds <= 9)
            timer.text = curTimeMinute + ":0" + Mathf.RoundToInt(curTimeSeconds);
        else
            timer.text = curTimeMinute + ":" + Mathf.RoundToInt(curTimeSeconds);

        if(curTimeMinute <= 0 && curTimeSeconds <= 30)
        {
            timer.color = Color.red;
        }

        keyImage.SetActive(player.hasKey);
    }

    public void UpdateUI(int[] IDs)
    {
        for(int i = 0; i < 4; i++)
        {
            if (IDs[i] == 1)
            {
                image[i].sprite = foodTex;
            }
            else if (IDs[i] == 2)
            {
                image[i].sprite = waterTex;
            }
            else if (IDs[i] == 0)
            {
                image[i].sprite = noneTex;
            }
            else return;
        }

        if (image[3].sprite != noneTex)
        {
            invFullText.SetActive(true);
        }
        else invFullText.SetActive(false);

    }
    void UpdateTime()
    {
        if(curTimeMinute <= 0 && curTimeSeconds <= 0)
        {
            print("Time's up!");
            player.GameOver();
            GameOver();
            
            return;
        }
        else if (curTimeSeconds <=0)
        {
            curTimeMinute--;
            curTimeSeconds = 59;
        }
        else
        {
            curTimeSeconds -= Time.deltaTime;
        }
    }

    public void OnResumeButtonClick()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GameOver()
    {
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
