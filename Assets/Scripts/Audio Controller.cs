using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
    [SerializeField] UIManager manager;
    [SerializeField] AudioSource BG_music;
    public AudioSource Ball;
    public AudioSource Transition;
    void Start()
    {
        Ball.Stop();
        Transition.Stop();
        BG_music.playOnAwake = true;
        BG_music.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        BG_music.volume = manager.slider.value;
    }
}
