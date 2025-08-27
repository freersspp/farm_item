using UnityEngine;
using TMPro;
namespace PPman
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private float floatSpeed = 50f;   // 冒出速度（UI 座標，所以可以用 px/s）
        [SerializeField] private float lifeTime = 1f;      // 幾秒後消失
        private TextMeshProUGUI textMesh;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        public void Setup(int damage)
        {
            textMesh.text = damage.ToString();
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            // 每禎往上飄
            transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);
        }
    }
}