using UnityEngine;
namespace PPman
{

    public class Enemy_attack : Enemy_state
    {
        public Enemy_attack(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // 進入攻擊狀態時的邏輯
        }
        public override void Exit()
        {
            base.Exit();
            // 離開攻擊狀態時的邏輯
        }
        public override void Update()
        {
            base.Update();
            // 攻擊狀態下的更新邏輯
        }
    }


}
