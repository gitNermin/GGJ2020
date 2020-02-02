using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    List<GameObject> tiles = new List<GameObject>();
    Tile _currTile;

    Queue<Action<Tile>> aiAction = new Queue<Action<Tile>>();

    private void Start()
    {
        Tile.OnTileBroke.AddListener(OnTileBroken);
        Tile.OnTileFixed.AddListener(OnTileFixed);
        GetBatee5aTiles();
        BreakBatee5a(GetBatee5a());
    }

    void GetBatee5aTiles()
    {
        tiles = GameObject.FindGameObjectsWithTag(Tags.Batee5a).ToList();
    }

    Tile GetBatee5a()
    {
        if (tiles.Count == 0)
            return null;

        _currTile = tiles[0].GetComponent<Tile>();
        if (_currTile._myState == Tile.State.Broken)
            return GetBatee5a();

        return _currTile;
    }

    void OnTileBroken(Tile tile)
    {
        if (tile == null)
            return;

        aiAction.Enqueue(OnTileBroken);
        if (tile.IsBatee5a)
        {
            tiles.Remove(tile.gameObject);
            if (tile.Equals(_currTile))
                MoveToNextTile();
        }
        else
        {
            Vector3 tilepos = tile.transform.position;
            transform.DOMoveX(tilepos.x, 1).OnComplete(() => transform.DOMoveZ(tilepos.z, 1).OnComplete(() => tile.Fix()));
        }
    }

    void OnTileFixed(Tile tile)
    {
        aiAction.Enqueue(OnTileFixed);
        if (tile.IsBatee5a)
        {
            tiles.Add(tile.gameObject);
        }
        else
        {
        }
    }

    void BreakBatee5a(Tile batee5a)
    {
        if (batee5a == null)
            return;

        aiAction.Enqueue(BreakBatee5a);
        Vector3 tilepos = batee5a.transform.position;
        transform.DOMoveX(tilepos.x, 1).OnComplete(() => transform.DOMoveZ(tilepos.z, 1).OnComplete(() => batee5a.Break()));
    }


    public void MoveToNextTile()
    {
        Tile tile = GetBatee5a();
        if (tile == null)
            return;

        BreakBatee5a(tile);
    }
}
