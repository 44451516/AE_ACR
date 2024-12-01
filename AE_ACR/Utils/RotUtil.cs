using System.Numerics;
using AEAssist;
using AEAssist.Helper;
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
            if (!(testPosition.X >= mapBottomLeft.X && testPosition.X <= mapTopRight.X && testPosition.Z <= mapBottomLeft.Z && testPosition.Z >= mapTopRight.Z))
                return;

            int xIndex = (int)((testPosition.X - mapBottomLeft.X) / squareWidth);
            int zIndex = (int)((mapBottomLeft.Z - testPosition.Z) / squareHeight);

            Vector3 squareBottomLeft = new Vector3(mapBottomLeft.X + xIndex * squareWidth, 0, mapBottomLeft.Z - zIndex * squareHeight);
            Vector3 squareTopRight = new Vector3(squareBottomLeft.X + squareWidth, 0, squareBottomLeft.Z - squareHeight);

            List<Vector3> possiblePoints = new List<Vector3>
            {
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

        public static void 骑士大翅膀FaceFarPoint()
        {
            const float radius = 8f; // 扇形半径
            const float sectorAngle = MathF.PI * 90f / 180f; // 扇形总角度（弧度）

            var selfPosition = Core.Me.Position.ToVector2();

            // 计算每个玩家相对于自身的位置角度
            var validPlayerAngles = new List<float>();
            foreach (var player in PartyHelper.CastableAlliesWithin10)
            {
                var toPlayer = player.Position.ToVector2() - selfPosition;

                // 跳过超出半径的玩家
                if (toPlayer.Length() > radius) continue;

                // 计算玩家相对自身的角度
                var angleToPlayer = MathF.Atan2(toPlayer.Y, toPlayer.X);
                validPlayerAngles.Add(angleToPlayer);
            }

            // 如果没有玩家在半径内，返回当前面向（默认 0 角度）
            if (validPlayerAngles.Count == 0)
                return;

            // 对角度进行排序并扩展成一个循环列表（加上360度范围的镜像）
            validPlayerAngles.Sort();
            validPlayerAngles.AddRange(validPlayerAngles.Select(angle => angle + 2 * MathF.PI));

            // 滑动窗口寻找包含最多玩家的扇形起始角度
            int maxPlayers = 0;
            float bestAngle = 0f;
            int start = 0;

            for (int end = 0; end < validPlayerAngles.Count; end++)
            {
                while (validPlayerAngles[end] - validPlayerAngles[start] > sectorAngle)
                {
                    start++;
                }

                int currentPlayers = end - start + 1;
                if (currentPlayers > maxPlayers)
                {
                    maxPlayers = currentPlayers;
                    bestAngle = validPlayerAngles[start] + sectorAngle / 2; // 扇形中心角度
                }
            }


            // 将最佳角度归一化到 -π 到 π 范围内
            var newRot = NormalizeAngle(bestAngle);
            // 设置自己的面向为最佳面向
            Core.Resolve<MemApiMove>().SetRot(newRot);
        }

        private static float NormalizeAngle(float angle)
        {
            // 将角度归一化到 -π 到 π 范围内
            while (angle > MathF.PI) angle -= 2 * MathF.PI;
            while (angle < -MathF.PI) angle += 2 * MathF.PI;
            return angle;
        }
    }
}