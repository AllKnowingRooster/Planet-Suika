using UnityEngine;

[System.Serializable]
public class AudioData
{
    public float volume;
    public float cooldown;
    public AudioClip clip;
    [HideInInspector] public float lastPlayed;

    public AudioData(float volume, float cooldown, AudioClip clip)
    {
        this.lastPlayed = 0.0f;
        this.volume = volume;
        this.cooldown = cooldown;
        this.clip = clip;
    }

    public void UpdateLastPlayed(float time)
    {
        lastPlayed = time;
    }

}
