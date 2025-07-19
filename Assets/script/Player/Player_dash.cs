using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 玩家衝刺狀態 
    /// </summary>
    public class Player_dash : PlayerState
    {
        private float dashSpeed;
        private float dashTime;
        private float dashDuration;

        public Player_dash(Player player, StateMachine statemachine, string name) : base(player, statemachine, name)
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

            // 🔥 啟動火焰粒子效果
            if (player.dashFireTrail != null)
            {
                player.dashFireTrail.SetActive(true);
            }
        }

        public override void Update()
        {
            base.Update();

            // 固定向左或右衝刺
            player.rig.velocity = new Vector2(h * dashSpeed, player.rig.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 切換到跳躍狀態（保持衝刺速度 + 跳躍）
                stateMachine.SwitchState(player.player_jump);
                return; // 立刻跳離 dash 邏輯
            }

            if (timer >= dashDuration)
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }

        public override void Exit()
        {
            base.Exit();

            // 🔥 停止火焰粒子效果
            if (player.dashFireTrail != null)
            {
                player.dashFireTrail.SetActive(false);
            }

            // 結束後速度歸零或回正常狀態
            player.rig.velocity = Vector2.zero;
        }
    }
}