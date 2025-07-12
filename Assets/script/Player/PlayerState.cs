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

    }
}