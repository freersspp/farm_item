using UnityEngine;
namespace PPman
{

    public class Play_defense : PlayerState
    {
        public float 防禦結束時間;
        public Play_defense(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("觸發防禦");
            player.rig.constraints = RigidbodyConstraints2D.FreezeAll; //凍結角色移動
        }

        public override void Exit()
        {
            base.Exit();
            防禦結束時間 = Time.time ; //設定防禦結束時間
            player.rig.constraints = RigidbodyConstraints2D.FreezeRotation; //凍結角色移動
        }

        public override void Update()
        {
            base.Update();
            player.ani.SetFloat("移動", 0);
            //如果防禦結束時間已經過去，則切換到待機狀態
            if(timer >= player.防禦時間)
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }
    }
}
