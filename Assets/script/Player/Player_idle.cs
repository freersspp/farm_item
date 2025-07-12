namespace PPman
{
    /// <summary>
    /// 玩家待機
    /// </summary>
    public class Player_idle : playerGround
    {
        public Player_idle(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetFloat("移動", 0);
            player.rig.constraints = UnityEngine.RigidbodyConstraints2D.FreezeAll; //凍結旋轉
            
        }

        public override void Exit()
        {
            base.Exit();
            player.rig.constraints = UnityEngine.RigidbodyConstraints2D.FreezeRotation; //凍結旋轉
        }

        public override void Update()
        {
            base.Update();
            if (!player.canmove)
            {
                return;
            }


            //如果玩家的水平值不為0，則切換到走路狀態
            if (h != 0)
            {
                stateMachine.SwitchState(player.player_walk);
            }


        }
    }
}