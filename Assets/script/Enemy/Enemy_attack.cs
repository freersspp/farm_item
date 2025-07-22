using UnityEngine;
namespace PPman
{

    public class Enemy_attack : Enemy_state
    {

        private int attackindex;
        private float attackindexmax = 2;
        private float 攻擊結束時間;
        public Enemy_attack(Enemy _enemy, StateMachine _stateMachine, string _name) : base(_enemy, _stateMachine, _name)
        {
        }
        public override void Enter()
        {
            base.Enter();
            // 進入攻擊狀態時的邏輯

            attackindex++;
            //重製攻擊段數
            if (attackindex > attackindexmax)
            {
                attackindex = 1;
            }
            enemy.ani.SetFloat("攻擊段數", attackindex);
            enemy.ani.SetTrigger("觸發攻擊");
            enemy.ani.SetFloat("移動", 0); //重置移動動畫
            enemy.rig.constraints = RigidbodyConstraints2D.FreezeAll; //凍結角色移動

        }

        public override void Exit()
        {
            base.Exit();
            // 離開攻擊狀態時的邏輯

            enemy.rig.constraints = RigidbodyConstraints2D.FreezeRotation; //凍結角色移動
        }
        public override void Update()
        {
            base.Update();
            // 攻擊狀態下的更新邏輯

            if(timer >= enemy.AttackTime)
            {
                //攻擊完畢後，切換到待機狀態
                stateMachine.SwitchState(enemy.enemy_walk);
            }
        }
    }


}
