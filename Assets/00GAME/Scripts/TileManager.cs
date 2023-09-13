using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HuynnLib;
using System;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] Camera _mainCam;
    public int _width, _heigh;
    [SerializeField] Tile _tilePrefab;
    List<Tile> _tiles = new List<Tile>();
    Tile _tile1, _tile2;
    // Start is called before the first frame update
    void Start()
    {
        this.InitGrid();
    }

    // Update is called once per frame
    void Update()
    {
        this.checkSelectTile1();
        this.checkSelectTile2();
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
                tmpT.name += x + "_" + y;
                _tiles.Add(tmpT);
            }
        }


        _mainCam.transform.position = new Vector3((_width - 1)/2f,(_heigh-1)/2f,-10f);
        float sizeCam = _heigh >= _width ? _heigh / 2f : (_width / _mainCam.aspect) /2f;
        _mainCam.orthographicSize = sizeCam;
    }


    public void checkSelectTile1()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

        if(hit.collider != null)
        {
            Tile selectTile = hit.collider.GetComponent<Tile>();
            if(_tile1 == null)
            {
                _tile1 = selectTile;
                _tile1.SpriteRenderer.color = Color.red;
                return;
            }
        }
    }


    public void checkSelectTile2()
    {
        if (!Input.GetMouseButton(0) )
            return;

        if (_tile1 == null)
            return;

        if (_tile2 != null)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

        if (hit.collider == null)
            return;
        
        Tile selectTile = hit.collider.GetComponent<Tile>();
        if (_tile1 != null && _tile1.GetInstanceID() == selectTile.GetInstanceID())
        { 
           
            return;
        }

        if (Mathf.Abs(selectTile._X - _tile1._X) + Mathf.Abs(selectTile._Y - _tile1._Y) == 1)
        {
            _tile2 = selectTile;
            _tile2.SpriteRenderer.color = Color.green;
        }

        if (_tile2 == null)
        {
            _tile1.SpriteRenderer.color = Color.white;
            _tile1 = null;
            return;
        }


        this.SwapTile(_tile1, _tile2, () =>
        {
            if (!checkMatch3())
            {
                this.SwapTile(_tile1, _tile2);

            }

            _tile1.SpriteRenderer.color = Color.white;
            _tile2.SpriteRenderer.color = Color.white;
            _tile1 = null;
            _tile2 = null;
        });
       
    }


    void SwapTile(Tile tile1, Tile tile2, Action callBack = null)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector2 index1 = new Vector2(_tile1._X, _tile1._Y);
        Vector3 pos2 = tile2.transform.position;
        Vector2 index2 = new Vector2(_tile2._X, _tile2._Y);

        tile1.transform.DOMove(pos2, 0.2f).Play();

        tile2.transform.DOMove(pos1, 0.2f).OnComplete(() =>
        {
            tile1.updateIndex(index2);
            tile2.updateIndex(index1);
            callBack?.Invoke();
        }).Play();
    }

    bool checkMatch3()
    {
        if (checkHorizontalLeft(_tile1) + checkHorizontalRight(_tile1) >= 2)
            return true;

        if (checkVerticalUp(_tile1) + checkVerticalDown(_tile1) >= 2)
            return true;

        if (checkHorizontalLeft(_tile2) + checkHorizontalRight(_tile2) >= 2)
            return true;

        if (checkVerticalUp(_tile2) + checkVerticalDown(_tile2) >= 2)
            return true;

        return false;
    }


    int checkHorizontalLeft(Tile tile)
    {
        if (tile._neighbor[0] == null)
            return 0;

        if(tile._ID == tile._neighbor[0]._ID)
        {
           return this.checkHorizontalLeft(tile._neighbor[0]) + 1;
        }
        return 0;
    }

    int checkHorizontalRight(Tile tile)
    {
        if (tile._neighbor[1] == null)
            return 0;

        if (tile._ID == tile._neighbor[1]._ID)
        {
            return this.checkHorizontalRight(tile._neighbor[1]) + 1;
        }
        return 0;
    }

    int checkVerticalUp(Tile tile)
    {
        if (tile._neighbor[3] == null)
            return 0;

        if (tile._ID == tile._neighbor[3]._ID)
        {
            return this.checkVerticalUp(tile._neighbor[3]) + 1;
        }
        return 0;
    }

    int checkVerticalDown(Tile tile)
    {
        if (tile._neighbor[2] == null)
            return 0;

        if (tile._ID == tile._neighbor[2]._ID)
        {
            return this.checkVerticalDown(tile._neighbor[2]) + 1;
        }
        return 0;
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
