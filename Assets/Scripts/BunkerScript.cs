using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BunkerScript : MonoBehaviour
{
    public List<GameObject> bunkerItems;
    public int totalScore = 0;
    public TextMeshProUGUI foodCounter, waterCounter;
    public bool isEnough = false;

    private int curFood = 0, curWater = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foodCounter.text = curFood + "/20"; 
        waterCounter.text = curWater + "/25"; 
        if(curFood >= 20 && curWater >= 25)
        {
            isEnough = true;
        }
    }

    public void TallyScore()
    {
        int limit = bunkerItems.Count;
        for (int i = 0; i < limit; i++)
        {
            if(bunkerItems[i].CompareTag("Food"))
            {
                totalScore += 100;
            }
            else if (bunkerItems[i].CompareTag("Water"))
            {
                totalScore += 200;
            }

        }
    }

    public void UpdateBunker()
    {
        int food = 0, water = 0;
        foreach(GameObject item in bunkerItems)
        {
            if (item.CompareTag("Food"))
                food++;
            else
                water++;
        }
        curFood = food;
        curWater = water;
    }
}
