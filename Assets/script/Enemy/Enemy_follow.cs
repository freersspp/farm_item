using UnityEngine;
namespace PPman
{

    public class Enemy_follow : Enemy_state
    {
        public Enemy_follow(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {

        }
        public override void Enter()
        {
            base.Enter();
            // 進入跟隨狀態時的邏輯

            enemy.ani.SetFloat("移動", 1);
        }
        public override void Exit()
        {
            base.Exit();
            // 離開跟隨狀態時的邏輯
        }
        public override void Update()
        {
            base.Update();
            // 跟隨狀態下的更新邏輯

            if (enemy.IsWallfront() || !enemy.IsGroundfront())
            {
                stateMachine.SwitchState(enemy.enemy_idle);
            }

            enemy.Setvelocity(enemy.transform.right * enemy.followspeed);
            float direction = enemy.player.position.x > enemy.transform.position.x ? 1 : -1;
            enemy.Flip(direction);

            //與玩家的距離 小於等於 攻擊距離就進入攻擊狀態
            if (Vector2.Distance(enemy.transform.position, enemy.player.position) <= enemy.InAttackArea)
            {
                stateMachine.SwitchState(enemy.enemy_attack);
            }

        }

    }

}

