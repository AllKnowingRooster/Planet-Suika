using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IObserver

{
    public static AudioManager instance { get; private set; }
    [SerializeField] List<AudioData> listSfx;
    [SerializeField] Dictionary<PlayerAction, AudioData> audioMap;
    [SerializeField] AudioData bgm;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        audioMap = new Dictionary<PlayerAction, AudioData>();
        PlayerAction[] listEnum = (PlayerAction[])Enum.GetValues(typeof(PlayerAction));
        for (int i = 0; i < listEnum.Length; i++)
        {
            audioMap[listEnum[i]] = listSfx[i];
        }
        bgmSource.clip = bgm.clip;
        bgmSource.volume = bgm.volume;
        bgmSource.Play();
        DontDestroyOnLoad(instance);
    }

    public void OnNotify(PlayerAction action)
    {
        if (!audioMap.ContainsKey(action))
        {
            return;
        }

        AudioData data = audioMap[action];

        if (Time.time - data.cooldown >= data.lastPlayed)
        {
            sfxSource.PlayOneShot(data.clip, data.volume);
            data.UpdateLastPlayed(Time.time);
        }
    }

    private void OnEnable()
    {
        GameManager.instance.AddObserver(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveObserver(this);
    }
}
