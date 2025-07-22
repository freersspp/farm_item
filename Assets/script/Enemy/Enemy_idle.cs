using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 敵人閒置狀態腳本
    /// </summary>

    public class Enemy_idle : Enemy_state
    {
        private float idleTime;
        public Enemy_idle(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            idleTime = Random.Range(enemy.idleTime.x, enemy.idleTime.y); // 隨機閒置時間
            //Debug.Log($"<color=blue>待機時間:<{idleTime}></color>");

            enemy.Setvelocity(Vector3.zero); // 設定敵人移動速度為0
            enemy.ani.SetFloat("移動", 0); // 設定動畫參數為閒置狀態
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //如果前方有玩家就切換到追擊狀態
            if (enemy.IsPlayerfront())
            {
                stateMachine.SwitchState(enemy.enemy_follow);
            }

            //如果計時器 >= 閒置時間，則切換到遊走狀態
            if (timer >= idleTime)
            {
                stateMachine.SwitchState(enemy.enemy_walk);
                //Debug.Log($"<color=blue>閒置時間結束，切換到遊走狀態</color>");
            }

            //如果前方有牆壁或是沒地板了, 就放棄追玩家回到待機
            if (enemy.IsWallfront() || !enemy.IsGroundfront())
            {
                return;
            }
        }
    }
}
