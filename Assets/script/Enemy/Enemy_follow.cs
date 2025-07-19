using UnityEngine;
namespace PPman
{

    public class Enemy_follow : Enemy_state
    {
        public Enemy_follow(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // 進入跟隨狀態時的邏輯
        }
        public override void Exit()
        {
            base.Exit();
            // 離開跟隨狀態時的邏輯
        }
        public override void Update()
        {
            base.Update();
            // 跟隨狀態下的更新邏輯
        }
    }


}
