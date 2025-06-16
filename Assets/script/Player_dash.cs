using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 玩家衝刺狀態
    /// </summary>
    public class Player_dash : State
    {
        private float dashSpeed;
        private float dashTime;
        private float dashDuration;

        public Player_dash(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();

            dashDuration = player.衝刺時間;
            dashSpeed = player.衝刺速度;

            // 動畫或特效
            player.ani.SetTrigger("觸發衝刺");

            // 設定角色面向方向
            player.Flip(h);

        }

        public override void Update()
        {
            base.Update();

            // 固定向左或右衝刺
            player.rig.velocity = new Vector2(h * dashSpeed, player.rig.velocity.y);

            if (timer >= dashDuration)
            {
                stateMachine.SwitchState(player.player_idle); 
            }

            
        }

        public override void Exit()
        {
            base.Exit();
            // 結束後速度歸零或回正常狀態
            player.rig.velocity = Vector2.zero;
        }
    }
}
