namespace PPman
{
    /// <summary>
    /// 玩家落下
    /// </summary>
    public class Player_fall : PlayerState
    {
        public Player_fall(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetFloat("跳躍", -1);
        }

        public override void Exit()
        {
            base.Exit();
            player.ani.SetBool("是否在地板上", true);

        }

        public override void Update()
        {
            base.Update();
            if (player.IsGround())
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }
    }
}