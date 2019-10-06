using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD45.Physic
{
    public class AvoidWater : MonoBehaviour
    {
        public float checkDistance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dir">1 for right, -1 for left</param>
        /// <returns></returns>
        public bool IsWaterNear(int dir)
        {
            Vector2 checkPosition = new Vector2(transform.position.x,transform.position.y) + Gravity.GetRightAt(transform.position) * dir * checkDistance;

            Vector2 checkRayDir = -Gravity.GetUpAt(checkPosition);

            RaycastHit2D hit = Physics2D.Raycast(checkPosition, checkRayDir);

            return hit.collider != null && hit.collider.tag == "Water";
        }
    }
}
