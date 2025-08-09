using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 道具類別
    /// </summary>
    public class Item : MonoBehaviour
    {
        [SerializeField, Header("吃道具距離"), Range(0,1)]private float eatDistance = 0.8f;
        [SerializeField, Header("延遲吃道具時間")]private float eatDelaytime;
        private Transform Player;
        private bool active;
        private void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            Invoke(nameof(Active), eatDelaytime);
        }

        private void Update()
        {
            if (!active) return;
            EatItem();
        }
        private void Active()
        {
            active = true;           
        }

        private void EatItem()
        {
            float distance = Vector2.Distance(Player.position, transform.position);

            if (distance < eatDistance)
            {
                GetItem();
                Destroy(gameObject);
            }

        }
        protected virtual void GetItem()
        {
            
        }

    }
}