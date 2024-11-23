using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.Helper;
using AEAssist.JobApi;
using ImGuiNET;

namespace AE_ACR.PLD.Triggers;

public class ITriggerCond_PLD背后人数 : ITriggerCond
{
    public int 人数 = 6;

    public bool Draw()
    {
        ImGui.Text(">=背后人数返回真");
        ImGui.DragInt("背后人数", ref 人数, 1f, 0, 7);
        return true;
    }


    public bool Handle(ITriggerCondParams condParams = null)
    {
        if (人数 >= GetPlayersBehind())
        {
            return true;
        }
        return false;
    }

    public int GetPlayersBehind()
    {
        float referenceRotation = Core.Me.Rotation;
        var myPosition = Core.Me.Position; // 自己的坐标
        float maxDistance = 8.0f; // 背后范围半径
        float backwardAngleThreshold = 145.0f * (MathF.PI / 180.0f); // 背后 ±145度转为弧度

        int playersBehindCount = 0; // 背后玩家计数

        foreach (var battleChara in PartyHelper.CastableAlliesWithin10)
        {

            //排除自己
            if (battleChara.GameObjectId == Core.Me.GameObjectId)
            {
                continue;
            }

            var battleCharaPosition = battleChara.Position;

            // 计算玩家与自身的方向向量
            Vector3 directionToPlayer = battleCharaPosition - myPosition;

            // 距离过滤
            if (directionToPlayer.Length() > maxDistance)
            {
                continue;
            }

            // 计算玩家相对指定面向的角度
            float angleBetween = MathF.Atan2(directionToPlayer.Z, directionToPlayer.X) - referenceRotation;

            // 归一化角度到[-π, π]
            angleBetween = (angleBetween + MathF.PI) % (2 * MathF.PI) - MathF.PI;

            // 判断玩家是否在背后145度范围
            if (angleBetween > MathF.PI - backwardAngleThreshold || angleBetween < -MathF.PI + backwardAngleThreshold)
            {
                playersBehindCount++;
            }
        }

        return playersBehindCount;
    }

    public string DisplayName { get; } = "骑士调停充能";
    public string Remark { get; set; }
}