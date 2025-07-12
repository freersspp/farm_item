using UnityEngine;
namespace PPman
{

    public class Play_defense : PlayerState
    {
        public Play_defense(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("觸發防禦");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
