using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 玩家技能2的腳本
    /// </summary>
    public class Player_skill2 : PlayerState
    {
        private float skill2EndTime; // 技能2結束時間
        public Player_skill2(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("skill2");
            

        }
        public override void Exit()
        {
            base.Exit();
            skill2EndTime = Time.time;
            

        }
        public override void Update()
        {
            base.Update();
            if(timer >= player.Skill2time)
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }

    }
}
