using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
 
public class SoundEffects : MonoBehaviour
 
{
   public AudioSource audioSource;
   [SerializeField ]public AudioClip mouseDownClip;
   [SerializeField ]public AudioClip mouseUpClip;
   public float volume = 0.9f;
  
   void OnMouseDown()
   {
       audioSource.PlayOneShot(mouseDownClip, volume);
   }
   void OnMouseUp()
   {
       audioSource.PlayOneShot(mouseUpClip, volume);
   }
 
}
