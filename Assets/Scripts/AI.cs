using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    object[] tiles;
    int _currTile = 0;

    void GetBatekhaTiles()
    {
        tiles = GameObject.FindGameObjectsWithTag(Tags.Batee5a);
    }

    object GetTile()
    {
        _currTile++;
        if (_currTile == tiles.Length)
            return null;

        return tiles[_currTile - 1];
    }

    void OnNonBatee5aBroken()
    {
        // yro7 ysl7ha
    }

    void BreakBatee5a()
    {
        //yro7 y break
    }
}
