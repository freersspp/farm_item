using System.Collections;
using UnityEngine;
namespace PPman
{
    public class Enemy_die : Enemy_state
    {
        public Enemy_die(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            enemy.ani.SetTrigger("觸發死亡");
            enemy.Setvelocity(Vector3.zero); // 停止移動
            // 0.5 秒後將敵人關閉（呼叫協程）
            enemy.StartCoroutine(DeactivateAfterDelay(0.5f));
        }

        private IEnumerator DeactivateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            enemy.gameObject.SetActive(false);
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