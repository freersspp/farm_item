using UnityEngine;
namespace PPman
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundManager>();
                }
                return _instance;
            }
        }
        private AudioSource audio;

        [SerializeField, Header("所有音效")]private AudioClip[] allsound;
        /// <summary>
        /// 主角受傷音效
        /// 主角死亡音效
        /// 劍氣音效
        /// 吃道具音效
        /// 怪物受傷音效
        /// 怪物死亡音效
        /// 掉落道具音效
        /// 攻擊1_2音效
        /// 攻擊3音效
        /// 落地音效
        /// 走路音效
        /// 跳躍音校
        /// </summary>


        private void Awake()
        {
            audio = GetComponent<AudioSource>();
        }


        /// <summary>
        /// 播放音效(固定音量)
        /// </summary>
        /// <param name="soundtype">音效類別</param>
        public void PlaySound(Soundtype soundtype)
        {
            audio.PlayOneShot(allsound[(int)soundtype]);
        }
        /// <summary>
        /// 播放音效(隨機音量)
        /// </summary>
        /// <param name="soundtype">音效類別</param>
        /// <param name="minvolume">最小音量</param>
        /// <param name="maxvolume">最大音量</param>
        public void PlaySound(Soundtype soundtype, float minvolume = 0.8f, float maxvolume = 1.1f)
        {
            float volume = Random.Range(minvolume, maxvolume);
            audio.PlayOneShot(allsound[(int)soundtype], volume);
        }


    }
}