using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundChild : MonoBehaviour {

    public string Sound_Name; //Master側で命名したほしい音の名前
    private SoundMaster soundmaster; //SoundMaster格納用
    private SoundStatus soundStatus; //SoundStatus格納用
    private int child_number = 0; //向こうの配列番号を格納する用
    private AudioClip child_audioclip; //引っ張ってくるclipを格納する用
    private AudioSource audioSource; //このオブジェクトにつけたAudioSoruceを選択する用

    private bool nowChannel; // 現在のチャンネルがどちらかの判断用trueがChannelBase


    void Start () {
        
        //まずは子オブジェクト（このスクリプトがついたオブジェクト）についたオーディオソースを取得
        audioSource = GetComponent<AudioSource>();

        //"SoundMaster_Obj"オブジェクトからSoundMasterを取得
        soundmaster = GameObject.Find("SoundMaster_Obj").GetComponent<SoundMaster>();
        soundStatus = GameObject.Find("SoundStatus_Obj").GetComponent<SoundStatus>();

        // 最初にどこのデータを参照するか名前で指定→名前から配列番号を取得（名前間違えると悲惨なので注意）
        child_number = soundStatus.namenumber.IndexOf(Sound_Name);

        //AudioCilpを親から取得
        child_audioclip = soundmaster.list_size[child_number].audioclip;

        //AudioSource内に上記オーディオクリップを格納
        audioSource.clip = child_audioclip;

        //spatialBlend(2Dか3Dかの割合、ブレンド率。)→変化するようにする
        //audioSource.spatialBlend = 0;//0→2D　1→3D 
        //下記関数等で使用している

        FirstBase(soundmaster.list_size[child_number].ChannelBase,soundmaster.list_size[child_number].ObjectBase);

        //rolloffMode → これでmaxDistanceに影響するモードを変更する。モードはLogarithmic、Linear、Customの3種類。
        //              （Customはスクリプトからいじれないらしいので、影響があるのはLogarithmic、Linearの2種類）デフォルトはLogarithmicモードになっている。
        //               また、Unity上の該当プルダウンメニューを操作すると、maxDistanceの値に変化はなかったので、最初からモードを選択した状態でいじるのが前提になると思う
        //モードの変更は下記のようなスクリプトとなる
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic; //デフォルト通りの形なので今のところ実質意味はない

        //maxDistance→Logarithmicモードでは音が減衰を停止する距離(減衰の停止がどういう意味かは不明（volumeが0になるという意味なのかただ単に止まるだけなのか）)
        //             Linearモードでは音が完全に聞こえなくなる距離
        audioSource.maxDistance = 500; //(Logarithmicモード)デフォルト通りの形なので今のところ実質意味はない

        //minDistance→この値の外側に行くと減衰が開始される
        audioSource.minDistance = 1; //デフォルト通りの形なので今のところ実質意味はない

    }

    void Update () {

        SampleShot(soundmaster.list_size[child_number].sampleshot);

        BaseChanger(ref soundmaster.list_size[child_number].Change.Change_now,
                    soundmaster.list_size[child_number].Change.Rate_of_change,
                    ref soundmaster.list_size[child_number].Change.Rate_now);

	}

    /// <summary>
    /// とりあえず一発鳴らして試す用
    /// Master側にチェックボックスがある
    /// </summary>
    /// <param name="shot"></param>
    void SampleShot(bool shot)
    {
        if (shot)
        {
            audioSource.PlayOneShot(child_audioclip);
            soundmaster.list_size[child_number].sampleshot = false;
        }
    }

    /// <summary>
    /// 用途として最初のチャンネル状態の設定用
    /// Master側にチェックボックスがある。
    /// </summary>
    /// <param name="Channel"></param>
    /// <param name="Object"></param>
    void FirstBase(bool Channel, bool Object)
    {
        //もしもどちらにもチェックが入っていたら、もしくはどちらにもチェックがはいっていなかったら強制的にChannelBaseにする
        if (!Channel && !Object) Channel = true;
        else if (Channel && Object) Channel = true;

        //最初期Base切り替え
        if (Channel) audioSource.spatialBlend = 0;
        else audioSource.spatialBlend = 1;

        //設定したベースと同じになるように現在のブレンド率を変更
        soundmaster.list_size[child_number].Change.Rate_now = audioSource.spatialBlend;

    }

    /// <summary>
    /// 用途としてチャンネルの変更用
    /// Master側に操作用のバーとチェックボックスがある。
    /// Change_nowがチェックされることで変更を開始する。changeRateは何秒間で2D↔3Dの切り替えをするかの値。
    /// nowRateは今の2D3Dのブレンド率
    /// </summary>
    /// <param name="Change_now"></param>
    /// <param name="changeRate"></param>
    /// <param name="nowRate"></param>
    void BaseChanger(ref bool Change_now,float changeRate, ref float nowRate)
    {

        //変更が実行されていない間はnowRateのバーのブレンド率となる
        if (!Change_now)
        {
            audioSource.spatialBlend = nowRate;
            if (audioSource.spatialBlend >= 0.5) nowChannel = false;
            else nowChannel = true;
            //Debug.Log("Don't_Change_now");
        }
        //自動でchangeRateで指定された時間で遷移する
        if (Change_now)
        {
            if (!nowChannel)
            {
                audioSource.spatialBlend -= Time.deltaTime / changeRate;
                if (audioSource.spatialBlend <= 0)
                {
                    nowRate = 0;
                    Change_now = false;
                }
            }
            else
            {
                audioSource.spatialBlend += Time.deltaTime / changeRate;
                if (audioSource.spatialBlend >= 1)
                {
                    nowRate = 1;
                    Change_now = false;
                }
            }
           // Debug.Log("Change_now" + audioSource.spatialBlend);
        }
    }
}
