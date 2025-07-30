using Cinemachine;
using UnityEngine;
using System.Collections;

namespace PPman
{
    public class CameraManager : MonoBehaviour
    {
        #region
        //使用時機:
        //1. 此類別只有一個存在
        //2. 需要在其他類別中方便地存取攝影機管理器

        // CameraManager類別使用單例模式，確保在整個遊戲中只有一個實例存在。
        private static CameraManager _instance;
        //讓外部讀取的唯獨屬性
        public static CameraManager Instance
        {
            get
            {
                //如果實例為空，則尋找場景中的CameraManager物件
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CameraManager>();
                   
                }
                //傳回變數
                return _instance;

            }
        }
        #endregion

        private CinemachineVirtualCamera virtualCamera;
        private CinemachineBasicMultiChannelPerlin perlin;

        private float defaultAmplitude, defaultFrequency;

        private void Awake()
        {
            //獲得虛擬攝影機元件
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            //獲得柏林函數元件
            perlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            //獲得柏林函數的預設振幅和頻率
            defaultAmplitude = perlin.m_AmplitudeGain;
            defaultFrequency = perlin.m_FrequencyGain;
        }

        public void StartShake(float amplitude, float frequency, float time)
        {
            
            //啟動震動協程
            StartCoroutine(ShakeCoroutine(amplitude, frequency, time));
        }

        private IEnumerator ShakeCoroutine(float amplitude, float frequency, float time)
        {
            //設定柏林函數的振幅和頻率
            perlin.m_AmplitudeGain = amplitude;
            perlin.m_FrequencyGain = frequency;
            //持續震動指定時間
            yield return new WaitForSeconds(time);
            //重置柏林函數的振幅和頻率
            perlin.m_AmplitudeGain = defaultAmplitude;
            perlin.m_FrequencyGain = defaultFrequency;
        }
    }
}