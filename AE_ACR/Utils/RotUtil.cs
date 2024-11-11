using System.Numerics;
using AEAssist;
using AEAssist.MemoryApi;

namespace AE_ACR.Utils
{
    public class RotUtil
    {
        public static void M1S_FaceFarPointInSquare(Vector3 testPosition)
        {
            float minRequiredDistance = 10.5f;
            float squareWidth = 10;
            float squareHeight = 10;
            Vector3 mapBottomLeft = new Vector3(80, 0, 110);
            Vector3 mapTopRight = new Vector3(120, 0, 90);
            if (!(testPosition.X >= mapBottomLeft.X && testPosition.X <= mapTopRight.X &&
                  testPosition.Z <= mapBottomLeft.Z && testPosition.Z >= mapTopRight.Z))
                return;

            int xIndex = (int)((testPosition.X - mapBottomLeft.X) / squareWidth);
            int zIndex = (int)((mapBottomLeft.Z - testPosition.Z) / squareHeight);

            Vector3 squareBottomLeft = new Vector3(mapBottomLeft.X + xIndex * squareWidth, 0, mapBottomLeft.Z - zIndex * squareHeight);
            Vector3 squareTopRight = new Vector3(squareBottomLeft.X + squareWidth, 0, squareBottomLeft.Z - squareHeight);

            List<Vector3> possiblePoints = new List<Vector3>{
                squareBottomLeft,
                new Vector3(squareBottomLeft.X, 0, squareTopRight.Z),
                new Vector3(squareTopRight.X, 0, squareBottomLeft.Z),
                squareTopRight
            };

            Vector3 targetPoint = Vector3.Zero;

            foreach (var point in possiblePoints)
            {
                float distance = Vector3.Distance(testPosition, point);
                if (distance > minRequiredDistance)
                {
                    targetPoint = point;
                    break;
                }
            }

            if (targetPoint == Vector3.Zero)
            {
                //  LogHelper.Debug("请注意，当前位置没有符合条件的击退点。");
                return;
            }

            float deltaX = targetPoint.X - testPosition.X;
            float deltaZ = targetPoint.Z - testPosition.Z;

            float newRotation = (float)Math.Atan2(deltaX, deltaZ);

            Core.Resolve<MemApiMove>().SetRot(newRotation);
        }

    }
}
