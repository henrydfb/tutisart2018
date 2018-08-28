using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private SoundMaster soundmaster; //SoundMaster格納用
    private SoundStatus soundStatus; //SoundStatus格納用
    private int child_number = 0; //向こうの配列番号を格納する用
    private AudioClip child_audioclip; //引っ張ってくるclipを格納する用


    private void Start()
    {
        //"SoundManager"オブジェクトからSoundMasterを取得
        soundmaster = GameObject.Find("SoundMaster_Obj").GetComponent<SoundMaster>();
        soundStatus = GameObject.Find("SoundStatus_Obj").GetComponent<SoundStatus>();
    }

    public void soundshot(string Sound_Name , AudioSource audioSource)
    {
        // 最初にどこのデータを参照するか名前で指定→名前から配列番号を取得（名前間違えると悲惨なので注意）
        child_number = soundStatus.namenumber.IndexOf(Sound_Name);

        //AudioCilpを親から取得
        child_audioclip = soundmaster.list_size[child_number].audioclip;

        audioSource.PlayOneShot(child_audioclip);
    }
   
}
