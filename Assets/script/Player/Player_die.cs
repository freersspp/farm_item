using UnityEngine;
namespace PPman
{

    public class Player_die : PlayerState
    {
        public Player_die(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("觸發死亡");
            player.Setvelocity(Vector3.right * 0 + Vector3.up * player.rig.velocity.y); //設定GameManager.PlayerName死後不會亂動
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