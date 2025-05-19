using System;
using UnityEngine.Audio;
using UnityEngine;
using System.Globalization;

public class AudioManager : MonoBehaviour
{
    /*
    Title: Introduction to AUDIO in Unity
    Author: Asbjørn Thirslund / Brackeys
    Date: 19 April 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=6OT43pvUyfY&t=620s
    */

    public bool isStartMenu;

    public Sound[] sounds;//the [] allows for lists

    public static AudioManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()//same as start method, exept "its called before"?
    {
        /*
        if (instance == null)//if the audiomanager already exists in the next scene, its deleted so there arent 2 at any one time
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        
        DontDestroyOnLoad(gameObject);//remains even if you have a scene transition, could be used for the player too
        */

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        //Play("background");
    }

    private void Start()
    {
        if (isStartMenu)
        {
            Play("background");//plays the theme song at the start of the game
        }
    }

    public void Play(string name)//string name will find sound with appropriate name
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

        if (s == null)
        {
            Debug.Log("sound" + name + "is missing");
        }
            return;
    }

    public void PlayForButton(string name)//string name will find sound with appropriate name
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.clip, 1f);

        if (s == null)
        {
            Debug.Log("sound" + name + "is missing");
        }
        return;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);//all this is doing is fingint the sound by the name given
        s.source.Stop();

        if (s == null)
        {
            Debug.Log("sound" + name + "is missing");
        }
        return;
    }
}
