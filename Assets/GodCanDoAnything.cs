using Assets.LifeLogic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GodCanDoAnything : MonoBehaviour
{
    public GameObject CellTemplate;

    // Start is called before the first frame update
    void Start()
    {
        CellBuilder.AfterCellBurn = Burn;

        var cell = CellBuilder.GetDefaultCell();
        God.Cells.Add(cell);
        Burn(cell);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Burn(Cell cell)
    {
        Debug.Log($"Burn new one [{cell.X},{cell.Y}]");
        if (God.Cells.Any(x => x.X == cell.X && x.Y == cell.Y && x != cell))
        {
            Debug.LogError("Duplicate!");
        }

        var child = Instantiate(CellTemplate);
        child.transform.position = new Vector3(cell.X, 0, cell.Y);
        child.GetComponent<LifeScript>().Cell = cell;
        child.GetComponent<LifeScript>().enabled = true;
    }
}
