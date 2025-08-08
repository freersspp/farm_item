using UnityEngine;
namespace PPman
{


    public class PlayerState : State
    {
        protected Player player;
        public PlayerState(Player _player, StateMachine _statemachine, string _name)
        {
            player = _player;
            stateMachine = _statemachine;
            name = _name;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log($"<color=blue>進入<{name}>狀態</color>");
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