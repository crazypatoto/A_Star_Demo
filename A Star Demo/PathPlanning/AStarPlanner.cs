using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Priority_Queue;
using A_Star_Demo.Maps;

namespace A_Star_Demo.PathPlanning
{
    public class AStarPlanner
    {
        public class AStarNode : IEquatable<AStarNode>
        {
            public MapNode RefererMapNode { get; }
            public AStarNode ParentNode { get; set; }
            public float F { get { return this.G + this.H; } }
            public float G { get; set; }
            public float H { get; set; }

            public AStarNode(MapNode refererNode)
            {
                this.RefererMapNode = refererNode;
                this.ParentNode = null;
                this.G = float.PositiveInfinity;
            }

            public bool Equals(AStarNode other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(this.RefererMapNode.Name, other.RefererMapNode.Name);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((AStarNode)obj);
            }

            public static bool operator ==(AStarNode left, AStarNode right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(AStarNode left, AStarNode right)
            {
                return !Equals(left, right);
            }

            public override int GetHashCode()
            {
                return (this.RefererMapNode.Name != null ? this.RefererMapNode.Name.GetHashCode() : 0);
            }
        }
        public enum HeuristicFormulas
        {
            Manhattan = 0,
            Euclidean,
        }

        public HeuristicFormulas HeuristicFormula { get; private set; }
        public float TurningPenalty { get; set; } = 3;
        private Map _refererMap;
        private VCSServer _server;
        private AStarNode[,] _allAStarNodes;
        private SimplePriorityQueue<AStarNode> _openList;
        private List<AStarNode> _closedList;

        public AStarPlanner(VCSServer server)
        {
            _refererMap = server.CurrentMap;
            _server = server;
        }

        private void InitializeAllAStarNodes()
        {
            _allAStarNodes = new AStarNode[_refererMap.Height, _refererMap.Width];
            for (int y = 0; y < _refererMap.Height; y++)
            {
                for (int x = 0; x < _refererMap.Width; x++)
                {
                    _allAStarNodes[y, x] = new AStarNode(_refererMap.AllNodes[y, x]);
                }
            }
        }

        public List<MapNode> FindPath(MapNode startMapNode, MapNode goalMapNode, bool carryingRack = false, int constraintLayerIndex = 0, HeuristicFormulas heuristicFormula = HeuristicFormulas.Manhattan)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            InitializeAllAStarNodes();
            List<MapNode> path = null;
            _openList = new SimplePriorityQueue<AStarNode>();                                   // Initialize open list
            _closedList = new List<AStarNode>();                                                // Initialize empty closed list
            var startNode = _allAStarNodes[startMapNode.Location.Y, startMapNode.Location.X];
            startNode.H = this.HeuristicFunction(startMapNode, goalMapNode);                    // Initialize start node h = h(startNode)
            startNode.G = 0;                                                                    // Initialize start node g = 0
            _openList.Enqueue(startNode, startNode.F);                                          // Add start node to open list

            AStarNode currentNode = null;
            while (_openList.Count > 0)                                                         // Scan until open list is empty
            {
                currentNode = _openList.Dequeue();                                              // Get node with lowest f in open                 
                //Debug.WriteLine($"CurrentNode = {currentNode.RefererMapNode.Name}, {{F,G,H}} = {{{currentNode.F},{currentNode.G},{currentNode.H}}}");
                if (currentNode.RefererMapNode == goalMapNode) break;                           // End if currentNode == goalNode (A* path found!)                                

                Parallel.ForEach(_refererMap.GetNeighborNodes(currentNode.RefererMapNode), neighborMapNode =>
                {
                    if (neighborMapNode == currentNode.ParentNode?.RefererMapNode) return;
                    if (currentNode.RefererMapNode.Location.X + currentNode.RefererMapNode.Location.Y < neighborMapNode.Location.X + neighborMapNode.Location.Y)
                    {
                        if ((_refererMap.GetEdgeByNodes(constraintLayerIndex, currentNode.RefererMapNode, neighborMapNode).PassingRestriction & MapEdge.PassingRestrictions.NoLeaving) > 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if ((_refererMap.GetEdgeByNodes(constraintLayerIndex, currentNode.RefererMapNode, neighborMapNode).PassingRestriction & MapEdge.PassingRestrictions.NoEntering) > 0)
                        {
                            return;
                        }
                    }
                    if ((_server.OccupancyGrid[neighborMapNode.Location.Y, neighborMapNode.Location.X] & 0x01) > 0)
                    {
                        if (neighborMapNode != goalMapNode) return;
                    }
                    if (carryingRack && (_server.OccupancyGrid[neighborMapNode.Location.Y, neighborMapNode.Location.X] & 0x02) > 0)
                    {
                        if (neighborMapNode != goalMapNode) return;
                    }
                    if (currentNode.RefererMapNode == startMapNode)
                    {
                        if (_server.AGVNodeQueue[startMapNode.Location.Y, startMapNode.Location.X].Count > 0)
                        {
                            if (_server.AGVNodeQueue[neighborMapNode.Location.Y, neighborMapNode.Location.X].Count > 0)
                            {
                                var neighborNodeAGV = _server.AGVNodeQueue[neighborMapNode.Location.Y, neighborMapNode.Location.X].Peek();
                                if (_server.AGVNodeQueue[startMapNode.Location.Y, startMapNode.Location.X].FirstOrDefault(agv => agv == neighborNodeAGV) != null)
                                    return;
                            }
                        }
                    }

                    var successorNode = _allAStarNodes[neighborMapNode.Location.Y, neighborMapNode.Location.X];
                    var successorCurrentCost = currentNode.G + 1;                       // Set successor current cost = g(currentNode) + w(currentNode, successorNode) 

                    /*-------------------------------------Calculate extra cost -------------------------------------*/
                    // Add extra cost if a trun is made
                    if (currentNode.ParentNode != null && TurningPenalty > 0)
                    {
                        if (currentNode.ParentNode.RefererMapNode.Location.X == currentNode.RefererMapNode.Location.X)
                        {
                            if (successorNode.RefererMapNode.Location.X != currentNode.RefererMapNode.Location.X)
                            {
                                successorCurrentCost += TurningPenalty;
                            }
                        }
                        else
                        {
                            if (successorNode.RefererMapNode.Location.Y != currentNode.RefererMapNode.Location.Y)
                            {
                                successorCurrentCost += TurningPenalty;
                            }
                        }
                    }

                    // Add extra cost if successorNode has neighbors that type isn't None.
                    //foreach (var node in _refererMap.GetNeighborNodes(successorNode.RefererMapNode))
                    //{
                    //    if (node.Type != MapNode.Types.None) successorCurrentCost += 0.25f;
                    //}

                    /*--------------------------------------------- End ---------------------------------------------*/


                    if (_openList.Contains(successorNode))                              // If successorNode is already in open list
                    {
                        if (successorCurrentCost < successorNode.G)                     // If successor current cost is lower than successorNode.G
                        {
                            successorNode.G = successorCurrentCost;                     // Update successorNode.G with new lower cost
                            successorNode.ParentNode = currentNode;                     // Set successorNode's parent node to currentNode
                            _openList.UpdatePriority(successorNode, successorNode.F);   // Update successorNode's priority with newly calculated f
                        }
                    }
                    else if (_closedList.Contains(successorNode))                       // If successorNode is already in closed list
                    {
                        if (successorCurrentCost < successorNode.G)                     // If successor current cost is lower than successorNode.G
                        {
                            successorNode.G = successorCurrentCost;                     // Update successorNode.G with new lower cost                 
                            successorNode.ParentNode = currentNode;                     // Set successorNode's parent node to currentNode
                            _closedList.Remove(successorNode);                          // Remove successor from closed list
                            _openList.Enqueue(successorNode, successorNode.F);          // Put successor node in open list with newly calculated f
                        }
                    }
                    else                                                                // Neighter successorNode is in open list nor closed list
                    {
                        successorNode.H = HeuristicFunction(successorNode.RefererMapNode, goalMapNode);     // Calculate successorNode.H = h(successorNode)
                        successorNode.G = successorCurrentCost;                                             // Set successorNode.G = current successor cost                        
                        successorNode.ParentNode = currentNode;                                             // Set successorNode's parent node to currentNode                         
                        _openList.Enqueue(successorNode, successorNode.F);                                  // Put successor node in open list with newly calculated f
                    }
                    //Debug.WriteLine($"\tSuccessorNode = {successorNode.RefererMapNode.Name}, {{F,G,H}} = {{{successorNode.F},{successorNode.G},{successorNode.H}}}");
                });
                //Debug.WriteLine($"Dequeue {currentNode.RefererMapNode.Name}");
                _closedList.Add(currentNode);                    // Add currentNode to closed list
            }
            if (currentNode.RefererMapNode != goalMapNode)      // All node are closed but goal node is not found (No available path found)
            {
                Debug.WriteLine($"Path Not Found! Took {stopwatch.ElapsedMilliseconds}ms");
                return path;
            }

            int count = 0;
            path = new List<MapNode>();
            while (currentNode != null)                          // Travel back to generate path
            {
                count++;
                path.Insert(0, currentNode.RefererMapNode);
                //Debug.WriteLine($"Path Node =  {currentNode.RefererMapNode.Name}");
                currentNode = currentNode.ParentNode;
            }
            Debug.WriteLine($"Path Found! Took {stopwatch.ElapsedMilliseconds}ms");
            return path;
        }

        private float HeuristicFunction(MapNode currentNode, MapNode goalNode)
        {
            float esitmatedCost;
            switch (this.HeuristicFormula)
            {
                case HeuristicFormulas.Manhattan:
                    esitmatedCost = Math.Abs(goalNode.Location.X - currentNode.Location.X) + Math.Abs(goalNode.Location.Y - currentNode.Location.Y);
                    break;
                case HeuristicFormulas.Euclidean:
                    esitmatedCost = (float)Math.Sqrt(Math.Pow(goalNode.Location.X - currentNode.Location.X, 2) + Math.Pow(goalNode.Location.Y - currentNode.Location.Y, 2));
                    break;
                default:
                    throw new Exception("Unknow Heuristic Formula");
            }
            return esitmatedCost;
        }


    }
}
