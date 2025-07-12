using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 玩家走路
    /// </summary>
    public class Player_walk : playerGround
    {
        public Player_walk(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
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

            //如果玩家的水平值為0，則切換到待機狀態
            if (h == 0)
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }
    }
}