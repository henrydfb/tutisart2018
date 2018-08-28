using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Masterの楽曲番号と曲につけた名前をリンクさせるためのList用スクリプト
/// </summary>
public class SoundStatus : MonoBehaviour {

    private SoundMaster soundmaster; //SoundMaster格納用
    public  List<string> namenumber; //名前を保存し、その番号が親の配列番号になるはず

    void Awake () {

        soundmaster = GameObject.Find("SoundMaster_Obj").GetComponent<SoundMaster>();

        for (int i = 0; i < soundmaster.list_size.Length; i++)
        {
            namenumber.Add(soundmaster.list_size[i].Sound_Name);
        }
       
    }

}
