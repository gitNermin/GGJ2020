using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AI : MonoBehaviour
{
    List<GameObject> tiles = new List<GameObject>();
    Tile _currTile;
    CharacterAnimation _myAnimation;
    public static UnityEvent OnTilesFinished = new UnityEvent();

    Queue<Action<Tile>> aiAction = new Queue<Action<Tile>>();

    private IEnumerator Start()
    {
        _myAnimation = GetComponent<CharacterAnimation>();
        Tile.OnTileBroke.AddListener(OnTileBroken);
        Tile.OnTileFixed.AddListener(OnTileFixed);
        yield return new WaitForSeconds(2);
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
        {
            OnTilesFinished.Invoke();
            return null;
        }

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
            _myAnimation.Walk();

            if (tilepos.x != transform.position.x)
            {
                MoveX(tilepos, () => { tile.Fix(); _myAnimation.Break(); });
            }
            else if (tilepos.z != transform.position.z)
            {
                MoveZ(tilepos.z, () => { tile.Fix(); _myAnimation.Break(); });
            }
            //transform.DOMoveX(tilepos.x, 1).OnComplete(() => { transform.forward = (tilepos.z - transform.position.z) * Vector3.forward; transform.DOMoveZ(tilepos.z, 1).OnComplete(() => { tile.Fix(); _myAnimation.Break(); }); });
        }
    }


    void MoveX(Vector3 position, UnityAction onFinished)
    {
        transform.forward = (position.x - transform.position.x) * Vector3.right;
        transform.DOMoveX(position.x, 1).OnComplete(() =>
        {
            if(transform.position.z != position.z)
            {
                MoveZ(position.z, onFinished);
            }
            else
            {
                onFinished();
            }
        });
    }

    void MoveZ(float zPosition, UnityAction onFinished)
    {
        transform.forward = (zPosition - transform.position.z) * Vector3.forward;
        transform.DOMoveZ(zPosition, 1).OnComplete(()=>
        {
            onFinished();
        });
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

    void BreakBatee5a(Tile tile)
    {
        if (tile == null)
            return;

        aiAction.Enqueue(BreakBatee5a);
        Vector3 tilepos = tile.transform.position;
        _myAnimation.Walk();
        if (tilepos.x != transform.position.x)
        {
            MoveX(tilepos, () => { tile.Break(); _myAnimation.Break(); });
        }
        else if (tilepos.z != transform.position.z)
        {
            MoveZ(tilepos.z, () => { tile.Break(); _myAnimation.Break(); });
        }
        //transform.forward = (tilepos.x - transform.position.x) * Vector3.right;
        //transform.DOMoveX(tilepos.x, 1).OnComplete(() => { transform.forward = (tilepos.z - transform.position.z) * Vector3.forward; transform.DOMoveZ(tilepos.z, 1).OnComplete(() => { batee5a.Break(); _myAnimation.Break(); }); }); 
    }


    public void MoveToNextTile()
    {
        Tile tile = GetBatee5a();
        if (tile == null)
            return;

        BreakBatee5a(tile);
    }
    bool _isWinner = false;
    private void Update()
    {
        if(!_isWinner && tiles.Count <= 0)
        {
            GameObject.FindGameObjectWithTag(Tags.Winner).SetActive(true);
            _isWinner = true;
        }
    }
}
