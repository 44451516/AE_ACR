using System.Data;
using System.Numerics;
using AE_ACR.PLD.Triggers;
using AEAssist;
using AEAssist.Avoid;
using AEAssist.Extension;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Game.ClientState.Objects.Types;

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
            float step = MathF.PI / 360;
            float temp = -555f;
            int tplayerCount = 0;

            var 新面向 = false;

            // 从 MathF.PI 到 -MathF.PI 遍历
            for (float angle = MathF.PI; angle >= -MathF.PI; angle -= step)
            {
                var playerCount = GetPlayersInFanShape(PartyHelper.CastableAlliesWithin10, angle);
                // var playerCount = ITriggerCond_PLD翅膀覆盖人数.GetPlayersInFanShape(ECHelperObjectsUtils.get8(), angle);
                if (playerCount > tplayerCount)
                {
                    tplayerCount = playerCount;
                    temp = angle;
                    新面向 = true;
                }
            }

            var newRot = temp;
            // 设置自己的面向为最佳面向
            if (新面向)
            {
                // LogHelper.Print($"新面向{temp} -{tplayerCount}");
                Core.Resolve<MemApiMove>().SetRot(newRot);
            }
        }

        public static int GetPlayersInFanShape(List<IBattleChara> list, float Rotation)
        {
            int count = 0;
            SectorShape sectorShape = new SectorShape()
            {
                Origin = Core.Me.Position, Angle = 90, Dir = -Rotation.GetDir(), Radius = 8f
            };

            foreach (var battleChara in list)
            {
                //去掉自己
                if (battleChara.GameObjectId == Core.Me.GameObjectId)
                {
                    continue;
                }

                var distance = Core.Me.Distance(battleChara);
                if (distance > 8)
                {
                    continue;
                }

                if (sectorShape.IsInShape(battleChara.Position))
                {
                    count++;
                }
            }

            return count;
        }
    }
}