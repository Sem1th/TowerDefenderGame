using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material MainMat, CanMat, CantMat;
    public GameObject TowerPrefab;
    public bool CanBuild;


    private Renderer render;

    private ResourceManager rm;

    
    // Start is called before the first frame update
    private void Start()
    {
        rm = FindObjectOfType<ResourceManager>();
        render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if(CanBuild && rm.Gold >= rm.TowerCost)
        {
            Instantiate(TowerPrefab, transform.position, Quaternion.Euler(0, Random.Range(0,360), 0));
            CanBuild = false;
            rm.BuildTower();
        }
    }

    private void OnMouseOver()
    {
        if(CanBuild)
            render.material = CanMat;
        else
            render.material = CantMat;
    }

    private void OnMouseExit()
    {
        render.material = MainMat;
    }
}
