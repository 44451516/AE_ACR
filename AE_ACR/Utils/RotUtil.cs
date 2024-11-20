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
        
        public static void 骑士大翅膀FaceFarPoint()
        {
            var myPosition = Core.Me.Position;  // 自己的坐标
            float maxDistance = 8.0f;           // 背后范围半径
            float backwardAngleThreshold = MathF.PI / 2; // ±90度范围
            int maxPlayers = 0;
            float optimalRotation = Core.Me.Rotation; // 初始化为当前面向

            // 枚举所有队友的方向作为候选面向
            foreach (var battleChara in PartyHelper.CastableAlliesWithin10)
            {
                var battleCharaPosition = battleChara.Position;

                // 计算与每个玩家的方向向量和角度
                Vector3 directionToPlayer = battleCharaPosition - myPosition;
                float candidateRotation = MathF.Atan2(directionToPlayer.Z, directionToPlayer.X);

                // 统计该候选面向背后玩家数量
                int playersBehind = 0;
                foreach (var otherChara in PartyHelper.CastableAlliesWithin10)
                {
                    var otherCharaPosition = otherChara.Position;

                    // 计算与其他玩家的方向向量
                    Vector3 directionToOther = otherCharaPosition - myPosition;

                    // 计算距离过滤
                    if (directionToOther.Length() > maxDistance)
                    {
                        continue;
                    }

                    // 计算相对候选面向的角度
                    float angleBetween = MathF.Atan2(directionToOther.Z, directionToOther.X) - candidateRotation;

                    // 归一化角度到[-π, π]
                    angleBetween = (angleBetween + MathF.PI) % (2 * MathF.PI) - MathF.PI;

                    // 判断是否在背后范围
                    if (angleBetween > MathF.PI / 2 || angleBetween < -MathF.PI / 2)
                    {
                        playersBehind++;
                    }
                }

                // 更新最大值和最佳面向
                if (playersBehind > maxPlayers)
                {
                    maxPlayers = playersBehind;
                    optimalRotation = candidateRotation;
                }
            }
            
            // 设置自己的面向为最佳面向
            Core.Resolve<MemApiMove>().SetRot(optimalRotation);
            
        }

    }
}
