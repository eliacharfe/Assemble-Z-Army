using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities{
    public static class Utils
    {
        public static Vector3 GetMouseWorldPosition(){
            Vector3 v = Input.mousePosition;
            v.z = 1;
            return Camera.main.ScreenToWorldPoint(v);
        }

        public static Vector3 ChangeXAxis(Vector3 pos, float x)
        {
            return new Vector3(x, pos.y, pos.z);
        }

        public static Vector3 ChangeYAxis(Vector3 pos, float y)
        {
            return new Vector3(pos.x, y, pos.z);
        }

        public static Vector3 ChangeZAxis(Vector3 pos, float z)
        {
            return new Vector3(pos.x, pos.y, z);
        }
    }
}
