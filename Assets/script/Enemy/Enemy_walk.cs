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
            WalkTime = Random.Range(enemy.WalkTime.x, enemy.WalkTime.y); // 隨機設定行走時間
            //Debug.Log($"<color=red>行走時間:<{WalkTime}></color>");
            enemy.ani.SetFloat("移動", 1);
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

            enemy.Setvelocity(enemy.transform.right * enemy.Walkspeed); // 設定敵人移動速度

            //如果計時器>=行走時間，則切換到待機狀態
            if (timer >= WalkTime)
            {
                stateMachine.SwitchState(enemy.enemy_idle);
                //Debug.Log($"<color=red>行走時間結束，切換到待機狀態</color>");
            }

            //如果 前方有牆壁 或是 前方沒地板 就轉向
            if (enemy.IsWallfront() || !enemy.IsGroundfront())
            {
                //依據Y軸角度翻面, 如果Y是0則翻面到-1(左), 否則翻面到+1(右)
                enemy.Flip(enemy.transform.eulerAngles.y == 0 ? -1 : +1);
            }

            //如果前方有玩家就切換到追擊狀態
            if (enemy.IsPlayerfront())
            {
                stateMachine.SwitchState(enemy.enemy_follow);
            }
        }
    }
}



