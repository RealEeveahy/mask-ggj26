using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioClip LuteC3, LuteD3, LuteE3, LuteF3, LuteG3, LuteA3, LuteA13, LuteC4;
    public AudioClip page1, page2;
    public AudioClip mg1, mg2;
    public AudioClip dt1, dt2;
    public AudioClip intro, voiceDefault;
    public AudioClip woosh, twang;
    public AudioClip hit1, hit2, hit3, hit4, impact, crack;
    public AudioClip GoodEnd, BadEnd, NeutralEnd;

    public List<AudioClip> kingVoiceProfile = new List<AudioClip>();
    public List<AudioClip> queenVoiceProfile = new List<AudioClip>();
    public List<AudioClip> jesterVoiceProfile = new List<AudioClip>();

    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, List<AudioClip>> profiles = new Dictionary<string, List<AudioClip>>();
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
        soundLibrary.Add("Downtime_Intro", dt1);
        soundLibrary.Add("Downtime_Loop", dt2);
        soundLibrary.Add("Intro_Theme", intro);
        soundLibrary.Add("Voice", voiceDefault);
        soundLibrary.Add("Woosh", woosh);
        soundLibrary.Add("Twang", twang);
        soundLibrary.Add("HitA", hit1);
        soundLibrary.Add("HitB", hit2);
        soundLibrary.Add("HitC", hit3);
        soundLibrary.Add("HitD", hit4);
        soundLibrary.Add("Impact", impact);
        soundLibrary.Add("Crack", crack);
        soundLibrary.Add("GoodEnd_Theme", GoodEnd);
        soundLibrary.Add("BadEnd_Theme", BadEnd);
        soundLibrary.Add("NeutralEnd_Theme", NeutralEnd);

        //define voice profiles
        profiles.Add("King", kingVoiceProfile);
        profiles.Add("Queen", queenVoiceProfile);
        profiles.Add("Jester", jesterVoiceProfile);

        //set default music
        SetMusic("Intro_Theme");
    }
    public void SetMusic(string clipKey)
    {
        musicSource.clip = soundLibrary[clipKey];
        if (musicSource.clip == dt1)
            PlayDowntime();
        else
            musicSource.Play();
    }
    public void PlaySoundEffect(string clipKey)
    {
        AudioClip toPlay;
        toPlay = soundLibrary[clipKey];
        if(toPlay == null)
            toPlay = soundLibrary["LuteC3"];  //play the lute if the sound doesnt exist
        sfxSource.PlayOneShot(toPlay);
    }
    public void PlaySoundRandomPitch(string profile)
    {
        int randCoefficient = UnityEngine.Random.Range(-1, 1);
        if (randCoefficient == 0) randCoefficient = 1;

        sfxSource.pitch = 
            (UnityEngine.Random.value + UnityEngine.Random.Range(0,3)) 
            * randCoefficient;

        int profileSize = profiles[profile].Count;
        AudioClip toPlay = profiles[profile][UnityEngine.Random.Range(0, profileSize)];
        sfxSource.PlayOneShot(toPlay);

        //reset pitch after
        sfxSource.pitch = 1;
    }
    public void PlayDowntime()
    {
        //play the intro for downtime
        musicSource.clip = dt1;
        musicSource.Play();
        StartCoroutine(ContinueToLoop(dt1.length));
    }
    IEnumerator ContinueToLoop(float waitTime)
    {
        // wait until the intro section is completed,
        // then play the loop if the a new song hasnt been selected
        yield return new WaitForSeconds(waitTime);
        if (musicSource.clip == dt1)
            SetMusic("Downtime_Loop");
    }

    public void StopAllAudio()
    {
        musicSource.clip = null;
        musicSource.gameObject.SetActive(false);
        //sfxSource.gameObject.SetActive(false);
    }
}
