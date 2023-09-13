using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int _X = 0;
    public int _Y = 0;

    public TypeTile _ID = TypeTile.type1;

    public SpriteRenderer SpriteRenderer;


    public List<Tile> _neighbor = new List<Tile>();

    Color _originColor;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer = this.GetComponent<SpriteRenderer>();
        _ID = (TypeTile)Random.Range(0, (int)TypeTile.type3+1);

        switch (_ID)
        {
            case TypeTile.type1:
                SpriteRenderer.color = Color.yellow;
               
                break;

            case TypeTile.type2:
                SpriteRenderer.color = Color.blue;
                break;

            case TypeTile.type3:
                SpriteRenderer.color = Color.grey;
                break;
        }

        _originColor = SpriteRenderer.color;

        for (int i = 0; i< 4; i++)
        {
            _neighbor.Add(null);
        }
    }


    public void updateIndex(Vector2 index)
    {
        _X = (int)index.x;
        _Y = (int)index.y;

        updateIndex();
    }

    public void updateIndex()
    {
        _neighbor[0] = (TileManager.Instant.getTile(_X - 1, _Y));

        _neighbor[1] = (TileManager.Instant.getTile(_X + 1, _Y));

        _neighbor[2] = (TileManager.Instant.getTile(_X, _Y - 1));

        _neighbor[3] = (TileManager.Instant.getTile(_X, _Y + 1));

    }

    public void resetColor()
    {
        SpriteRenderer.color = _originColor;
    }
}

public enum TypeTile
{
    type1,
    type2,
    type3
}
