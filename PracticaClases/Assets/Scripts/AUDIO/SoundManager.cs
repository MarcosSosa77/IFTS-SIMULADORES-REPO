using UnityEngine;
using System;
using System.Collections;

public enum SoundType
{
    TAP_SCREW,
    COLLECT_SCREW_OK,
    COLLECT_SCREW_WRONG,
    COMPLETE_BOX,
    WIN_SOUND

}


[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private SoundList[] soundList;
    [SerializeField] private AudioClip[] musicList;
    
    public AudioSource audSource;
    public AudioSource musicSource;

    private bool isMuted = false;
   
    private void Awake()
    {
        instance = this;

        audSource = GetComponent<AudioSource>();
        musicSource = GetComponent<AudioSource>();
        musicSource.loop = true; // Ensure music loops by default

    }

    //Sound type and volume
    public void PlaySound(SoundType sound, float vol = 0.5f)
    {
        if (isMuted) return;

        //instance.audSource.PlayOneShot(instance.soundList[(int)sound], vol);
        AudioClip[] clips = soundList[(int)sound].Sounds;
        if (clips.Length <= 0) return;
        AudioClip rndClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audSource.PlayOneShot(rndClip, vol);
    }  

    public void StopSound()
    {
        audSource.Stop();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
    }

    private void PlaySoundOnSource(AudioSource source, SoundType sound, float vol = 0.5f)
    {
        if (isMuted) return;

        AudioClip[] clips = soundList[(int)sound].Sounds;
        if (clips.Length <= 0) return;
        AudioClip rndClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        source.PlayOneShot(rndClip, vol);
    }







#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }


    }
#endif
}

[Serializable]
public struct SoundList
{
    public readonly AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
