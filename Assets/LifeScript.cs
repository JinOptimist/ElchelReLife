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
    public GodCanDoAnything GodCanDoAnything;

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
        if ((DateTime.Now - LastTurnTime).TotalSeconds > 1)
        {
            if (Cell.StoreEnegry < 0)
            {
                Dead();
                return;
            }

            Cell.Turn();
            LastTurnTime = DateTime.Now;
        }

        transform.position = new Vector3(Cell.X, transform.position.y, Cell.Y);
    }

    private void Dead()
    {
        God.Cells.Remove(Cell);
        Destroy(gameObject);
    }
}
