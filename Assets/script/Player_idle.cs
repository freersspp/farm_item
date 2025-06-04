namespace PPman
{
    /// <summary>
    /// 玩家待機
    /// </summary>
    public class Player_idle : State
    {
        public Player_idle(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
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

            //如果玩家的水平值不為0，則切換到走路狀態
            if (h != 0)
            {
                stateMachine.SwitchState(player.player_walk);
            }


        }
    }
}