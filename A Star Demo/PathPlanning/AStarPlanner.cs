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
        public enum HeuristicFormulas
        {
            Manhattan = 0,
        }
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
        private Map _refererMap;
        private AStarNode[,] _allAStarNodes;
        private SimplePriorityQueue<AStarNode> _openList;
        private List<AStarNode> _closedList;
        public HeuristicFormulas HeuristicFormula { get; private set; }


        public AStarPlanner(Map map)
        {
            _refererMap = map;
            _allAStarNodes = new AStarNode[map.Height, map.Width];
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    _allAStarNodes[y, x] = new AStarNode(map.AllNodes[y, x]);
                }
            }
        }

        public int FindPath(MapNode startMapNode, MapNode goalMapNode, int constraintLayerIndex = 0, HeuristicFormulas heuristicFormula = HeuristicFormulas.Manhattan)
        {
            _openList = new SimplePriorityQueue<AStarNode>();                   // Initialize open list
            _closedList = new List<AStarNode>();                                                                    // Initialize empty closed list
            var newNode = new AStarNode(startMapNode);
            newNode.H = this.HeuristicFunction(startMapNode, goalMapNode);                              // Initialize start node with f(startNode) = h(startNode)
            newNode.G = 0;
            _openList.Enqueue(newNode, newNode.F);                                                                  // Add start node to open list

            AStarNode currentNode = null;
            while (_openList.Count > 0)
            {
                currentNode = _openList.Dequeue();                                                      // Get node with lowest f in open list                
                if (currentNode.RefererMapNode == goalMapNode) break;                                          // End if node == goal node (A* path found!)                
                
                foreach (var neighborMapNode in _refererMap.GetNeighborNodes(currentNode.RefererMapNode))   // Extract all successor nodes
                {
                    if (currentNode.RefererMapNode.Location.X + currentNode.RefererMapNode.Location.Y < neighborMapNode.Location.X + neighborMapNode.Location.Y)
                    {
                        if ((_refererMap.GetEdgeByNodes(constraintLayerIndex, currentNode.RefererMapNode, neighborMapNode).PassingRestriction & MapEdge.PassingRestrictions.NoLeaving) > 0)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if ((_refererMap.GetEdgeByNodes(constraintLayerIndex, currentNode.RefererMapNode, neighborMapNode).PassingRestriction & MapEdge.PassingRestrictions.NoEntering) > 0)
                        {
                            continue;
                        }
                    }
                    var successorNode = _allAStarNodes[neighborMapNode.Location.Y, neighborMapNode.Location.X];
                    var successorCurrentCost = currentNode.G + 1;             // Set successor current cost = g(currentNode) + w(currentNode, successorNode)
                    if (_openList.Contains(successorNode))
                    {
                        if (successorCurrentCost < successorNode.G)
                        {
                            successorNode.G = successorCurrentCost;
                            successorNode.ParentNode = currentNode;
                            _openList.UpdatePriority(successorNode, successorNode.F);
                        }
                    }
                    else if (_closedList.Contains(successorNode))
                    {
                        if (successorCurrentCost < successorNode.G)
                        {
                            Debug.WriteLine($"Move to open list {successorCurrentCost.ToString()},{successorNode.G.ToString()}");
                            successorNode.G = successorCurrentCost;
                            successorNode.ParentNode = currentNode;
                            _closedList.Remove(successorNode);
                            _openList.Enqueue(successorNode, successorNode.F);
                        }
                    }
                    else
                    {
                        successorNode.H = HeuristicFunction(successorNode.RefererMapNode, goalMapNode);
                        successorNode.G = successorCurrentCost;
                        successorNode.ParentNode = currentNode;
                        _openList.Enqueue(successorNode, successorNode.F);
                    }
                    _closedList.Add(currentNode);
                }
                Debug.WriteLine($"{_openList.Count},{_closedList.Count}");                
            }
            if (currentNode.RefererMapNode != goalMapNode) return -1;

            int count = 0;
            while (currentNode.ParentNode != null)
            {
                count++;
                currentNode = currentNode.ParentNode;
            }
            return count;
        }

        private float HeuristicFunction(MapNode currentNode, MapNode goalNode)
        {
            float esitmatedCost;
            switch (this.HeuristicFormula)
            {
                case HeuristicFormulas.Manhattan:
                    esitmatedCost = Math.Abs(goalNode.Location.X - currentNode.Location.X) + Math.Abs(goalNode.Location.Y - currentNode.Location.Y);
                    break;
                default:
                    throw new Exception("Unknow Heuristic Formula");
            }
            return esitmatedCost;
        }


    }
}
