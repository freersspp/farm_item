using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 玩家攻擊
    /// </summary>
    public class Player_attack : State
    {
        private int 當前攻擊段數;
        private float 攻擊最大段數 = 3;
        private float 攻擊結束時間;

        public Player_attack(Player _player, StateMachine _statemachine, string _name) : base(_player, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //攻擊中斷時間後會reset到第一段攻擊
            if (Time.deltaTime > 攻擊結束時間 + player.攻擊中斷時間)
            {
                攻擊最大段數 = 0;
            }

            當前攻擊段數++;

            //重製攻擊段數
            if (當前攻擊段數 > 攻擊最大段數)
            {
                當前攻擊段數 = 1;
            }

            player.ani.SetFloat("攻擊段數", 當前攻擊段數);
            player.ani.SetTrigger("觸發攻擊");
        }

        public override void Exit()
        {
            base.Exit();

            攻擊結束時間 = Time.deltaTime;

        }

        public override void Update()
        {
            base.Update();
            //Debug.Log($"<color=yellow>計時器: {timer}</color>");

            //Reset動畫到idle狀態
            player.ani.SetFloat("移動", 0);

            //攻擊完畢後，切換到待機狀態
            if (timer >= player.攻擊動畫時間[當前攻擊段數 - 1])
            {
                stateMachine.SwitchState(player.player_idle);
            }
        }
    }
}