using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private Tilemap tilemap;

    [SerializeField] private Tile[] groundTiles;
    [SerializeField]private GameObject player;
    [SerializeField] private Vector3Int playerPosition;
    [SerializeField] private int playerX;
    [SerializeField] private int playerY;

    [SerializeField] private int maxMapX;
    [SerializeField] private int maxMapY;
    [SerializeField] private int minMapX;
    [SerializeField] private int minMapY;

    public int width;
    public int height;
    /*void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        player = GameObject.FindWithTag("Player");
        playerPosition = tilemap.WorldToCell(player.transform.position);
        playerX = playerPosition.x;
        playerY = playerPosition.y;
        

        maxMapX = playerX + width;
        maxMapY = playerY + height;

        minMapX = playerX - width;
        minMapY = playerY - height;

        Tile RandomGroundTile;
        for (int i = -width; i < width + 1; i++)
        {
            for(int j = -height; j < height + 1; j++)
            {
                RandomGroundTile = groundTiles[Random.Range(0, groundTiles.Length)];
                tilemap.SetTile(new Vector3Int(i, j, 0), RandomGroundTile);
            }
        }
    }*/

    public void Init(GameObject player)
    {
        tilemap = GetComponentInChildren<Tilemap>();

        this.player = player;
        playerPosition = tilemap.WorldToCell(player.transform.position);
        playerX = playerPosition.x;
        playerY = playerPosition.y;


        maxMapX = playerX + width;
        maxMapY = playerY + height;

        minMapX = playerX - width;
        minMapY = playerY - height;

        Tile RandomGroundTile;
        for (int i = -width; i < width + 1; i++)
        {
            for (int j = -height; j < height + 1; j++)
            {
                RandomGroundTile = groundTiles[Random.Range(0, groundTiles.Length)];
                tilemap.SetTile(new Vector3Int(i, j, 0), RandomGroundTile);
            }
        }
    }

    void FixedUpdate()
    {
        playerPosition = tilemap.WorldToCell(player.transform.position);
        UpdateMapTiles();
    }

    private void UpdateMapTiles()
    {
        int xOffset = 0;
        int yOffset = 0;

        if (playerPosition.x > playerX)
        {
            for (int i = minMapY; i < maxMapY + 1; i++)
            {
                tilemap.SetTile(new Vector3Int(minMapX, i, 0), null);
                tilemap.SetTile(new Vector3Int(maxMapX, i, 0), groundTiles[Random.Range(0, groundTiles.Length)]);
            }
            xOffset = playerPosition.x - playerX;

        }
        if (playerPosition.x < playerX)
        {
            for (int i = minMapY; i < maxMapY + 1; i++)
            {
                tilemap.SetTile(new Vector3Int(maxMapX, i, 0), null);
                tilemap.SetTile(new Vector3Int(minMapX, i, 0), groundTiles[Random.Range(0, groundTiles.Length)]);
            }
            xOffset = playerPosition.x - playerX;


        }

        if (playerPosition.y > playerY)
        {

            for (int i = minMapX; i < maxMapX + 1; i++)
            {
                tilemap.SetTile(new Vector3Int(i, minMapY, 0), null);
                tilemap.SetTile(new Vector3Int(i, maxMapY, 0), groundTiles[Random.Range(0, groundTiles.Length)]);
            }
            yOffset = playerPosition.y - playerY;

        }
        if (playerPosition.y < playerY)
        {

            for (int i = minMapX; i < maxMapX + 1; i++)
            {
                tilemap.SetTile(new Vector3Int(i, maxMapY, 0), null);
                tilemap.SetTile(new Vector3Int(i, minMapY, 0), groundTiles[Random.Range(0, groundTiles.Length)]);
            }
            yOffset = playerPosition.y - playerY;


        }

        if (xOffset != 0 && yOffset != 0)
        {
            Vector3Int corner = new Vector3Int(
                xOffset > 0 ? minMapX : maxMapX,
                yOffset > 0 ? maxMapY : minMapY,
                0
                );
            tilemap.SetTile(corner, null);

        }

        playerX = playerPosition.x;
        minMapX += xOffset;
        maxMapX += xOffset;

        playerY = playerPosition.y;
        minMapY += yOffset;
        maxMapY += yOffset;
    }
}
