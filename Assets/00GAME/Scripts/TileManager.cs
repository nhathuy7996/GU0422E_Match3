using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] int _width, _heigh;
    [SerializeField] Tile _tilePrefab;
    List<Tile> _tiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        this.InitGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGrid()
    {
        for (int x = 0; x< _width; x++)
        {
            for (int y = 0; y < _heigh; y++)
            {
                Tile tmpT = Instantiate(_tilePrefab,new Vector3(x,y),Quaternion.identity);
                tmpT._X = x;
                tmpT._Y = y;
                _tiles.Add(tmpT);
            }
        }
    }

    public Tile getTile(int x, int y)
    {
        foreach (Tile t in _tiles)
        {
            if (t._X == x && t._Y == y)
                return t;
        }

        return null;
    }
}
