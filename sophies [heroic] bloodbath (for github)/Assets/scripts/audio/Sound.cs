using UnityEngine;

[System.Serializable]
public class Sound
{
    /*
    Title: Introduction to AUDIO in Unity
    Author: Asbj�rn Thirslund / Brackeys
    Date: 19 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=6OT43pvUyfY&t=620s
    */

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]//this is how you get sliders in the inspector
    public float volume;
    [Range(.1f, 3f)]//same, this is for sliders in the inspector
    public float pitch;

    [HideInInspector]//self explanatory
    public AudioSource source;

    public bool loop;
}
