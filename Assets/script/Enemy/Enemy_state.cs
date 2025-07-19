using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 敵人腳本:管理敵人資料與行為
    /// </summary>

    public class Enemy_state : State
    {
        protected Enemy enemy;
        public Enemy_state(Enemy _enemy, StateMachine _stateMachine, string _name)
        {
            enemy = _enemy;
            stateMachine = _stateMachine;
            enemy.name = _name;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log($"<color=green>進入<{name}>狀態</color>");
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
