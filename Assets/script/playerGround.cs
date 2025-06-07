using UnityEngine;

namespace PPman
{

    public class playerGround : State
    {
        public playerGround(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
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

            //如果玩家在地面上 並且 按空白鍵就切換到"跳躍狀態"
            if (player.IsGround() && Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState(player.player_jump);
            }
            //如果玩家在地面上 並且 按滑鼠左鍵就切換到"攻擊狀態"
            if (player.IsGround() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                stateMachine.SwitchState(player.player_attack);
            }
        }
    }
}
