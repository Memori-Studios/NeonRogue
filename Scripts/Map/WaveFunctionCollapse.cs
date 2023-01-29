using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveFunctionCollapse : MonoBehaviour
{
    public GameObject allProtoPrefab;
    public float gridOffset = 1;
    public Vector2 size;
    public Vector3 startPosition;
    public List<Cell> cells;
    public Dictionary<Vector2, Cell> activeCells = new Dictionary<Vector2, Cell>();
    public List<Cell> cellsAffected = new List<Cell>();
    public Weights weights;
    public GameObject borderPrefab;
    void Start()
    {
        // InitializeWaveFunction();
        StartCoroutine(CollapseOverTime());
    }
    private void LoadData()
    {
        //load dictionary here
    }
    public void InitializeWaveFunction()
    {
        ClearAll();
        for (int x = 0, y = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.y; z++)
            {
                Vector3 pos = new Vector3(x* gridOffset + startPosition.x, 0, z * gridOffset + startPosition.z);

                if(this.gameObject.transform.childCount>y)//kinda breaks
                {
                    GameObject block = this.transform.GetChild(y).gameObject;
                    block.SetActive(true);
                    block.transform.position = pos;        
                }
                else
                {
                    #if UNITY_EDITOR
                        GameObject block = (GameObject)PrefabUtility.InstantiatePrefab(allProtoPrefab as GameObject);
                        PrefabUtility.UnpackPrefabInstance(block, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                        block.transform.SetParent(this.transform);
                        block.transform.position = pos;
                    #endif
                }
                Cell cell = this.transform.GetChild(y).gameObject.GetComponent<Cell>();
                cell.coords = new Vector2(x,z);
                cells.Add(cell);
                activeCells.Add(cell.coords, cell);
                y++;
            }
        }
        foreach(Cell c in cells)
            FindNeighbours(c);

        foreach(Cell c in cells)
            c.GenerateWeight(weights);

        StartCollapse();

        CreateBorder();
        RandomizeBuildings();
    }
    private void CreateBorder()
    {
        //create a GameObject to contain the border
        //to keep things neat in the Hierarchy tab if you decide
        //to start generating huge landscapes later on
         
        //create border blocks along x axes
        for ( int x = 0; x < size.x; x++ )
        {
            DoInstantiate(borderPrefab, new Vector3(x* gridOffset + startPosition.x,0,-1* gridOffset + startPosition.z), Quaternion.identity,this.transform);
            DoInstantiate(borderPrefab, new Vector3(x* gridOffset + startPosition.x,0,size.y* gridOffset + startPosition.z), Quaternion.identity,this.transform);
        }
        
        //create border blocks along z axes
        for ( int z = 0; z < size.y; z++ )
        {
            DoInstantiate(borderPrefab, new Vector3(-1* gridOffset,0,z* gridOffset + startPosition.z), Quaternion.identity,this.transform);
            DoInstantiate(borderPrefab, new Vector3(size.x* gridOffset + startPosition.x,0,z* gridOffset + startPosition.z), Quaternion.identity,this.transform);
        }
    }
    [ContextMenu("Randomize Buildings")]
    public void RandomizeBuildings()
    {
        BuildingRandomizer[] buildings = GetComponentsInChildren<BuildingRandomizer>();
        foreach(BuildingRandomizer b in buildings)
            b.RandomizeBuilding();
    }
    private void DoInstantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent) {
         Transform temp = ((GameObject)Instantiate(prefab,position,rotation)).transform;
         temp.parent = parent;
     }
    private void FindNeighbours(Cell c)
    {
        c.posZneighbour = GetCell(c.coords.x,c.coords.y+1);
        c.negZneighbour = GetCell(c.coords.x,c.coords.y-1);
        c.posXneighbour = GetCell(c.coords.x+1,c.coords.y);
        c.negXneighbour = GetCell(c.coords.x-1,c.coords.y);
    }
    private Cell GetCell(float x, float z)
    {
        Cell cell = null;
        if(activeCells.TryGetValue(new Vector2(x,z), out cell))
            return cell;
        else
            return null;
    }
    int collapsed;
    public void StartCollapse()
    {
        collapsed=0;
        while(!isCollapsed())
            Iterate();
    }
    public IEnumerator CollapseOverTime()
    {
        while(!isCollapsed())
        {
            Iterate();
            yield return new WaitForSeconds(0.5f);
        }
    }
    private bool isCollapsed()
    {
        //check if any cells contain more than one entry
        foreach(Cell c in cells)
            if(c.possiblePrototypes.Count>1)
                return false;

        return true;
    }
    private void Iterate()
    {
        Cell cell = GetCellWithLowestEntropy();
        CollapseAt(cell);
        Propagate(cell);
    }
    private Cell GetCellWithLowestEntropy()
    {
        //add randomization in the case of a tie
        List<Cell> cellWithLowestEntropy = new List<Cell>();
        int x = 100000;

        foreach(Cell c in cells)
        {
            if(!c.isCollapsed)
            {
                if(c.possiblePrototypes.Count==x)
                {
                    cellWithLowestEntropy.Add(c);
                }
                else if(c.possiblePrototypes.Count<x)
                {
                    cellWithLowestEntropy.Clear();
                    cellWithLowestEntropy.Add(c);
                    x = c.possiblePrototypes.Count;
                }
            }
        }
        return cellWithLowestEntropy[Random.Range(0, cellWithLowestEntropy.Count)];
    }
    private void CollapseAt(Cell cell)
    {
        int selectedPrototype = SelectPrototype(cell.prototypeWeights);
        Prototype finalPrototype = cell.possiblePrototypes[selectedPrototype];
        finalPrototype.prefab = cell.possiblePrototypes[selectedPrototype].prefab;
        cell.possiblePrototypes.Clear();
        cell.possiblePrototypes.Add(finalPrototype);
        GameObject finalPrefab = Instantiate(finalPrototype.prefab, cell.transform, true);
        finalPrefab.transform.Rotate(new Vector3(0f, finalPrototype.meshRotation*90, 0f), Space.Self);
        finalPrefab.transform.localPosition = Vector3.zero;
        cell.name = cell.coords.ToString()+"_"+ collapsed.ToString();
        collapsed++;
        cell.isCollapsed = true;
    }
    private int SelectPrototype(List<int> prototypeWeights)
    {
        //multiply each possible prototype by their weight 
        //which is the average of the weights of their attributes
        //then add all those together
        //pick a random value 0-allAtributes weight
        //if less than the random number, return it, else keep going
        int total = 0;
        foreach(int weight in prototypeWeights)
            total+=weight;

        total = Random.Range(0, total);

        foreach(int weight in prototypeWeights)
        {
            for (int i = 0; i < prototypeWeights.Count; i++)
            {
                if(total<=prototypeWeights[i])
                {
                    return i;
                }
                else
                    total-=weight;
            }
        }
        return 0;
    }
    private void Propagate(Cell cell)
    {
        cellsAffected.Add(cell);
        int y = 0;
        while(cellsAffected.Count > 0)
        {
            Cell currentCell = cellsAffected[0];
            cellsAffected.Remove(currentCell);

            //get neighbor to the right
            Cell otherCell = currentCell.posXneighbour;
            if(otherCell!=null)
            {
                //Get sockets that we have available on our Right
                List<WFC_Socket> possibleConnections = GetPossibleSocketsPosX(currentCell.possiblePrototypes);

                bool constrained = false;
                for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
                {
                    //if the list of sockets that we have on the right does not contain the connector on the other cell to the left...
                    if(!possibleConnections.Contains(otherCell.possiblePrototypes[i].negX))
                    {
                        //then that is not a valid possibility and must be removed
                        otherCell.possiblePrototypes.RemoveAt(i);
                        otherCell.prototypeWeights.RemoveAt(i);
                        i-=1;
                        constrained = true;
                    }
                }

                if(constrained)
                    cellsAffected.Add(otherCell);
            }

            otherCell = currentCell.posZneighbour;
            if(otherCell!=null)
            {
                List<WFC_Socket> possibleConnections = GetPossibleSocketsPosZ(currentCell.possiblePrototypes);
                bool hasBeenConstrained = false;
        
                //check all neighbours
                for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
                {
                    if(!possibleConnections.Contains(otherCell.possiblePrototypes[i].negZ))
                    {
                        otherCell.possiblePrototypes.RemoveAt(i);
                        otherCell.prototypeWeights.RemoveAt(i);
                        i-=1;
                        hasBeenConstrained = true;
                    }
                }
                if(hasBeenConstrained)
                    cellsAffected.Add(otherCell);
            }
            otherCell = currentCell.negXneighbour;
            if(otherCell!=null)
            {
                List<WFC_Socket> possibleConnections = GetPossibleSocketsNegX(currentCell.possiblePrototypes);
                bool hasBeenConstrained = false;
                for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
                {
                    if(!possibleConnections.Contains(otherCell.possiblePrototypes[i].posX))
                    {
                        otherCell.possiblePrototypes.RemoveAt(i);
                        otherCell.prototypeWeights.RemoveAt(i);
                        i-=1;
                        hasBeenConstrained = true;
                    }
                }
                if(hasBeenConstrained)
                    cellsAffected.Add(otherCell);
            }
            otherCell = currentCell.negZneighbour;
            if(otherCell!=null)
            {
                List<WFC_Socket> possibleConnections = GetPossibleSocketsNegZ(currentCell.possiblePrototypes);
                bool hasBeenConstrained = false;
                for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
                {
                    if(!possibleConnections.Contains(otherCell.possiblePrototypes[i].posZ))
                    {
                        otherCell.possiblePrototypes.RemoveAt(i);
                        otherCell.prototypeWeights.RemoveAt(i);
                        i-=1;
                        hasBeenConstrained = true;
                    }
                }
                if(hasBeenConstrained)
                    cellsAffected.Add(otherCell);
            }


            // Debug.Log($"There are currently {cellsAffected.Count} cellsAffected");

            // if(otherCell!=null)
            // {
            //     int posible = otherCell.possiblePrototypes.Count;
            //     //other cell coords
            //     //other cell posible proto

            //     //possible neighbors of current cell to the other cell's direction

            //     //get all valid neighbors of current cell
            //     // Compare

            //     //if prototpe is not valid with current cell, remove
            //         //add to cells Affected
            //     Constrain(otherCell, currentCell);
            //     // if(Constrain(otherCell, currentCell))
            //     //     cellsAffected.Add(otherCell);
            // }
            // otherCell = cell.negXneighbour;
            // if(otherCell!=null)
            // {
            //     int posible = otherCell.possiblePrototypes.Count;
            //     if(Constrain(otherCell, currentCell))
            //         Debug.Log(otherCell.possiblePrototypes.Count);
            // }
            // otherCell = cell.posZneighbour;
            // if(otherCell!=null)
            // {
            //     int posible = otherCell.possiblePrototypes.Count;
            //     if(Constrain(otherCell, currentCell))
            //         Debug.Log(otherCell.possiblePrototypes.Count);
            // }
            // otherCell = cell.negZneighbour;
            // if(otherCell!=null)
            // {
            //     int posible = otherCell.possiblePrototypes.Count;
            //     if(Constrain(otherCell, currentCell))
            //         Debug.Log(otherCell.possiblePrototypes.Count);
            // }
            y++;
        }
    }
    private List<WFC_Socket> GetPossibleSocketsNegX(List<Prototype> prototypesAvailable)
    {
        List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
        foreach (Prototype proto in prototypesAvailable)
        {
            // if(!socketsAccepted.Contains(proto.posX))
            //     socketsAccepted.Add(proto.posX);
            if(!socketsAccepted.Contains(proto.negX))
                socketsAccepted.Add(proto.negX);
            // if(!socketsAccepted.Contains(proto.posZ))
            //     socketsAccepted.Add(proto.posZ);
            // if(!socketsAccepted.Contains(proto.negZ))
            //     socketsAccepted.Add(proto.negZ);
        }
        return socketsAccepted;
    }
    private List<WFC_Socket> GetPossibleSocketsNegZ(List<Prototype> prototypesAvailable)
    {
        List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
        foreach (Prototype proto in prototypesAvailable)
        {
            if(!socketsAccepted.Contains(proto.negZ))
                socketsAccepted.Add(proto.negZ);
        }
        return socketsAccepted;
    }
    private List<WFC_Socket> GetPossibleSocketsPosZ(List<Prototype> prototypesAvailable)
    {
        List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
        foreach (Prototype proto in prototypesAvailable)
        {
            if(!socketsAccepted.Contains(proto.posZ))
                socketsAccepted.Add(proto.posZ);
        }
        return socketsAccepted;
    }
    private List<WFC_Socket> GetPossibleSocketsPosX(List<Prototype> prototypesAvailable)
    {
        List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
        foreach (Prototype proto in prototypesAvailable)
        {
            if(!socketsAccepted.Contains(proto.posX))
            {
                // Debug.Log($"Adding {proto.posX}, to the list of accepted sockets for {proto.name}");
                socketsAccepted.Add(proto.posX);
            }
        }
        return socketsAccepted;
    }
    
    private bool Constrain(Cell otherCell, WFC_Socket socketItMustPairWith)
    {
        bool hasBeenConstrained = false;
        
        //check all neighbours
        for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
        {
            // if(otherCell.possiblePrototypes[i])
            // List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
            // socketsAccepted.AddRange(GetPossibleSockets(currentCell.possiblePrototypes));
            // Debug.Log($"Sockets accepted {socketsAccepted.Count}");
            // if(HasAConnector(currentCell.possiblePrototypes[0].negX, otherCell.possiblePrototypes[i].posX))
            // {
            //     otherCell.possiblePrototypes.RemoveAt(i);
            //     i-=1;
            //     hasBeenConstrained = true;
            // }
            // else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].posZ))
            // {
            //     otherCell.possiblePrototypes.RemoveAt(i);
            //     i-=1;
            //     hasBeenConstrained = true;
            // }
            // else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].negX))
            // {
            //     otherCell.possiblePrototypes.RemoveAt(i);
            //     i-=1;
            //     hasBeenConstrained = true;
            // }
            // else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].negZ))
            // {
            //     otherCell.possiblePrototypes.RemoveAt(i);
            //     i-=1;
            //     hasBeenConstrained = true;
            // }
        }
        return hasBeenConstrained;
    }
    // private bool Constrain(Cell otherCell, Cell currentCell)
    // {
    //     bool hasBeenConstrained = false;
        
    //     //check all neighbours
    //     for (int i = 0; i < otherCell.possiblePrototypes.Count; i++)
    //     {
    //         List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
    //         // socketsAccepted.AddRange(GetPossibleSockets(currentCell.possiblePrototypes));
    //         Debug.Log($"Sockets accepted {socketsAccepted.Count}");
    //         if(HasAConnector(currentCell.possiblePrototypes[0].negX, otherCell.possiblePrototypes[i].posX))
    //         {
    //             otherCell.possiblePrototypes.RemoveAt(i);
    //             i-=1;
    //             hasBeenConstrained = true;
    //         }
    //         else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].posZ))
    //         {
    //             otherCell.possiblePrototypes.RemoveAt(i);
    //             i-=1;
    //             hasBeenConstrained = true;
    //         }
    //         else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].negX))
    //         {
    //             otherCell.possiblePrototypes.RemoveAt(i);
    //             i-=1;
    //             hasBeenConstrained = true;
    //         }
    //         else if(HasAConnector(socketsAccepted, otherCell.possiblePrototypes[i].negZ))
    //         {
    //             otherCell.possiblePrototypes.RemoveAt(i);
    //             i-=1;
    //             hasBeenConstrained = true;
    //         }
    //     }
    //     return hasBeenConstrained;
    // }
    private bool HasAConnector(List<WFC_Socket> socketsAccepted, WFC_Socket thisSocket)
    {
        foreach (WFC_Socket s in socketsAccepted)
        {
            if(s == thisSocket)
                return true;
        }
        return false;
    }
    private List<WFC_Socket> GetPossibleSockets(List<Prototype> possibleNeighbors)
    {
        List<WFC_Socket> socketsAccepted = new List<WFC_Socket>();
        foreach (Prototype proto in possibleNeighbors)
        {
            if(!socketsAccepted.Contains(proto.posX))
                socketsAccepted.Add(proto.posX);
            if(!socketsAccepted.Contains(proto.negX))
                socketsAccepted.Add(proto.negX);
            if(!socketsAccepted.Contains(proto.posZ))
                socketsAccepted.Add(proto.posZ);
            if(!socketsAccepted.Contains(proto.negZ))
                socketsAccepted.Add(proto.negZ);
        }
        return socketsAccepted;
    }
    public void ClearAll()
    {
        cells.Clear();
        activeCells.Clear();
        for(int i = this.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(this.transform.GetChild(i).gameObject);
        }
    }
}
