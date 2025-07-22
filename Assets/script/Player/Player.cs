using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 儲存玩家資料與基本功能
    /// </summary>
    public class Player : character
    {
        #region 變數
        [field:Header("基本控制")]
        [field:SerializeField, Range(0, 20)] public float movespeed { get; private set; } = 5f;
        [field: SerializeField, Range(0, 20)] public float jumpForce { get; private set; } = 10f;
        [field: SerializeField, Range(0, 3)] public float 攻擊中斷時間 { get; private set; } = 1;
        [field: SerializeField] public float[] 攻擊動畫時間 { get; private set; }
        [field: SerializeField, Range(0, 20)] public float 衝刺速度 { get; private set; } = 12f;
        [field: SerializeField, Range(0, 3)] public float 衝刺時間 { get; private set; } = 0.3f;
        [field:SerializeField]public float 防禦時間 { get; private set; }  //防禦時間





        public bool canmove { get; set; } = false; //是否可以移動
        public bool canjump { get; set; } = false; //是否可以跳躍
        public bool canattack { get; set; } = false; //是否可以攻擊 
        public bool candash { get; set; } = false; //是否可以衝刺
        public bool candefense { get; set; } = false; //是否可以防禦


        [Header("檢查地板")]
        [SerializeField] private Vector3 確認地板尺寸 = Vector3.one;
        [SerializeField] private Vector3 確認地板尺寸的位置;
        [SerializeField] private LayerMask 可以跳得圖層;

        [Header("粒子效果")]
        public GameObject dashFireTrail;


        #endregion

        #region 狀態資料
        public StateMachine stateMachine { get; private set; }
        public Player_idle player_idle { get; private set; }
        public Player_walk player_walk { get; private set; }
        public Player_jump player_jump { get; private set; }
        public Player_fall player_fall { get; private set; }
        public Player_attack player_attack { get; private set; }
        public Player_dash player_dash { get; private set; }
        public Player_defense player_defense { get; private set; }
        public Player_die player_die { get; private set; }


        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + 確認地板尺寸的位置, 確認地板尺寸);
        }

        protected override void Awake()
        {
            base.Awake();

            //實例化狀態機
            stateMachine = new StateMachine();
            //初始化各個狀態, this在這邊指得是"Player" 
            player_idle = new Player_idle(this, stateMachine, "待機");
            player_walk = new Player_walk(this, stateMachine, "走路");
            player_jump = new Player_jump(this, stateMachine, "跳躍");
            player_fall = new Player_fall(this, stateMachine, "落下");
            player_attack = new Player_attack(this, stateMachine, "攻擊");
            player_dash = new Player_dash(this, stateMachine, "衝刺");
            player_defense = new Player_defense(this, stateMachine, "防禦");
            player_die = new Player_die(this, stateMachine, "死亡");

            //設定狀態機的"待機"為預設狀態
            stateMachine.DefaultState(player_idle);

        }

        private void Update()
        {
            //更新狀態機
            stateMachine.UpdateState();
           
        }

        /// <summary>
        /// 確認是否在地板上
        /// </summary>
        /// <returns></returns>
        public bool IsGround()
        {
            return Physics2D.OverlapBox(transform.position + 確認地板尺寸的位置, 確認地板尺寸, 0, 可以跳得圖層);
        }

        public void SwitchControl(bool cancontrol)
        {
            rig.velocity = Vector3.zero; //將玩家停止
            stateMachine.SwitchState(player_idle); //切換到待機狀態
            canmove = cancontrol;
            canjump = cancontrol;
            canattack = cancontrol;
        }

        protected override void Die()
        {
            base.Die();
            stateMachine.SwitchState(player_die); //切換到死亡狀態
        }


    }
     
}
 