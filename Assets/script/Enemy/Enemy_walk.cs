using UnityEngine;
namespace PPman
{

    public class Enemy_walk : Enemy_state
    {
        private float WalkTime; // 遊走時間
        public Enemy_walk(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // 進入行走狀態時的邏輯
            WalkTime = Random.Range(enemy.WalkTime.x, enemy.WalkTime.y ); // 隨機設定行走時間
            Debug.Log($"<color=red>行走時間:<{WalkTime}></color>");
        }
        public override void Exit()
        {
            base.Exit();
            // 離開行走狀態時的邏輯
        }
        public override void Update()
        {
            base.Update();
            // 行走狀態下的更新邏輯
            //如果計時器>=行走時間，則切換到待機狀態
            if(timer >= WalkTime)
            {
                stateMachine.SwitchState(enemy.enemy_idle);
                Debug.Log($"<color=red>行走時間結束，切換到待機狀態</color>");
            }
        }
    }



}