using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [System.Serializable]
    public struct ColorToPrefab
    {
        public Color color;
        public GameObject prefab;
    }

    [SerializeField] private SpriteRenderer pixelSpriteRenderer;
    [SerializeField] private ColorToPrefab[] colorMappings;

    private Texture2D pixelTexture;
    public Vector2 mapHalfSize;
    public GridGraph map;

    private void Awake()
    {
        if (pixelSpriteRenderer == null)
            pixelSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        pixelTexture = pixelSpriteRenderer.sprite.texture;

        map = new GridGraph(pixelTexture.width, pixelTexture.height);
        map.Walls = new List<Vector2>();
        map.Forests = new List<Vector2>();
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        // Setup
        pixelSpriteRenderer.enabled = false;
        
        mapHalfSize = new Vector2(pixelTexture.width, pixelTexture.height) * 0.5f;

        void SpawnTileAt(int x, int y)
        {
            Color pixelColor = pixelTexture.GetPixel(x, y);
            if (pixelColor.a == 0)
                return;

            for (int i = 0; i < colorMappings.Length; i++)
            {
                if (colorMappings[i].color != pixelColor)
                    continue;

                Instantiate(colorMappings[i].prefab, transform).transform.localPosition =
                    new Vector2(
                        x - mapHalfSize.x + 0.5f,
                        y - mapHalfSize.y + 0.5f);

                map.Walls.Add(new Vector2(x, y));
            }
        }

        // Spawn Tiles
        for (int x = 0; x < pixelTexture.width; x++)
            for (int y = 0; y < pixelTexture.height; y++)
                SpawnTileAt(x, y);
    }
}
