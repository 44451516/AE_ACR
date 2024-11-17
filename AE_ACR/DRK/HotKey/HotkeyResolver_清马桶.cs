using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace AE_ACR_DRK.HotKey;

public class HotkeyResolver_清马桶 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(DRKBaseSlotResolvers.蔑视厌恶Disesteem, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(DRKBaseSlotResolvers.蔑视厌恶Disesteem.GetSpell(), size, isActive);
    }

    public int Check()
    {
        return 0;
    }

    public void Run()
    {
        AI.Instance.BattleData.HighPrioritySlots_OffGCD.Clear();
        AI.Instance.BattleData.HighPrioritySlots_GCD.Clear();
    }
}