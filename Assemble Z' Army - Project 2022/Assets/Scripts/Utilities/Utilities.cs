using System.Collections.Generic;
using UnityEngine;

namespace Utilities{
    public static class Utils
    {
        public static Vector3 GetMouseIconPos()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos.y += 0.5f;
            mousePos.x += 0.5f;
            mousePos.z = 0;

            return mousePos;
        }

        public static Vector3 GetMouseWorldPosition(){
            Vector3 v = Input.mousePosition;
            v.z = 1;
            return Camera.main.ScreenToWorldPoint(v);
        }

        // Allowing to change one axis for relevent vector
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


        // Get positions around the point given.
        public static List<Vector3> GetCircleForamtionPosList(Vector3 startPos, float[] ringDistanceArr, int[] ringPosCountArr)
        {
            List<Vector3> posList = new List<Vector3>();
            posList.Add(startPos);
            for (int i = 0; i < ringPosCountArr.Length; i++)
            {
                posList.AddRange(GetPosListAround(startPos, ringDistanceArr[i], ringPosCountArr[i]));
            }
            return posList;
        }
 
        private static List<Vector3> GetPosListAround(Vector3 startPostion, float distance, int posCount)
        {
            List<Vector3> posList = new List<Vector3>();
            for (int i = 0; i < posCount; i++)
            {
                float angle = i * (360f / posCount);
                Vector3 direction = ApplyRotationToVec(new Vector3(1, 0), angle);
                Vector3 position = startPostion + direction * distance;
                posList.Add(position);
            }
            return posList;
        }

        private static Vector3 ApplyRotationToVec(Vector3 vec, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vec;
        }
    }
}
