using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Windows.System;

namespace java_menace.Movement
{
    class CollisionManager
    {
        private List<Collider> Colliders;
        private Dictionary<VirtualKey, VirtualKey> ReverseKey;

        public CollisionManager()
        {
            Colliders = new List<Collider>();
            ReverseKey = new Dictionary<VirtualKey, VirtualKey>()
            {
                { VirtualKey.W, VirtualKey.S},
                { VirtualKey.S, VirtualKey.W},
                { VirtualKey.A, VirtualKey.D},
                { VirtualKey.D, VirtualKey.A}
            };
        }

        public void AddCollider(Collider c)
        {
            Colliders.Add(c);
            c.CheckCollisionRequest += OnCheckCollisionRequest;
        }

        public void RemoveCollider(Collider c)
        {
            if (Colliders.Contains(c)){
                Colliders.Remove(c);
            }
        }

        public void OnCheckCollisionRequest(object sender, CollisionRequestArgs e)
        {
            var RequestorCollider = (Collider)sender;
            var WatchedColliders = Colliders.Where(x => Vector2.Distance(RequestorCollider.VerticesCoordinates["Center"],
                                                                        x.VerticesCoordinates["Center"]) <= 72 && x != RequestorCollider);
            bool IsColliding = false;
            var _WatchedVertices = WatchedVertices(RequestorCollider, e.Key);

            foreach (var Collider in WatchedColliders)
            {
                if (IsInside(_WatchedVertices[0], Collider) && IsInside(_WatchedVertices[1], Collider))
                {
                    IsColliding = true;
                    RequestorCollider.IsColliding[e.Key] = true;
                }
            }

            if (!IsColliding)
            {
                RequestorCollider.IsColliding[e.Key] = false;
            }
        }

        private Vector2[] WatchedVertices(Collider c, VirtualKey Key)
        {
            var Points = new Vector2[2];
            Vector2[] AdjustmentVector;

            if (Key == VirtualKey.W || Key == VirtualKey.S)
                AdjustmentVector = new Vector2[] { new Vector2(1, 0), new Vector2(-1, 0) };

            else
                AdjustmentVector = new Vector2[] { new Vector2(0, 1), new Vector2(0, -1) };

            switch (Key)
            {
                case VirtualKey.W:
                    Points[0] = (c.VerticesCoordinates["Top-Left"]);
                    Points[1] = (c.VerticesCoordinates["Top-Right"]);
                    break;

                case VirtualKey.S:
                    Points[0] = (c.VerticesCoordinates["Bottom-Left"]);
                    Points[1] = (c.VerticesCoordinates["Bottom-Right"]);
                    break;

                case VirtualKey.A:
                    Points[0] = c.VerticesCoordinates["Top-Left"];
                    Points[1] = c.VerticesCoordinates["Bottom-Left"];
                    break;

                case VirtualKey.D:
                    Points[0] = c.VerticesCoordinates["Top-Right"];
                    Points[1] = c.VerticesCoordinates["Bottom-Right"];
                    break;
            }

            Points[0] += AdjustmentVector[0];
            Points[1] += AdjustmentVector[1];

            return Points;
        }

        private bool IsInside(Vector2 Vertice, Collider c)
        {
            if (Vertice.X < c.VerticesCoordinates["Top-Right"].X && Vertice.X > c.VerticesCoordinates["Top-Left"].X)
            {
                if (Vertice.Y > c.VerticesCoordinates["Top-Right"].Y && Vertice.Y < c.VerticesCoordinates["Bottom-Right"].Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
