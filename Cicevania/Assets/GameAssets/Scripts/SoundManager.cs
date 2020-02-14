using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField]
    private AudioClip audioC;
    [SerializeField]
    private bool isLoopable;

    public bool IsLoopable() { return isLoopable; }
    
    public AudioClip GetAudioClip() { return audioC; }
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private List<Sound> gameSounds = new List<Sound>();

    AudioSource aSource;   

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    //Play loopable/menu music
   
    public void PlayMainTheme()
    {
        SetLoopable(0);
        aSource.clip = gameSounds[0].GetAudioClip();
        PlayMusic();
    }

    public void PlayLevelCompleted()
    {
        SetLoopable(1);
        aSource.clip = gameSounds[1].GetAudioClip();
        PlayMusic();
    }

    public void PlayGameOver()
    {
        SetLoopable(2);
        aSource.clip = gameSounds[2].GetAudioClip();
        PlayMusic();
    }

    public void PlayWorldClear()
    {
        SetLoopable(3);
        aSource.clip = gameSounds[3].GetAudioClip();
        PlayMusic();
    }

    public void PlayPauseMusic()
    {
        SetLoopable(4);
        PauseMusic();
        aSource.PlayOneShot(gameSounds[4].GetAudioClip());
    }

    //Play one shots

    public void Play1UPShot()
    {
        aSource.PlayOneShot(gameSounds[5].GetAudioClip());
    }

    public void PlayBumpShot()
    {
        aSource.PlayOneShot(gameSounds[6].GetAudioClip());
    }

    public void PlayCoinPickUpShot()
    {
        aSource.PlayOneShot(gameSounds[7].GetAudioClip());
    }

    public void PlayGoalFlagDownShot()
    {
        aSource.PlayOneShot(gameSounds[8].GetAudioClip());
    }

    public void PlayJumpShot()
    {
        aSource.PlayOneShot(gameSounds[9].GetAudioClip());
    }

    public void PlayEnemyKilledShot()
    {
        aSource.PlayOneShot(gameSounds[10].GetAudioClip());
    }

    public void PlayMarioDiesShot()
    {
        aSource.PlayOneShot(gameSounds[11].GetAudioClip());
    }

    public void PlayPeachDiesShot()
    {
        aSource.PlayOneShot(gameSounds[12].GetAudioClip());
    }

    public void PlayPipeShot()
    {
        aSource.PlayOneShot(gameSounds[13].GetAudioClip());
    }

    public void PlayPowerUpAppearShot()
    {
        aSource.PlayOneShot(gameSounds[14].GetAudioClip());
    }

    public void PlayPowerUpPickUpShot()
    {
        aSource.PlayOneShot(gameSounds[15].GetAudioClip());
    }

    public void PlaySpringActivationShot()
    {
        aSource.PlayOneShot(gameSounds[16].GetAudioClip());
    }


    
    public void PlayMusic()
    {
        aSource.Play();
    }

    public void PauseMusic()
    {
        if (aSource.isPlaying) aSource.Pause();
    }

    public void StopMusic()
    {
        if (aSource.isPlaying) aSource.Stop();
    }

    public void SetLoopable(int audioIndex)
    {
        if (gameSounds[audioIndex].IsLoopable()) aSource.loop = true;
        else aSource.loop = false;
    }
}
