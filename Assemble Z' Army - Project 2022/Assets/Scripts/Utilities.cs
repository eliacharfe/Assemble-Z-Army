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
    }
}
