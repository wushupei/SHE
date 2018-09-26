using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip milk, enemy, death; //牛奶音效和敌人音效  
    public Transform cameraTh; //摄像机位置

    public void PlayAudio(string name) //在摄像机位置播放声音
    {
        AudioClip clip = null;
        switch (name)
        {
            case "M":
                clip = milk;
                break;
            case "E":
                clip = enemy;
                break;
            case "D":
                clip = death;
                break;
        }
        if (clip)
            AudioSource.PlayClipAtPoint(clip, cameraTh.position);
    }
}
