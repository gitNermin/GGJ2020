using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    GameObject[] tiles;
    int _currTile = 0;

    Queue<Action<Tile>> aiAction;

    private void Start()
    {
        GetBatee5aTiles();
    }

    void GetBatee5aTiles()
    {
        tiles = GameObject.FindGameObjectsWithTag(Tags.Batee5a);
    }

    GameObject GetBatee5a()
    {
        _currTile++;
        if (_currTile == tiles.Length)
            return null;

        return tiles[_currTile - 1];
    }

    public void OnNonBatee5aBroken(Tile nonbatee5a)
    {
        if (nonbatee5a == null)
            return;

        aiAction.Enqueue(OnNonBatee5aBroken);
        Vector3 tilepos = nonbatee5a.transform.position;
        transform.DOMoveX(tilepos.x, 1).OnComplete(() => transform.DOMoveZ(tilepos.z, 1).OnComplete(() => nonbatee5a.Fix()));
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
        GameObject tile = GetBatee5a();
        if (tile == null)
            return;

        BreakBatee5a(tile.GetComponent<Tile>());
    }
}
