using UnityEngine;

namespace PPman
{
    public class Bullet : MonoBehaviour
    {
        public Vector2 direction = Vector2.right; // 預設向右
        public float speed = 10f;

        void Update()
        {
            transform.position += (Vector3)(direction.normalized * speed * Time.deltaTime);
            transform.Rotate(0, 0, 720 * Time.deltaTime); // 加個旋轉特效
            Debug.Log("Direction: " + direction);
            SoundManager.Instance.PlaySound(Soundtype.skill2, 0.8f, 1.5f); // 播放子彈音效
        }

        private void OnEnable()
        {
            // 自動在啟用後 1.5 秒消失（也可改為物件池回收）
            Destroy(gameObject, 0.3f);
        }
    }
}