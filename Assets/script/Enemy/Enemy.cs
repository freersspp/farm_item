using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace PPman
{
    /// <summary>
    /// 敵人腳本:管理敵人資料與行為
    /// </summary>
    public class Enemy : character
    {
        [field: SerializeField]public Vector2 idleTime { get; private set; }
        [field: SerializeField] public Vector2 WalkTime { get; private set; }
        [field: SerializeField, Range(1, 5)] public float Walkspeed { get; private set; }  = 1.5f; // 遊走速度
        [field:SerializeField] public float followspeed { get; private set; } = 4f; // 追擊速度
        [field:SerializeField, Tooltip("進入攻擊距離")] public float InAttackArea { get; private set; } = 6; // 攻擊範圍
        [field: SerializeField, Tooltip("攻擊間隔時間")] public float AttackTime { get; private set; } = 1.2f; // 攻擊時間

        [SerializeField, Tooltip("敵人血條UI預製物:群組敵人血條")] private GameObject enemyPrefabHP; // 群組敵人血條預製物
        private Transform groupEnemyHP;

        /// <summary>
        /// 檢測前方有無牆壁和地上有無地板SSSSS
        /// </summary>
        [field: SerializeField] private Vector3 CheckWall = Vector3.one;
        [field: SerializeField] private Vector3 CheckWalloffset;
        [field: SerializeField] private LayerMask layerWall; 
        [field: SerializeField] private Vector3 CheckGround = Vector3.one;
        [field: SerializeField] private Vector3 CheckGroundoffset;
        [field: SerializeField] private LayerMask layerGround;

        /// <summary>
        /// 檢查前方有無玩家
        /// </summary>
        [field: SerializeField] private Vector3 CheckPlayer = Vector3.one;
        [field: SerializeField] private Vector3 CheckPlayeroffset;
        [field: SerializeField] private LayerMask layerPlayer;

        public StateMachine stateMachine { get; private set; } // 狀態機
        public Enemy_idle enemy_idle { get; private set; } // 待機狀態
        public Enemy_walk enemy_walk { get; private set; } // 遊走狀態
        public Enemy_follow enemy_follow { get; private set; } // 追擊狀態
        public Enemy_attack enemy_attack { get; private set; } // 攻擊狀態
        public Enemy_die enemy_die { get; private set; } // 死亡狀態

        public Transform player { get; private set; } // 玩家位置

        private CanvasGroup groupHP; // 敵人血條群組

        private WorktoUIpoint WorktoUIpointHP;
        [SerializeField] private Vector3 offsetHP;

        private void OnDrawGizmos()
        {
            // 繪製檢測牆壁的範圍
            Gizmos.color = new Color(1, 1, 0.6f, 0.5f);
            Gizmos.DrawWireCube(transform.position + transform.TransformDirection(CheckWalloffset), CheckWall);
            // 繪製檢測地面的範圍
            Gizmos.color = new Color(1, 1, 0.6f, 0.5f);
            Gizmos.DrawWireCube(transform.position + transform.TransformDirection(CheckGroundoffset), CheckGround);
            // 繪製檢測玩家的範圍
            Gizmos.color = new Color(0.6f, 1, 0.6f, 0.5f);
            Gizmos.DrawWireCube(transform.position + transform.TransformDirection(CheckPlayeroffset), CheckPlayer);

        }


        protected override void Awake()
        {
            base.Awake();

            player = FindClosestPlayer().transform; // 獲取玩家位置

            stateMachine = new StateMachine();
            enemy_idle = new Enemy_idle(this, stateMachine, "Idle");
            enemy_walk = new Enemy_walk(this, stateMachine, "Walk");
            enemy_follow = new Enemy_follow(this, stateMachine, "Follow");
            enemy_attack = new Enemy_attack(this, stateMachine, "Attack");
            enemy_die = new Enemy_die(this, stateMachine, "Die");
            // 設定初始狀態
            stateMachine.DefaultState(enemy_idle);

            //獲得物件"群組_敵人血條_全部"後, 在其子物件新增在prefab內的"群組_敵人血條"
            groupEnemyHP = GameObject.Find("群組_敵人血條_全部").transform; 
            Transform enemyHP = Instantiate(enemyPrefabHP, groupEnemyHP).transform;
            groupHP = enemyHP.GetComponent<CanvasGroup>(); // 獲取血條群組的CanvasGroup組件
            WorktoUIpointHP = enemyHP.GetComponent<WorktoUIpoint>(); // 獲取血條群組的WorktoUIpoint組件

            //Find("名稱")可以找到其下的子物件
            imgHP = enemyHP.Find("圖片_血條").GetComponent<Image>();
            imgHPeffect = enemyHP.Find("圖片_血條_效果").GetComponent<Image>();
        }

        private void Update()
        {
            // 更新狀態機
            stateMachine.UpdateState();
            WorktoUIpointHP.Updatepoint(transform, offsetHP); // 更新血條位置
            player = FindClosestPlayer();
        }

        private Transform FindClosestPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            Transform closest = null;
            float minDistance = Mathf.Infinity;

            foreach (GameObject obj in players)
            {
                float dist = Vector3.Distance(transform.position, obj.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    closest = obj.transform;
                }
            }

            return closest;
        }


        // 檢測前方是否有牆壁
        public bool IsWallfront()
        {
            return Physics2D.OverlapBox(transform.position + transform.TransformDirection(CheckWalloffset), CheckWall, 0, layerWall);
        }

        // 檢測前方是否有地板
        public bool IsGroundfront()
        {
            return Physics2D.OverlapBox(transform.position + transform.TransformDirection(CheckGroundoffset), CheckGround, 0, layerGround);
        }

        // 檢測前方是否有玩家
        public bool IsPlayerfront()
        {
            return Physics2D.OverlapBox(transform.position + transform.TransformDirection(CheckPlayeroffset), CheckPlayer, 0, layerPlayer);
        }

        protected override void Damage(float damage)
        {
            base.Damage(damage);
            StartCoroutine(FadeSystem.Fade(groupHP));
        }

        protected override void Die()
        {
            base.Die();
            stateMachine.SwitchState(enemy_die); // 切換到死亡狀態
            StartCoroutine(DelayFadeOut()); // 延遲淡出血條
        }

        private IEnumerator DelayFadeOut()
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(FadeSystem.Fade(groupHP, false)); // 淡出血條
        }
        

    }
}