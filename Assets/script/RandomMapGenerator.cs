using UnityEngine;
using UnityEngine.Tilemaps; // 引入 Tilemap 所需的命名空間

public class RandomMapGenerator : MonoBehaviour
{
    // 欲操作的 Tilemap（從場景中拖進來）
    public Tilemap groundTilemap;

    // 地面所使用的 Tile，可以是 RuleTile 或普通 Tile
    public TileBase groundTile;

    // 地圖的寬度（水平格數）
    public int mapWidth = 50;

    // 地圖的總高度（最大 Y 值）
    public int mapHeight = 6;

    // 預設的地面高度
    public int groundHeight = 1;

    // 遊戲開始時自動呼叫，用來生成地圖
    void Start()
    {
        GenerateRandomMap();
    }

    // 地圖生成邏輯
    void GenerateRandomMap()
    {
        // 清空之前的地圖（避免重新播放時殘留）
        groundTilemap.ClearAllTiles();

        // 記錄目前高度，用來讓地形有起伏
        int currentHeight = groundHeight;

        // 從左到右依序放置地塊
        for (int x = 0; x < mapWidth; x++)
        {
            // 每一格橫向都有機會微幅上升或下降（-1 到 +1）
            int yVariation = Random.Range(-1, 2); // -1, 0, 1 隨機
            currentHeight += yVariation;

            // 限制高度不能超過範圍（避免過高或過低）
            currentHeight = Mathf.Clamp(currentHeight, 1, mapHeight - 1);

            // 在目前高度以下填滿地面區塊
            for (int y = 0; y <= currentHeight; y++)
            {
                // 設定 Tile 到對應位置 (x, y, z)
                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
            }
        }
    }
}
