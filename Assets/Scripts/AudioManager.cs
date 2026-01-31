using UnityEngine;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioClip LuteC3, LuteD3, LuteE3, LuteF3, LuteG3, LuteA3, LuteA13, LuteC4;
    public AudioClip page1, page2;
    public AudioClip mg1, mg2;
    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    public AudioSource musicSource, sfxSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //define sound library
        soundLibrary.Add("LuteC3", LuteC3);
        soundLibrary.Add("LuteD3", LuteD3);
        soundLibrary.Add("LuteE3", LuteE3);
        soundLibrary.Add("LuteF3", LuteF3);
        soundLibrary.Add("LuteG3", LuteG3);
        soundLibrary.Add("LuteA3", LuteA3);
        soundLibrary.Add("LuteA13", LuteA13);
        soundLibrary.Add("LuteC4", LuteC4);
        soundLibrary.Add("page1", page1);
        soundLibrary.Add("page2", page2);
        soundLibrary.Add("Minigame_Theme_1", mg1);
        soundLibrary.Add("Minigame_Theme_2", mg2);

        //set default music
        SetMusic("Minigame_Theme_1");
    }
    public void SetMusic(string clipKey)
    {
        musicSource.clip = soundLibrary[clipKey];
        musicSource.Play();
    }
    public void PlaySoundEffect(string clipKey)
    {
        AudioClip toPlay;
        try
        {
            toPlay = soundLibrary[clipKey];
        }
        catch (Exception ex)
        {
            //play the lute if the sound doesnt exist
            toPlay = soundLibrary["LuteC3"];
        }
        sfxSource.PlayOneShot(toPlay);
    }
}
