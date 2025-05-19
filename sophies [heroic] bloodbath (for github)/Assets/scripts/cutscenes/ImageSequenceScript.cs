using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageSequenceScript : MonoBehaviour
{
    /*
    Title: How to change UI image Array with button in Unity | Unity Tutorial
    Author: Grafik Games
    Date: 14 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=UQ7FjIwbJsA&list=WL&index=3
    */

    [Header("gameObjects")]
    public GameObject[] background;

    [Header("previous buttons")]
    public GameObject previousButton;
    public GameObject previousButtonOFF;

    [Header("next buttons")]
    public GameObject nextButton;
    public GameObject nextButtonOFF;

    [Header("continue buttons")]
    public GameObject nextLevelButton;
    public GameObject nextLevelButtonOFF;

    private int indexAmount;
    private int index;

    private void Awake()
    {
        indexAmount = background.Length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= indexAmount)
        {
            index = indexAmount;//resets the index when its been completed
        }

        if (index < 0)
        {
            index = 0;//sets it to 0, thus ensuring it doesnt enter the negatuve digits
        }

        if (index == 1)//if it is the first image, then the previous button is disabled
        {
            previousButton.SetActive(true);
            previousButtonOFF.SetActive(false);
        }
        else if (index == 0)
        {
            previousButton.SetActive(false);
            previousButtonOFF.SetActive(true);
        }

        if (index == indexAmount - 1)//if it is the last image in the sequence, then the next button is disabled, and the continue button, enabled
        {
            nextButton.SetActive(false);
            nextLevelButton.SetActive(true);
            nextLevelButtonOFF.SetActive(false);
            nextButtonOFF.SetActive(true);
        }
        else if (index < indexAmount)
        {
            nextButton.SetActive(true);
            nextButton.SetActive(true);
            nextLevelButtonOFF.SetActive(true);
            nextLevelButton.SetActive(false);
            nextButtonOFF.SetActive(false);
        }

        if (index == 0)
        {
            background[0].gameObject.SetActive(true);//if the index is at 0, return to the first image
        }
    }

    public void Next()
    {
        index += 1;//add 1 to the index

        for (int i = 0; i < background.Length; i++)//if [i] is less than the max number of images in the index, add 1 to it
        {
            background[i].gameObject.SetActive(false);//turns off current image, and enables the next
            background[index].gameObject.SetActive(true);//enables the next in index, after 1 is added to current
        }
        //Debug.Log(index);
    }

    public void Previous()
    {
        index -= 1;//does the same as the above but in reverse, this time, subtracting 1 to it

        for (int i = 0; i < background.Length; i++)
        {
            background[i].gameObject.SetActive(false);
            background[index].gameObject.SetActive(true);
        }
        //Debug.Log(index);
    }
}
