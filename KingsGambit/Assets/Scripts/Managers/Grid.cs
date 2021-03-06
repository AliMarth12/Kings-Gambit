﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private void Awake()
    {
        if (M == null)
        {
            M = this;
        }
        else if (M != this)
        {
            Destroy(this);
        }

        SetGridCoords();
    }


    //Script to convert world space locations to a 2D array of tiles

    public static Grid M;

    public Vector3[,] GridPos = new Vector3[8,8];
    public Tile[,] Tiles = new Tile[8, 8];


    void SetGridCoords()
    {
        Vector3 tempCoord = new Vector3(-14f,0f,14f);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GridPos[j,i] = tempCoord;
                tempCoord.x += 4f;
            }
            tempCoord.x = -14f;
            tempCoord.z -= 4f;
        }
    }

    public int Distance(Tile a, Tile b)
    {
        return highest(Mathf.Abs(a.PosX - b.PosX), Mathf.Abs(a.PosY - b.PosY));
    }

    int highest(int a, int b)
    {
        if (a > b)
            return a;
        else
            return b;
    }
}