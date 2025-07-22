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

        [SerializeField, Header("會讓自己受傷的物件標籤")] private string damageTag;
        [SerializeField, Range(0, 100)] protected float HPMAX = 100f; // 最大生命值
        protected float HP; // 當前生命值



        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            HP = HPMAX; // 初始化生命值
        }

        //2D觸發事件(碰到勾選Istrigger的物件會執行一次, "collision"會記錄碰到的物件資訊)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果血量為0就跳出;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }

            if (collision.CompareTag(damageTag))
            {
                Damage(collision.GetComponent<AttackValue>().attackPower);
            }
            
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

        private void Damage(float damage)
        {
            HP -= damage; // 減少生命值
            Debug.Log($"<color=red>受到傷害: {name}受傷, 生命值: {HP}</color>");

            if (HP <= 0)
            {
                Die(); // 死亡處理
            }
        }

        protected virtual void Die()
        {
            Debug.Log($"<color=red> {name}死亡</color>");
        }
    }
}