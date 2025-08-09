using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace PPman
{
    /// <summary>
    /// 腳色:翻面和加速度或者腳色共同功能
    /// </summary>
    public class character : MonoBehaviour
    {
        // {get; private set;}唯獨屬性:允許外部取得但不能修改
        public Animator ani { get; private set; }
        public Rigidbody2D rig { get; private set; }

        [SerializeField, Header("會讓自己受傷的物件標籤")] private string damageTag;
        [SerializeField, Range(0, 100)] protected float HPMAX = 100f; // 最大生命值
        protected float HP; // 當前生命值
        protected Image imgHP, imgHPeffect; // 生命值UI和效果

        public event Action onDie; // 死亡事件

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            rig = GetComponent<Rigidbody2D>();
            HP = HPMAX; // 初始化生命值
        }

        //2D觸發事件(碰到勾選Istrigger的物件會執行一次, "collision"會記錄碰到的物件資訊)
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (HP <= 0)
            {
                return; // 如果生命值小於等於0，則不處理碰撞
            }
            if (collision.CompareTag(damageTag))
            {
                Damage(collision.GetComponent<AttackValue>().attackPower);
            }
        }


        /// <summary>
        /// 設定加速度
        /// </summary>
        /// <param name="velocity">加速度</param>
        public void Setvelocity(Vector3 velocity)
        {
            rig.velocity = velocity;
        }

        /// <summary>
        /// 翻轉角色
        /// </summary>
        /// <param name="h">方向</param>
        public bool isFacingRight = true;
        public void Flip(float h)
        {
            if (Mathf.Abs(h) < 0.1)
            {
                return;
            }
            isFacingRight = h < 0;

            float 腳色角度 = h > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, 腳色角度, 0);
        }

        protected virtual void Damage(float damage)
        {
            StartCoroutine(HPDamageEffect(HP, damage)); // 開始生命值減少效果協程
            HP -= damage; // 減少生命值
            HPeffect(); // 更新生命值UI效果

            if (HP <= 0)
            {
                Die(); // 死亡處理
            }
        }

        private IEnumerator HPDamageEffect(float hpOriginal, float damage)
        {
            float count = 20;//執行次數
            float reduce = damage / count; // 每次減少的生命值
            for (int i = 0; i < count; i++)
            {
                hpOriginal -= reduce; // 減少生命值
                imgHPeffect.fillAmount = hpOriginal / HPMAX; // 更新生命值UI效果
                yield return new WaitForSeconds(0.04f); // 等待0.04秒
            }
        }
        protected virtual void HPeffect()
        {
            imgHP.fillAmount = HP / HPMAX; // 更新生命值UI
        }

        protected virtual void Die()
        {
            Debug.Log($"<color=red> {name}死亡</color>");
            StartCoroutine(gameobjectinactive(gameObject, 1)); // 禁用物件
            SoundManager.Instance.PlaySound(Soundtype.PlayerDie); // 播放死亡音效
            onDie?.Invoke(); // 觸發死亡事件
        }


        #region 音效處理
        private void PlaySound(Soundtype soundtype)
        {
            SoundManager.Instance.PlaySound(soundtype); // 播放受傷音效
        }

        private void PlaySoundRandomvolume(Soundtype soundtype)
        {
            SoundManager.Instance.PlaySound(soundtype, 0.8f, 1.5f); // 播放受傷音效
        }
        #endregion

        private IEnumerator gameobjectinactive(GameObject obj, float time)
        {
            yield return new WaitForSeconds(1); // 等待指定時間
            obj.SetActive(false); // 禁用物件
        }
    }
}