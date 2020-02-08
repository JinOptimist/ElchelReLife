using Assets;
using Assets.LifeLogic;
using Assets.LifeLogic.Genes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public float TimeOfTurn = 1000;
    public int Border = 12;
    public GodCanDoAnything GodCanDoAnything;

    public Material РerbivorousMaterial;
    public Material PredatorMaterial;

    public Cell Cell { get; set; }

    private DateTime LastTurnTime { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        LastTurnTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if ((DateTime.Now - LastTurnTime).TotalMilliseconds > TimeOfTurn)
        {
            if (Cell.StoreEnegry < 0
                || Cell.LifePoint < 1
                || CellOutOfBorder())
            {
                if (Cell.StoreEnegry < 0)
                {
                    Debug.Log($"ZeroEnergy: {Cell}");
                }

                Die();
                return;
            }

            Cell.Turn();
            //Debug.Log(Cell);
            LastTurnTime = DateTime.Now;
        }

        transform.position = new Vector3(Cell.X, transform.position.y, Cell.Y);
    }

    private bool CellOutOfBorder()
    {
        return Cell.X < -Border
            || Cell.Y < -Border
            || Cell.X > Border
            || Cell.Y > Border;
    }

    public void Die()
    {
        God.Cells.Remove(Cell);
        Destroy(gameObject);
    }
}
