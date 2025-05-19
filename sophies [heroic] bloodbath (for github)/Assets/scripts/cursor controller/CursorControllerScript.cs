using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorControllerScript : MonoBehaviour
{
    /*
    Title: Custom Cursor with Input System - Unity Tutorial
    Author: samyam
    Date: 17 May 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=PTL19bXCsNU&t=7s
    */

    public Texture2D cursor;
    public Texture2D cursorClicked;

    private Cursorcontroller controls;

    private CursorControllerScript cursorControllerScript;

    private void Awake()
    {
        controls = new Cursorcontroller();

        ChangeCursor(cursor);
        //Cursor.lockState = CursorLockMode.Confined;

        /*
        if (cursorControllerScript == null)
        {
            cursorControllerScript = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);//as a new scene is loaded, the gameobject this script is attached to will follow, and if the object already exists, then this (now duplicate) is deleted
        }
        */
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controls.mouse.click.started += _ => StartedClick();//???
        controls.mouse.click.performed += _ => EndedClick();//???
    }


    private void StartedClick()
    {
            ChangeCursor(cursorClicked);
    }


    private void EndedClick()
    {
            ChangeCursor(cursor);
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 4); //this makes it so that the curosr checks whats clicked, from the center and not the top left, as with standard cursors
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto );
    }

    // Update is called once per frame
    void Update()
    {

    }
}
