using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 世界座標(物件座標) 轉換 介面座標
    /// </summary>
    public class WorktoUIpoint : MonoBehaviour
    {
        private RectTransform rect;
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
        /// <summary>
        /// 更新UI位置到物件座標 + 偏移量
        /// </summary>
        /// <param name="target">目標物件</param>
        /// <param name="offset">位移</param>
        private void Updatepoint(Transform target, Vector3 offset)
        {
            // 物件座標 + 偏移量
            Vector3 point = target.position + offset;
            Vector3 uipoint = Camera.main.WorldToScreenPoint(point);
            rect.position = uipoint;
        }


    }

}


