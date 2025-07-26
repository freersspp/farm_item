using System.Collections;
using UnityEngine;
namespace PPman
{
    public class FadeSystem : MonoBehaviour
    {
        /// <summary>
        /// 漸變系統: 控制CanvasGroup的透明度漸變
        /// </summary>
        /// <param name="group">畫布群組元件</param>
        /// <param name="fadein">是否淡入</param>
        /// <returns></returns>
        public static IEnumerator Fade(CanvasGroup group, bool fadein = true)
        //static : 讓這個方法可以在不需要 掛上物件 或 實例化狀況下就可以直接使用
        {
            //漸變質: 確認增加的的話, 增加量為0.1, 否的話減少量為0.1
            float increase = fadein ? 0.1f : -0.1f;

            //迴圈重複執行10次, 每次等待0.03秒
            for (int i = 0; i < 10; i++)
            {
                //如果是淡入, 透明度增加, 否則透明度減少
                group.alpha += increase;
                //漸變中間等待時間為0.03秒
                yield return new WaitForSeconds(0.03f);
            }

        }
    }
}