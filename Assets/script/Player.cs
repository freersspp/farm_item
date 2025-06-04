using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 儲存玩家資料與基本功能
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region 變數
        [field:Header("基本控制")]
        [field:SerializeField, Range(0, 20)] public float movespeed { get; private set; } = 5f;
        [field: SerializeField, Range(0, 20)] public float jumpForce { get; private set; } = 10f;

        // {get; private set;}唯獨屬性:允許外部取得但不能修改
        public Animator ani { get; private set; }
        public Rigidbody2D rig { get; private set; }


        [Header("檢查地板")]
        [SerializeField] private Vector3 確認地板尺寸 = Vector3.one;
        [SerializeField] private Vector3 確認地板尺寸的位置;
        [SerializeField] private LayerMask 可以跳得圖層;
        #endregion

        #region 狀態資料
        public StateMachine stateMachine { get; private set; }
        public Player_idle player_idle { get; private set; }
        public Player_walk player_walk { get; private set; }
        public Player_jump player_jump { get; private set; }
        public Player_fall player_fall { get; private set; }
        public Player_attack player_attack { get; private set; } 
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + 確認地板尺寸的位置, 確認地板尺寸);
        }

        private void Awake()
        {

            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            //實例化狀態機
            stateMachine = new StateMachine();
            //初始化各個狀態, this在這邊指得是"Player" 
            player_idle = new Player_idle(this, stateMachine, "待機");
            player_walk = new Player_walk(this, stateMachine, "走路");
            player_jump = new Player_jump(this, stateMachine, "跳躍");
            player_fall = new Player_fall(this, stateMachine, "落下");
            player_attack = new Player_attack(this, stateMachine, "攻擊");
             


            //設定狀態機的"待機"為預設狀態
            stateMachine.DefaultState(player_idle);
        }

        private void Update()
        {
            //更新狀態機
            stateMachine.UpdateState();
           
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

        /// <summary>
        /// 確認是否在地板上
        /// </summary>
        /// <returns></returns>
        public bool Isground()
        {
            return Physics2D.OverlapBox(transform.position + 確認地板尺寸的位置, 確認地板尺寸, 0, 可以跳得圖層);
        }

    }
     
}
 