using UnityEngine;
namespace PPman
{
    public class Redpotion : Item
    {
        private Player player;

        protected override void GetItem()
        {
            base.GetItem();

            if (player == null)
            {
                player = GameObject.FindObjectOfType<Player>();
            }

            if (player != null)
            {
                player.Heal(30); // 回復30點HP
            }

            SoundManager.Instance.PlaySound(Soundtype.EatItem, 0.8f, 1.1f);
        }
    }
}
