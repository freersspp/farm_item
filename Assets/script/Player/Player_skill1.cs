using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 玩家技能1的腳本
    /// </summary>

    public class Player_skill1 : PlayerState
    {
        public Player_skill1(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.ani.SetTrigger("skill1");

        }
        public override void Exit()
        {
            base.Exit();
            // 在這裡添加技能1的退出邏輯
        }
        public override void Update()
        {
            base.Update();
            
        }
    }
}



