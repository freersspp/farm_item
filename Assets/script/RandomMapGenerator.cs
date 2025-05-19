using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomMapGenerator : MonoBehaviour
{
    [Header("Tilemap 和地磚設定")]
    public Tilemap groundTilemap;
    public TileBase groundTile;

    [Header("Perlin Noise 參數")]
    public float noiseScale = 0.15f;
    public float heightMultiplier = 4f;
    private float noiseOffset;

    [Header("浮空平台設定")]
    public float floatingPlatformChance = 0.3f;
    public int minFloatingHeightOffset = 2;
    public int maxFloatingHeightOffset = 3;
    public int maxFloatingPlatformLength = 3;

    [Header("跳躍限制")]
    public int maxJumpHorizontal = 4;
    public int maxJumpVertical = 3;

    [Header("地形連續限制")]
    public int maxGroundStepHeight = 1;

    void Start()
    {
        GeneratePerlinMapWithFloatingPlatforms();
    }

    void GeneratePerlinMapWithFloatingPlatforms()
    {
        groundTilemap.ClearAllTiles();

        noiseOffset = Random.Range(0f, 1000f);

        int startX = -5;
        int endX = 25;
        int lastGroundHeight = 0;

        int lastPlatformX = -999;
        int lastPlatformY = -999;

        for (int x = startX; x <= endX; x++)
        {
            // 使用 Perlin Noise 取得 Y 值高度
            float perlinValue = Mathf.PerlinNoise((x + 1000) * noiseScale, noiseOffset);
            int tileHeight = Mathf.FloorToInt((perlinValue - 0.3f) * heightMultiplier);
            tileHeight = Mathf.Clamp(tileHeight, -5, 5);

            // 限制地板區塊之間高度差不要過大
            if (x > startX)
            {
                int diff = tileHeight - lastGroundHeight;
                if (Mathf.Abs(diff) > maxGroundStepHeight)
                {
                    tileHeight = lastGroundHeight + Mathf.Clamp(diff, -maxGroundStepHeight, maxGroundStepHeight);
                }
            }
            lastGroundHeight = tileHeight;

            // 建立主地板（地面）
            for (int y = -5; y <= tileHeight; y++)
            {
                groundTilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
            }

            // 若地形太低，隨機補一格平台在 Y=1
            if (tileHeight < 1 && Random.value < 0.5f)
            {
                groundTilemap.SetTile(new Vector3Int(x, 1, 0), groundTile);
            }

            // 浮空平台隨機生成
            if (Random.value < floatingPlatformChance)
            {
                int floatingY = tileHeight + Random.Range(minFloatingHeightOffset, maxFloatingHeightOffset + 1);
                floatingY = Mathf.Clamp(floatingY, tileHeight + 1, 5); // 限制浮空高度

                int dx = Mathf.Abs(x - lastPlatformX);
                int dy = Mathf.Abs(floatingY - lastPlatformY);

                // 若上一平台存在，則檢查水平與垂直跳躍限制
                if (lastPlatformX != -999 && (dx > maxJumpHorizontal || dy > maxJumpVertical))
                    continue;

                // 產生浮空平台
                int platformLength = Random.Range(1, maxFloatingPlatformLength + 1);

                for (int i = 0; i < platformLength && (x + i) <= endX; i++)
                {
                    groundTilemap.SetTile(new Vector3Int(x + i, floatingY, 0), groundTile);
                }

                lastPlatformX = x;
                lastPlatformY = floatingY;

                x += platformLength - 1; // 避免平台之間重疊
            }
        }
    }
}
