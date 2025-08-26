using Fungus;
using UnityEngine;

namespace PPman
{

    public class playerGround : PlayerState
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

            player.ani.SetBool("是否在地板上", player.IsGround());

            //如果玩家在某些條件下 並且 按空白鍵就切換到"跳躍狀態"
            if (player.canjump && player.IsGround() && Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState(player.player_jump);
            }
            //如果玩家在某些條件下 並且 按滑鼠左鍵就切換到"攻擊狀態"
            if (player.canattack && player.IsGround() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                stateMachine.SwitchState(player.player_attack);
            }
            //如果玩家在地面上並且按下左邊control就切換到"衝刺狀態"
            if (player.candash && player.IsGround() && Input.GetKeyDown(KeyCode.LeftControl))
            {
                stateMachine.SwitchState(player.player_dash);
            }
            //如果玩家在地面上並且按下滑鼠右鍵就切換到"防禦狀態"
            if (player.candefense && player.IsGround() && Input.GetKeyDown(KeyCode.Mouse1))
            {
                stateMachine.SwitchState(player.player_defense);
            }
            //如果玩家在地面上並且按下E鍵就切換到"技能2狀態"
            if (player.canskill2 && player.IsGround() && Input.GetKeyDown(KeyCode.E))
            {
                stateMachine.SwitchState(player.player_Skill2);
            }

        }
    }
}
