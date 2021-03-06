﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    void Awake()
    {
        Safe = true;
        StartCoroutine(FindPosition());
        Audio = GetComponent<AudioSource>();
    }

    //Tile script, used to build up the grid that pieces traverse
    public int PosY, PosX = 0;
    public Light l;
    public CustomPiece Occupier;
    public bool Safe;

    public List<CustomPiece> Graves;

    public AudioSource Audio;

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
        if(piece.Pos)
            piece.Pos.Exit(piece);

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

    void TileSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Game.M.Selected && !Game.M.TargetTile && !Game.M.SearchingPiece && !Game.M.SearchingPos && l.enabled)
            {
                Game.M.TargetTile = this;
            }

            if (Game.M.NeedPos && Game.M.SearchingPos && l.enabled)
            {
                Game.M.AbilityPosition = this;
                Game.M.SearchingPos = false;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (Occupier)
            Occupier.Show();
    }

    private void OnMouseOver()
    {
        if (Occupier)
            Occupier.Select();
            
        TileSelect();
    }

    private void OnMouseExit()
    {
        if (Occupier)
            Occupier.Deselect();
    }
}
