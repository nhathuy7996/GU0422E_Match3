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
     

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = this.GetComponent<SpriteRenderer>();
        _ID = (TypeTile)Random.Range(0, (int)TypeTile.type3+1);

        if(_X - 1  > 0)
            _neighbor.Add(TileManager.Instant.getTile(_X - 1, _Y));
        if (_X + 1 < TileManager.Instant._width)
            _neighbor.Add(TileManager.Instant.getTile(_X + 1, _Y));
        if (_Y - 1 > 0)
            _neighbor.Add(TileManager.Instant.getTile(_X , _Y - 1));
        if (_Y + 1 < TileManager.Instant._heigh)
            _neighbor.Add(TileManager.Instant.getTile(_X , _Y + 1));

    }

    public void updateIndex(Vector2 index)
    {
        _X = (int)index.x;
        _Y = (int)index.y;
    }
}

public enum TypeTile
{
    type1,
    type2,
    type3
}
