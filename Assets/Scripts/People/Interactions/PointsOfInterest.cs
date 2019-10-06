using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.People.Interations
{

    public static class PointsOfInterest
    {
        public static List<Transform> points = new List<Transform>();

        public static Vector2 ClosestPointOfInterest(Vector2 position, out bool anyPointFound)
        {
            anyPointFound = false;
            Vector2 closestPos = Cursor.Instance.CursorPosition;
            float minDist = Vector2.Distance(position, closestPos);
            foreach (Transform t in points)
            {
                float dist = Vector2.Distance(position, t.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestPos = t.position;
                }
            }

            if (minDist < 3)
                anyPointFound = true;
            return closestPos;
        }
    }
}