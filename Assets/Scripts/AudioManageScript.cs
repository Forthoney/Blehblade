using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.IO;

public class AudioManageScript : MonoBehaviour
{
    [SerializeField] private AudioSource aSource;
    public List<AudioClip> musicList;
    
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    public void playSound(int index)
    {
        aSource.clip = musicList[index];
        aSource.Play();
    }
}
