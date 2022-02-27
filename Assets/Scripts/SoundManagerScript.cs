using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour
{
    private string currentScene = "MainMenuScene";

    public AudioSource menuMusic, overworldMusic, victoryMusic;
    public AudioSource interactSFX, depositSFX;
    //Check to see if there's only 1 Manager
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        //mixer = GetComponent<AudioMixer>();

        DontDestroyOnLoad(this.gameObject);
    }

    //Called when a scene is loaded
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        currentScene = scene.name;

        //StopAllMusic();
        PlayBGM();
    }
    private List<AudioSource> allAudioSources;
    void StopAllMusic()
    {

        allAudioSources = new List<AudioSource>(FindObjectsOfType(typeof(AudioSource)) as AudioSource[]);
        allAudioSources.Remove(interactSFX);
        allAudioSources.Remove(depositSFX);
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    //Play a music track depending on current scene
    public void PlayBGM()
    {
        switch (currentScene)
        {
            case ("MainMenuScene"):
                Debug.Log("menu");
                if(victoryMusic.isPlaying)
                StopAllMusic();
                if (!menuMusic.isPlaying)
                    menuMusic.Play();
                break;
            case ("CreditsScene"):
                Debug.Log("menu");
                if (!menuMusic.isPlaying)
                    menuMusic.Play();
                break;
            case ("InstructionScene"):
                Debug.Log("menu");
                if (!menuMusic.isPlaying)
                    menuMusic.Play();
                break;
            case "MainScene":
                Debug.Log("lvl");
                StopAllMusic();
                overworldMusic.Play();
                break;
            case "WinScene":
                Debug.Log("win");
                StopAllMusic();
                victoryMusic.Play();
                break;
            default:
                Debug.Log("Invalid Scene");
                break;
        }

    }

    public void PlayInteractSFX()
    {
        if(!interactSFX.isPlaying)
        interactSFX.Play();
    }

    public void PlayDepositSFX()
    {
        if(!depositSFX.isPlaying)
        depositSFX.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
