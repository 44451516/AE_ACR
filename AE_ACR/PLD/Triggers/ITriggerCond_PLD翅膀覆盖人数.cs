using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Helper;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD翅膀覆盖人数 : ITriggerCond
{
    public int 人数 = 4;

    public bool Draw()
    {
        ImGui.Text(">=PLD翅膀可以覆盖人数返回真");
        ImGui.Text("[写的有问题不一定准]");
        ImGui.Text("当前位置和面向释放大翅膀能覆盖的人数");
        ImGui.DragInt("PLD翅膀可以覆盖人数", ref 人数, 1f, 0, 7);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        if (人数 >= GetPlayersInFanShape())
        {
            return true;
        }
        return false;
    }


    public string DisplayName { get; } = "骑士翅膀覆盖人数";
    public string Remark { get; set; }

    


    public static List<string> 测试列表 = new List<string>();

    public static int GetPlayersInFanShape()
    {
        测试列表.Clear();
        Vector3 selfPosition = Core.Me.Position;
        float selfRotation = Core.Me.Rotation;
        const float radius = 8f; // 扇形半径
        const float sectorAngle = MathF.PI * 120f / 180f; // 扇形总角度（弧度）
        const float halfSectorAngle = sectorAngle / 2;
        int count = 0;
        
        foreach (var player in PartyHelper.CastableAlliesWithin10)
        {
            //去掉自己
            if (player.GameObjectId == Core.Me.GameObjectId)
            {
                continue;
            }

            var selfPosition2D = selfPosition.ToVector2();
            var playerPosition2D = player.Position.ToVector2();

            // 计算玩家相对自己的向量
            var toPlayer = playerPosition2D - selfPosition2D;

            // 检查玩家是否在扇形半径范围内
            if (toPlayer.Length() > radius)
                continue;

            // 计算玩家与自身方向的夹角
            var angleToPlayer = MathF.Atan2(toPlayer.Y, toPlayer.X); // 玩家相对自身位置的角度
            var relativeAngle = NormalizeAngle(angleToPlayer - selfRotation); // 调整到自身面向方向的相对角度

            // 判断玩家是否在扇形范围内
            if (MathF.Abs(relativeAngle) <= halfSectorAngle)
            {
                测试列表.Add(player.Name.TextValue);
                count++;
            }
        }
        return count;
    }

    private static float NormalizeAngle(float angle)
    {
        // 将角度归一化到-π到π范围内
        while (angle > MathF.PI)
        {
            angle -= 2 * MathF.PI;
        }
        while (angle < -MathF.PI)
        {
            angle += 2 * MathF.PI;
        }
        return angle;
    }
    private static float 新面向(float angle)
    {
        if (angle > 0)
        {
            return angle - MathF.PI;
        }

        if (angle < 0)
        {
            return MathF.PI + angle;
        }
        return angle;
    }

}