using UnityEngine;

public class CursorManagerScript : MonoBehaviour
{
    /*
    Title: Custom Cursors In Unity
    Author: bendux
    Date: 06 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=ELhg7ge2rIA
    */

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotSpot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
