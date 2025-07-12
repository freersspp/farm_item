using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 玩家跳躍
    /// </summary>
    public class Player_jump : PlayerState
    {
        public Player_jump(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.Setvelocity(new Vector3(player.rig.velocity.x, player.jumpForce));
            player.ani.SetBool("是否在地板上", false);
            player.ani.SetFloat("跳躍", 1);

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //加速度
            player.Setvelocity(new Vector2(h * player.movespeed, player.rig.velocity.y));
            //設定移動動畫
            player.ani.SetFloat("移動", Mathf.Abs(h));
            //腳色角度
            player.Flip(h);
            //當玩家Y軸加速度 <0 ,切換到落下狀態
            if (player.rig.velocity.y < 0)
            {
                stateMachine.SwitchState(player.player_fall);
            }
            //Reset動畫到idle狀態
            player.ani.SetFloat("移動", 0);
        }
    }
}