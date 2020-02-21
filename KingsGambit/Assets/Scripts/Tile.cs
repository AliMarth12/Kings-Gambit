﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Tile script, used to build up the grid that pieces traverse

    public int PosY, PosX = 0;
    public Light l;
    public CustomPiece Occupier;
    public bool Safe;

    public List<CustomPiece> Graves;

    public AudioSource Audio;

    void Awake()
    {
        Safe = true;
        StartCoroutine(FindPosition());
        Audio = GetComponent<AudioSource>();
    }
    IEnumerator FindPosition()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (transform.position == Grid.M.GridPos[i, j])
                {
                    PosX = i;
                    PosY = j;
                    Grid.M.Tiles[i, j] = this;
                }
            }
        }
    }

    public void Enter(Piece piece)
    {
        Occupier = piece.GetComponent<CustomPiece>();
        Occupier.Pos = this;
        Occupier.PosY = PosY;
        Occupier.PosX = PosX;
    }

    public void Exit(Piece p)
    {
        if(Occupier)
            if(Occupier.Equals(p))
                Occupier = null;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {

            if (Game.M.Selected && !Game.M.TargetTile && !Game.M.SearchingPiece && !Game.M.SearchingPos && l.enabled)
            {
                Game.M.TargetTile = this;
            }


            if (Game.M.SearchingPiece && !Game.M.NeedPos)
            {
                if (Occupier && Game.M.Selected.TargetValid(Occupier) == "Valid")
                {
                    Game.M.AbilityTarget = Occupier;
                    Game.M.SearchingPiece = false;
                    Game.M.SearchingPos = true;
                }

            }
            else if (Game.M.NeedPos && Game.M.SearchingPos && l.enabled)
            {
                Game.M.AbilityPosition = this;
                Game.M.SearchingPos = false;
            }

        }

    }
}
