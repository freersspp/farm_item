using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 敵人腳本:管理敵人資料與行為
    /// </summary>
    public class Enemy : character
    {
        [field: SerializeField]public Vector2 idleTime { get; private set; }
        [field: SerializeField] public Vector2 WalkTime { get; private set; }

        public StateMachine stateMachine { get; private set; } // 狀態機
        public Enemy_idle enemy_idle { get; private set; } // 待機狀態
        public Enemy_walk enemy_walk { get; private set; } // 遊走狀態
        public Enemy_follow enemy_follow { get; private set; } // 追擊狀態
        public Enemy_attack enemy_attack { get; private set; } // 攻擊狀態
        public Enemy_die enemy_die { get; private set; } // 死亡狀態


        protected override void Awake()
        {
            base.Awake();
            stateMachine = new StateMachine();
            enemy_idle = new Enemy_idle(this, stateMachine, "Idle");
            enemy_walk = new Enemy_walk(this, stateMachine, "Walk");
            enemy_follow = new Enemy_follow(this, stateMachine, "Follow");
            enemy_attack = new Enemy_attack(this, stateMachine, "Attack");
            enemy_die = new Enemy_die(this, stateMachine, "Die");
            // 設定初始狀態
            stateMachine.DefaultState(enemy_idle);
        }

        private void Update()
        {
            // 更新狀態機
            stateMachine.UpdateState();
        }



    }
}