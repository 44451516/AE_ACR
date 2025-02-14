using System.Numerics;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Utility;
using ImGuiNET;

namespace AE_ACR.utils;

public static class UIHelp
{
    public static void Feedback(JobViewWindow obj)
    {
        ImGui.Text("请先提前组织好语言");
        ImGui.Text("请先提前组织好语言");
        ImGui.Text("请先提前组织好语言");
        if (ImGui.Button("反馈建议"))
        {
            Util.OpenLink("https://docs.qq.com/sheet/DT0NkbkNKd0NXTWRM");
        }
    }


    public static void MyAddQt(this JobViewWindow jobView, Dictionary<string, bool> MyQtDict, string name, bool qtValueDefault, string toolTip = "")
    {
        var qtArray = MyQtDict;

        if (qtArray.TryGetValue(name, out var qt))
        {
            jobView.AddQt(name, qt, toolTip);
        }
        else
        {
            jobView.AddQt(name, qtValueDefault, toolTip);
        }
    }


    public static void MyAddQt(this JobViewWindow jobView, Dictionary<string, bool> MyQtDict, string name, bool qtValueDefault, Action<bool> action, string toolTip = "")
    {
        var qtArray = MyQtDict;

        if (qtArray.TryGetValue(name, out var qt))
        {
            jobView.AddQt(name, qt, action);
        }
        else
        {
            jobView.AddQt(name, qtValueDefault, action);
        }
    }

    public static void DrawSpellNormal(Spell spell, Vector2 hotKeySize, bool isActive)
    {
        if (spell.IsAbility())
        {
            if (spell.Id.IsUnlockWithCDCheck())
            {
                if (isActive)
                {
                    //激活状态
                    ImGui.SetCursorPos(new Vector2(0, 0));
                    if (Core.Resolve<MemApiIcon>().TryGetTexture
                        (
                            @"Resources\Spells\Icon\activeaction.png",
                            out IDalamudTextureWrap? textureWrap_active
                        ))
                        ImGui.Image(textureWrap_active.ImGuiHandle, hotKeySize);
                }
                else
                {
                    //常规状态
                    ImGui.SetCursorPos(new Vector2(0, 0));
                    if (Core.Resolve<MemApiIcon>().TryGetTexture
                        (
                            @"Resources\Spells\Icon\iconframe.png",
                            out IDalamudTextureWrap? textureWrap_normal
                        ))
                        ImGui.Image(textureWrap_normal.ImGuiHandle, hotKeySize);
                }
            }
            else
            {
                //技能不可使用
                //变黑
                ImGui.SetCursorPos(new Vector2(0, 0));
                if (Core.Resolve<MemApiIcon>().TryGetTexture
                    (
                        @"Resources\Spells\Icon\icona_frame_disabled.png",
                        out IDalamudTextureWrap? textureWrap_black
                    ))
                    ImGui.Image(textureWrap_black.ImGuiHandle, hotKeySize);
            }
        }
        else
        {
            //技能可使用
            if (isActive)
            {
                //激活状态
                ImGui.SetCursorPos(new Vector2(0, 0));
                if (Core.Resolve<MemApiIcon>().TryGetTexture
                    (
                        @"Resources\Spells\Icon\activeaction.png",
                        out IDalamudTextureWrap? textureWrap_active
                    ))
                    ImGui.Image(textureWrap_active.ImGuiHandle, hotKeySize);
            }
            else
            {
                //常规状态
                ImGui.SetCursorPos(new Vector2(0, 0));
                if (Core.Resolve<MemApiIcon>().TryGetTexture
                    (
                        @"Resources\Spells\Icon\iconframe.png",
                        out IDalamudTextureWrap? textureWrap_normal
                    ))
                    ImGui.Image(textureWrap_normal.ImGuiHandle, hotKeySize);
            }
        }
        
        var cd = spell.Cooldown.TotalSeconds;
        //cd文字显示
        if (cd > 0 && (int)(cd * 1000) + 1 !=
            Core.Resolve<MemApiSpell>().GetGCDDuration() - Core.Resolve<MemApiSpell>().GetElapsedGCD())
        {
            //cd
            if (spell.Id != 0)
                cd = cd % ((int)spell.RecastTime.TotalSeconds / spell.MaxCharges);
            ImGui.SetCursorPos(new Vector2(4, hotKeySize.Y - 17));
            ImGui.Text($"{(int)cd + 1}");
        }

        //Charge
        if (spell.MaxCharges > 1)
        {
            var charge = (int)spell.Charges;
            var chargeSize = hotKeySize * 0.3f;
            ImGui.SetCursorPos(new Vector2(hotKeySize.X - chargeSize.X - 3, hotKeySize.Y - chargeSize.Y - 5));
            if (Core.Resolve<MemApiIcon>().TryGetTexture($"Resources\\Spells\\Icon\\Charge{charge}.png",
                    out IDalamudTextureWrap? textureWrap_normal))
                ImGui.Image(textureWrap_normal.ImGuiHandle, chargeSize);
        }
    }
}