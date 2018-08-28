using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 必要なデータの格納場所として作用する目的
/// 元データ、作業内容などを保存できるようにしたい
/// </summary>

public class SoundMaster : MonoBehaviour {

    public Once_Action[] list_size;

    [System.Serializable]
    public class Once_Action
    {

        public string Sound_Name;//音源の名前
        public AudioClip audioclip;//音源本体

        [SerializeField, HeaderAttribute("Select either/どちらか選択してください（開始時のみ影響します）")] //項目の前にあるタイトル

        public bool ChannelBase;
        public bool ObjectBase;

        [Space(10)]
        public SurroundChanger Change;

        public bool sampleshot = false;

    }

    [System.Serializable]
    public class SurroundChanger
    {
        [TooltipAttribute("異なるBaseに遷移開始")]
        public bool Change_now; // 変更開始のトリガー

        [TooltipAttribute("何秒で遷移するかの秒数")]
        public float Rate_of_change; //変化率(何秒で100％遷移するか)

        [Range(0, 1), TooltipAttribute("ChannelBaseとObjectBaseのブレンド率\n（0でchannelbase、1でObjectBaseになります）")]
        public float Rate_now = 0; //ChannelBase（0のとき）とObjectBase（１のとき）のブレンド率　
    }

}
