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
    private DateTime LastTurnTime { get; set; }

    public Cell Cell { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        LastTurnTime = DateTime.Now;

        if (!God.Cells.Any())
        {
            Cell = CellBuilder.GetDefaultCell();
            God.Cells.Add(Cell);
            CellBuilder.AfterCellBurn = Burn;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((DateTime.Now - LastTurnTime).TotalSeconds > 1)
        {
            Cell.Turn();
            LastTurnTime = DateTime.Now;

            if (Cell.StoreEnegry < 0)
            {
                God.Cells.Remove(Cell);
                Destroy(this);
            }
        }
    }

    private void Burn(Cell cell)
    {
        Debug.Log($"Burn new one [{cell.X},{cell.Y}]");
        if (God.Cells.Any(x => x.X == cell.X && x.Y == cell.Y && x != cell))
        {
            Debug.LogError("Duplicate!");
        }
        var child = Instantiate(this);
        child.transform.position = new Vector3(cell.X, transform.position.y, cell.Y);
        child.Cell = cell;
    }
}
