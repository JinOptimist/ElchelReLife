using Assets.LifeLogic;
using Assets.LifeLogic.Genes;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!God.Cells.Any())
        {
            var cell = CellBuilder.GetDefaultCell();
            God.Cells.Add(cell);
            Burn(cell);
        }
    }

    public void Burn(Cell cell)
    {
        Debug.Log($"Burn new one [{cell}]");
        if (God.Cells.Any(x => x.X == cell.X && x.Y == cell.Y && x != cell))
        {
            Debug.LogError("Duplicate!");
        }

        var child = Instantiate(CellTemplate);
        var lifeScript = child.GetComponent<LifeScript>();
        cell.AfterCellDie = lifeScript.Die;
        lifeScript.Cell = cell;
        lifeScript.enabled = true;

        child.transform.position = new Vector3(cell.X, 0, cell.Y);

        child.GetComponent<Rigidbody>().isKinematic = false;

        if (cell.Genome.OfType<Bite>().Any())
        {
            child.GetComponent<MeshRenderer>().material = lifeScript.PredatorMaterial;
        }
        else
        {
            child.GetComponent<MeshRenderer>().material = lifeScript.РerbivorousMaterial;
        }
    }
}
