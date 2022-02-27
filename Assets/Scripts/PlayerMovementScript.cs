using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    public float speed = 10f;
    public PlayerInput playerInput;
    public UIManagerScript UIManager;
    public BunkerScript bunker;
    public bool hasKey = false;
    public Animator animator;
    public float aimSensitivity = 10f;
    public float turnRate = 10f;
    public GameObject playerModel;
    public GameObject interactText, interact2Text, keyText, unlockText, endText;
    public GameObject pauseUI;
    public SoundManagerScript soundManager;
    

    public List<GameObject> items;
    public int[] itemsID = {0,0,0,0 };

    private Vector3 moveVec;
    private Vector3 moveDirection;
    private Vector2 inputVector = Vector2.zero;
    private float lookLeft = 0;
    private bool isInteracting = false;
    private bool isMoving = false;
    
    //private int curItem = 0;


    public void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();      
    }

    public void OnInteract(InputValue input)
    {
        if (animator.GetBool("isDead")) return;
        soundManager.PlayInteractSFX();
        isInteracting = input.isPressed;
        animator.SetTrigger("InteractTrigger");
    }
    public void OnLook(InputValue input)
    {
        //lookLeft = input.Get<float>() ;
        //print(lookLeft);
    }

    // Start is called before the first frame update
    void Start()
    {
        items = new List<GameObject> {null, null, null, null};
        soundManager = FindObjectOfType<SoundManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("isDead"))
            return;

        UpdateItemsID();

        if (!(inputVector.magnitude > 0))
        { 
            moveDirection = Vector3.zero;
            isMoving = false;
        }
        else
        {
            isMoving = true;
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, toRotation, turnRate * Time.deltaTime);
            
            moveDirection = new Vector3( inputVector.x , 0 , inputVector.y);
            Vector3 movementDirection = moveDirection * (speed * Time.deltaTime);
            transform.position += movementDirection;
        }

        animator.SetBool("isMoving", isMoving);
        
    }
   
    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Key") && isInteracting)
        {
            hasKey = true;
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Door") && isInteracting)
        {
            if (hasKey)
            {
                if(!other.gameObject.GetComponent<Animator>().GetBool("isOpened"))
                {
                    hasKey = false;
                    other.gameObject.GetComponent<Animator>().SetBool("isOpened", true);
                    other.gameObject.tag = "Terrain";
                }
            }
            else
                print("no key");
            
        }
        else if (other.CompareTag("Bunker") && isInteracting)
        {
            soundManager.PlayDepositSFX();
            for(int i = 0; i < 4; i++)
            {

                if (items[i] != null)
                {
                    bunker.bunkerItems.Add(items[i]);
                    bunker.UpdateBunker();
                }
                items[i] = null;
            }
        }
        else if (!other.CompareTag("Terrain") && isInteracting)
        {
            if (items[3] == null)
            {
                items.RemoveAt(3);
                items.Insert(0, other.gameObject);
                print("taking food " + items.Count);
                other.gameObject.SetActive(false);


            }
            else
            {
                print("Inv full");
            }
        }

        if (other.CompareTag("Bunker"))
        {
            interact2Text.SetActive(true);
            if(bunker.isEnough)
            endText.SetActive(true);
        }

        if (other.CompareTag("Food") || other.CompareTag("Water") || other.CompareTag("Key"))
        {
            interactText.SetActive(true);
        }

        if (other.CompareTag("Door"))
        {
            if (hasKey)
                unlockText.SetActive(true);
            else
                keyText.SetActive(true);
        }

        if(!(other.CompareTag("Bunker") || other.CompareTag("Food") || other.CompareTag("Water") || other.CompareTag("Key") || other.CompareTag("Door")) && other.CompareTag("Terrain"))
        {
            endText.SetActive(false);
            interactText.SetActive(false);
            interact2Text.SetActive(false);
            keyText.SetActive(false);
            unlockText.SetActive(false);
        }



    }

    public void OnEnd()
    {
        if (bunker.isEnough)
            SceneManager.LoadScene("WinScene");
    }

    public void GameOver()
    {
        //play death anim here
        animator.SetBool("isDead", true);
        print("Blearhg i'm dead");
    }

    public void OnPause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    void UpdateItemsID()
    {
        for(int i = 0; i < 4; i++)
        {
            if (items[i] == null)
            {
                itemsID[i] = 0;
                //return;
            }
            else if (items[i].CompareTag("Food"))
            {
                itemsID[i] = 1;
            }
            else if (items[i].CompareTag("Water"))
            {
                itemsID[i] = 2;
            }
            UIManager.UpdateUI(itemsID);
        }

        
    }
 
}
