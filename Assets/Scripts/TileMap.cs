using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * parts of this project was created thanks to the help of this tutorial 
 * https://www.youtube.com/watch?v=QhaKb5N3Hj8
 */

public class TileMap : MonoBehaviour
{
    private UnitController control;
    public GameObject selected;
    Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
    public GameObject[,] grid;
    public TileInfo[] tileType;
    public Unit[] units;
    List<Node> allowed = new List<Node>();
    int count = 0;

    int[,] tiles;
    Node[,] graph;

    public int gridSize;

    public class Node
    {
        public List<Node> neighbours;
        public int xCoord;
        public int yCoord;

        public bool passible = true;
        public bool player = false;

        public Node()
        {
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node _n)
        {
            return Vector2.Distance
                (new Vector2(xCoord, yCoord),
                new Vector2(_n.xCoord, _n.yCoord)
                );
        }
    }

    void Start()
    {
        control = UnitController.Instance;
        tiles = new int[gridSize, gridSize];
        grid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                tiles[x, y] = 0;
            }
        }

        generateGraph();

        generateMap();
    }

    void generateGraph()
    {
        graph = new Node[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                graph[x, y] = new Node();

                graph[x, y].xCoord = x;
                graph[x, y].yCoord = y;
            }
        }

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (x < gridSize - 1)
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < gridSize - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }

    void generateMap()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                TileInfo temp = tileType[tiles[x, y]];

                GameObject tile = (GameObject)Instantiate(temp.prefab, new Vector3(x, 0, y), Quaternion.identity);

                Clickable click = tile.GetComponent<Clickable>();

                click.xCoord = x;
                click.yCoord = y;
                click.map = this;

                grid[x, y] = tile;
            }
        }
    }

    public Vector3 tileCoordtoWorldCoord(int _x, int _y)
    {
        return new Vector3(_x, 0, _y);
    }

    public GameObject[] detectEnemy()
    {
        GameObject[] enemies = new GameObject[1];

        print("Detect");
        selected.GetComponent<Unit>().currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        prev.Clear();
        prev = new Dictionary<Node, Node>();

        List<Node> unvistied = new List<Node>();

        allowed.Clear();
        allowed = new List<Node>();

        Node source = graph[selected.GetComponent<Unit>().xCoord,
            selected.GetComponent<Unit>().yCoord];

        // Node target = graph[_x, _y];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
                if (grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemy == true)
                {
                    print("true");
                    v.passible = false;
                    grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemyObj.GetComponent<Enemy>().attackable = true;
                    enemies[0] = grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemyObj;
                }
            }
            unvistied.Add(v);
        }
        while (unvistied.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvistied)
            {
                bool enemy = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().enemy;
                bool friend = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().friend;

                if (u == null || dist[possibleU] < dist[u] && possibleU.passible == true)
                {
                    //print("pos");
                    u = possibleU;
                    //grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().changeColourActive();
                }
            }

            if (u == null)
            {
                break;
            }

            unvistied.Remove(u);

            foreach (Node v in u.neighbours)
            {
                if (u.passible == false)
                {
                    print("false");
                }
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v] && alt < 2 && v.passible == false)
                {
                    dist[v] = alt;
                    prev[v] = u;
                    grid[v.xCoord, v.yCoord].GetComponent<Clickable>().changeColourActive();
                    //grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemyObj.GetComponent<Enemy>().attackable = false;
                    allowed.Add(v);
                }
            }
        }
        return enemies;
    }

    public void generatePathTo(GameObject _current)
    {

        print("WHY HERE");

        selected = control._currentUnit.gameObject;
        selected.GetComponent<Unit>().currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        prev.Clear();
        prev = new Dictionary<Node, Node>();

        List<Node> unvistied = new List<Node>();
        allowed.Clear();
        allowed = new List<Node>();

        Node source = graph[selected.GetComponent<Unit>().xCoord, 
            selected.GetComponent<Unit>().yCoord];

       // Node target = graph[_x, _y];

        dist[source] = 0;
        prev[source] = null;

        foreach(Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
                if( grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemy == true ||
                    grid[v.xCoord, v.yCoord].GetComponent<Clickable>().friend == true  )
                {
                    v.passible = false;
                }
            }
            unvistied.Add(v);
        }

        while(unvistied.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvistied)
            {
                //bool enemy = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().enemy;
                //bool friend = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().friend;
               
                if (u == null || dist[possibleU] < dist[u] && possibleU.passible == true)
               {
                    u = possibleU;
                    // grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().changeColour();
                }
            }

            if(u == null)
            {
                break;
            }

            unvistied.Remove(u);

            foreach (Node v in u.neighbours)
            {
                if (u.passible == false)
                {
                    print("false");
                }
                float alt = dist[u] + u.DistanceTo(v);
                if(alt < dist[v] && alt < 4 && v.passible == true)
                {
                    dist[v] = alt;
                    prev[v] = u;
                    grid[v.xCoord, v.yCoord].GetComponent<Clickable>().changeColourActive();
                    allowed.Add(v);
                }
            }
        }
    }

    public void playerUnits()
    {
      
    }

    public Node enemyMovement(GameObject _current)
    {
        selected = _current;
        selected.GetComponent<Enemy>().currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        prev.Clear();
        prev = new Dictionary<Node, Node>();

        List<Node> unvistied = new List<Node>();
        allowed.Clear();
        allowed = new List<Node>();

        Node source = graph[selected.GetComponent<Enemy>().xCoord,
        selected.GetComponent<Enemy>().yCoord];

        // Node target = graph[_x, _y];
        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
                if (grid[v.xCoord, v.yCoord].GetComponent<Clickable>().enemy == true ||
                    grid[v.xCoord, v.yCoord].GetComponent<Clickable>().friend == true)
                {
                    v.passible = false;
                }
                if (grid[v.xCoord, v.yCoord].GetComponent<Clickable>().friend == true)
                {
                    v.player = true;
                }
            }
            unvistied.Add(v);
        }
        while (unvistied.Count > 0)
        {
          Node u = null;
        foreach (Node possibleU in unvistied)
            {
                //bool enemy = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().enemy;
                //bool friend = grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().friend;
                if (u == null || dist[possibleU] < dist[u] && possibleU.passible == true)
                {
                    u = possibleU;
                        // grid[possibleU.xCoord, possibleU.yCoord].GetComponent<Clickable>().changeColour();
                }
            }

            if (u.player == true)
            {
                break;
            }

            if (u == null)
            {
                break;
            }

                unvistied.Remove(u);

            foreach (Node v in u.neighbours)
            {
                if (u.passible == false)
                {
                    print("false");
                }
                float alt = dist[u] + u.DistanceTo(v);

                if (alt < dist[v] && v.passible == true && v.player == false)
                {
                    dist[v] = alt;
                    prev[v] = u;
                    //grid[v.xCoord, v.yCoord].GetComponent<Clickable>().changeColourActive();
                    allowed.Add(v);
                }
                else if(v.player == true)
                {
                    dist[v] = alt;
                    prev[v] = u;
                    return v;
                }
            }
        }
        return null;
    }

    public void enemyPathMove(Node _target)
    {
        Node target = _target;

        if (target == null)
        {
            print("null");
        }

        //if (prev[target] == null)
        //{
        //    //no route
        //    print("NOOOOOOOOOOOOOO");
        //    //selected.transform.position = new Vector3(selected.GetComponent<Unit>().startXCoord, 0, selected.GetComponent<Unit>().startYCoord);
        //    //selected.GetComponent<Unit>().xCoord = selected.GetComponent<Unit>().startXCoord;
        //    //selected.GetComponent<Unit>().yCoord = selected.GetComponent<Unit>().startYCoord;
        //    return;
        //}

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        selected.GetComponent<Enemy>().currentPath = currentPath;

        int currNode = 0;

        print(selected.GetComponent<Enemy>().currentPath.Count);

        while (currNode < selected.GetComponent<Enemy>().currentPath.Count -1)
        //foreach (Node v in selected.GetComponent<Enemy>().currentPath)
        {
            print(currNode + " = "+ "currNode");
            if (currNode >= 2)
                //(currNode + 1 >= selected.GetComponent<Enemy>().currentPath.Count - 1)
            {
                selected.transform.position = new Vector3(
                    selected.GetComponent<Enemy>().currentPath[currNode].xCoord,
                    0,
                    selected.GetComponent<Enemy>().currentPath[currNode].yCoord);
                break;

            }
            currNode++;
        }
        print("No End Point");
        //selected.transform.position = new Vector3(selected.GetComponent<Unit>().startXCoord, 0, selected.GetComponent<Unit>().startYCoord);
    }

    public void isItInPath(int _x, int _y)
    {
        print("map");

        Node target = graph[_x, _y];

        if (prev[target] == null)
        {
            //no route
            selected.transform.position = new Vector3(selected.GetComponent<Unit>().startXCoord, 0, selected.GetComponent<Unit>().startYCoord);
            selected.GetComponent<Unit>().xCoord = selected.GetComponent<Unit>().startXCoord;
            selected.GetComponent<Unit>().yCoord = selected.GetComponent<Unit>().startYCoord;
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        selected.GetComponent<Unit>().currentPath = currentPath;

        foreach (Node v in selected.GetComponent<Unit>().currentPath)
        {
            print("NODE");
            if (v.xCoord == _x && v.yCoord == _y)
            {
                print("ACCEPTABLE");
                selected.transform.position = new Vector3(v.xCoord, 0, v.yCoord);
                return;
            }
            else
            {
               //selected.transform.position = new Vector3(selected.GetComponent<Unit>().startXCoord, 0, selected.GetComponent<Unit>().startYCoord);
                print("UNACCEPTABLE");
            }
        }

        selected.transform.position = new Vector3(selected.GetComponent<Unit>().startXCoord, 0, selected.GetComponent<Unit>().startYCoord);
    }

    public void colourReturn()
    {
        foreach (Node v in graph)
        {
            //print("COLOUR");
            grid[v.xCoord, v.yCoord].GetComponent<Clickable>().changeColourDeactive();
            v.passible = true;
        }
    }
}