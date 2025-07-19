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
            Debug.Log($"<color=blue>待機時間:<{idleTime}></color>");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            //如果計時器 >= 閒置時間，則切換到遊走狀態
            if (timer >= idleTime)
            {
                stateMachine.SwitchState(enemy.enemy_walk);
                Debug.Log($"<color=blue>閒置時間結束，切換到遊走狀態</color>");
            }
        }
    }
}
