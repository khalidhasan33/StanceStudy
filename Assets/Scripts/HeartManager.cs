using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite HalfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        IntHeart();   
    }

    public void IntHeart()
    {
        for(int i = 0; i< heartContainers.intialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    // Update is called once per frame
    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.RunTimeValue / 2;
        for(int i = 0; i < heartContainers.intialValue; i++)
        {
            if (i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if(i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = HalfFullHeart;
            }
        }
    }
}
