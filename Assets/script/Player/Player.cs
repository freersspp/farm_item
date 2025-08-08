using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace PPman
{
    /// <summary>
    /// 儲存玩家資料與基本功能
    /// </summary>
    public class Player : character
    {
        #region 變數
        [field: Header("基本控制")]
        [field: SerializeField, Range(0, 20)] public float movespeed { get; private set; } = 5f;
        [field: SerializeField, Range(0, 20)] public float jumpForce { get; private set; } = 10f;
        [field: SerializeField, Range(0, 3)] public float 攻擊中斷時間 { get; private set; } = 1;
        [field: SerializeField] public float[] 攻擊動畫時間 { get; private set; }
        [field: SerializeField, Range(0, 20)] public float 衝刺速度 { get; private set; } = 12f;
        [field: SerializeField, Range(0, 3)] public float 衝刺時間 { get; private set; } = 0.3f;
        [field: SerializeField] public float 防禦時間 { get; private set; }  //防禦時間
        [field: SerializeField] public GameObject bulletPrefab; //子彈預製體
        [field: SerializeField] public Transform firePoint; //發射位置
        [field: SerializeField] public float bulletSpeed = 10f; //子彈速度
        [field:SerializeField] public float Skill2time { get; private set; } 




        public bool canmove { get; set; } = false; //是否可以移動
        public bool canjump { get; set; } = false; //是否可以跳躍
        public bool canattack { get; set; } = false; //是否可以攻擊 
        public bool candash { get; set; } = false; //是否可以衝刺
        public bool candefense { get; set; } = false; //是否可以防禦
        public bool canskill2 { get; set; } = false; //是否可以使用技能2


        [Header("檢查地板")]
        [SerializeField] private Vector3 確認地板尺寸 = Vector3.one;
        [SerializeField] private Vector3 確認地板尺寸的位置;
        [SerializeField] private LayerMask 可以跳得圖層;

        [Header("粒子效果")]
        public GameObject dashFireTrail;

        #endregion

        #region 狀態資料
        public StateMachine stateMachine { get; private set; }
        public Player_idle player_idle { get; private set; }
        public Player_walk player_walk { get; private set; }
        public Player_jump player_jump { get; private set; }
        public Player_fall player_fall { get; private set; }
        public Player_attack player_attack { get; private set; }
        public Player_dash player_dash { get; private set; }
        public Player_defense player_defense { get; private set; }
        public Player_die player_die { get; private set; }
        public Player_skill1 player_Skill1 { get; private set; }
        public Player_skill2 player_Skill2 { get; private set; }


        #endregion

        private WorktoUIpoint WorktoUIpointHP; // 用於將世界座標轉換為UI座標
        [SerializeField] private Vector3 offsetHP; // 血條UI的偏移量
        private CanvasGroup PlayerHP; // 玩家血條UI的CanvasGroup
        private CanvasGroup BlackImg; // 黑色背景的CanvasGroup

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + 確認地板尺寸的位置, 確認地板尺寸);
        }

        protected override void Awake()
        {
            base.Awake();

            //實例化狀態機
            stateMachine = new StateMachine();
            //初始化各個狀態, this在這邊指得是"Player" 
            player_idle = new Player_idle(this, stateMachine, "待機");
            player_walk = new Player_walk(this, stateMachine, "走路");
            player_jump = new Player_jump(this, stateMachine, "跳躍");
            player_fall = new Player_fall(this, stateMachine, "落下");
            player_attack = new Player_attack(this, stateMachine, "攻擊");
            player_dash = new Player_dash(this, stateMachine, "衝刺");
            player_defense = new Player_defense(this, stateMachine, "防禦");
            player_die = new Player_die(this, stateMachine, "死亡");
            player_Skill1 = new Player_skill1(this, stateMachine, "技能1");
            player_Skill2 = new Player_skill2(this, stateMachine, "技能2");

            //設定狀態機的"待機"為預設狀態
            stateMachine.DefaultState(player_idle);
            //獲取血條UI的WorktoUIpoint組件
            WorktoUIpointHP = GameObject.Find("群組_玩家血條").GetComponent<WorktoUIpoint>();
            PlayerHP = GameObject.Find("群組_玩家血條").GetComponent<CanvasGroup>();

            imgHP = GameObject.Find("圖片_血條").GetComponent<Image>();
            imgHPeffect = GameObject.Find("圖片_血條_效果").GetComponent<Image>();
            BlackImg = GameObject.Find("圖片_黑色布幕").GetComponent<CanvasGroup>();

        }

        private void Update()
        {
            //更新狀態機
            stateMachine.UpdateState();
            WorktoUIpointHP.Updatepoint(transform, offsetHP); // 更新血條位置

        }

        /// <summary>
        /// 確認是否在地板上
        /// </summary>
        /// <returns></returns>
        public bool IsGround()
        {
            return Physics2D.OverlapBox(transform.position + 確認地板尺寸的位置, 確認地板尺寸, 0, 可以跳得圖層);
        }

        public void SwitchControl(bool cancontrol)
        {
            rig.velocity = Vector3.zero; //將玩家停止
            stateMachine.SwitchState(player_idle); //切換到待機狀態
            canmove = cancontrol;
            canjump = cancontrol;
            canattack = cancontrol;
        }

        protected override void Damage(float damage)
        {
            base.Damage(damage);
            StartCoroutine(FadeSystem.Fade(PlayerHP)); // 開始血條淡入效果協程
            CameraManager.Instance.StartShake(3, 4, 0.2f); // 相機震動效果
            SoundManager.Instance.PlaySound(Soundtype.PlayerHurt, 0.8f, 1.5f); // 播放玩家受傷音效
        }

        protected override void Die()
        {
            base.Die();
            stateMachine.SwitchState(player_die); //切換到死亡狀態
            StartCoroutine(DelayfadeinBlack()); // 開始黑色背景淡入效果協程
            CameraManager.Instance.StartShake(4, 5, 0.3f); // 相機震動效果
            SoundManager.Instance.PlaySound(Soundtype.PlayerDie); // 播放玩家死亡音效
        }

        private IEnumerator DelayfadeinBlack()
        {
            yield return new WaitForSeconds(1f); // 等待1秒
            StartCoroutine(FadeSystem.Fade(BlackImg)); // 開始黑色背景淡入效果協程
        }
        public void ShootProjectile()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            // 根據角色面向方向決定子彈飛行方向
            if (isFacingRight)
            {
                bulletScript.direction = Vector2.left;
                bullet.transform.localScale = new Vector3(-1, 1, 1); // 子彈圖形也反轉
            }
            else
            {
                bulletScript.direction = Vector2.right;
                bullet.transform.localScale = new Vector3(1, 1, 1);
            }
        }


    }
}
