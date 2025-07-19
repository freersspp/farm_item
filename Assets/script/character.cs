using UnityEngine;
namespace PPman
{
     /// <summary>
    /// 腳色:翻面和加速度或者腳色共同功能
    /// </summary>
    public class character : MonoBehaviour
    {
        // {get; private set;}唯獨屬性:允許外部取得但不能修改
        public Animator ani { get; private set; }
        public Rigidbody2D rig { get; private set; }

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 設定加速度
        /// </summary>
        /// <param name="velocity">加速度</param>
        public void Setvelocity(Vector3 velocity)
        {
            rig.velocity = velocity;
        }

        /// <summary>
        /// 翻轉角色
        /// </summary>
        /// <param name="h">方向</param>
        public void Flip(float h)
        {
            if (Mathf.Abs(h) < 0.1)
            {
                return;
            }

            float 腳色角度 = h > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, 腳色角度, 0);
        }
    }
}