using UnityEngine;
using System.Collections;

namespace PPman
{
    public class Player_skill1 : PlayerState
    {
        private Vector3 originalPosition;   // 原始位置
        private Vector3 dashTarget;         // 衝刺目標位置
        private float dashDistance = 5f;    // 衝刺距離
        private float skillDuration = 0.3f; // 技能持續時間

        public Player_skill1(Player _player, StateMachine _statemachine, string _name)
            : base(_player, _statemachine, _name) { }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("skill1");

            // 記錄原始位置
            originalPosition = player.transform.position;

            // 根據面向方向決定衝刺目標
            float direction = player.isFacingRight ? -1 : 1;
            dashTarget = originalPosition + new Vector3(direction * dashDistance, 0, 0);

            // 先瞬間移動到衝刺位置
            player.transform.position = dashTarget;

            // 等待技能結束
            player.StartCoroutine(EndSkillAfterDelay());
        }

        private IEnumerator EndSkillAfterDelay()
        {
            yield return new WaitForSeconds(skillDuration);

            // 回到原位
            player.transform.position = originalPosition;

            // 切回待機
            stateMachine.SwitchState(player.player_idle);
        }

        public override void Exit()
        {
            base.Exit();
            
            
        }
    }
}
