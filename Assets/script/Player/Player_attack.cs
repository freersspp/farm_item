using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 玩家攻擊
    /// </summary>
    public class Player_attack : PlayerState
    {
        private int attackindex;
        private float attackindexmax = 3;
        private float 攻擊結束時間;

        public Player_attack(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();

            attackindex++;
            //重製攻擊段數
            if (attackindex > attackindexmax)
            {
                attackindex = 1;
            }
            player.ani.SetFloat("攻擊段數", attackindex);
            player.ani.SetTrigger("觸發攻擊");

            //攻擊中斷時間後會reset到第一段攻擊
            if (Time.time > 攻擊結束時間 + player.攻擊中斷時間)
            {
                attackindex = 1;
            }
            player.rig.constraints = RigidbodyConstraints2D.FreezeAll; //凍結角色移動

        }

        public override void Exit()
        {
            base.Exit();

            攻擊結束時間 = Time.time;
            player.rig.constraints = RigidbodyConstraints2D.FreezeRotation; //凍結角色移動

        }

        public override void Update()
        {
            base.Update();
            //Debug.Log($"<color=yellow>計時器: {timer}</color>");
            //Reset動畫到idle狀態
            player.ani.SetFloat("移動", 0);

            //攻擊完畢後，切換到待機狀態
            if (timer >= player.攻擊動畫時間[attackindex - 1])
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }
    }
}