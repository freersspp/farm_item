using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToScene : MonoBehaviour
{
    public string sceneName; // 要傳送到的場景名稱

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 確保只有GameManager.PlayerName觸發
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
