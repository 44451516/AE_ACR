using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.AILoop;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace AE_ACR_DRK.HotKey;

public class HotkeyResolver_献奉Pm2 : IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(DRKBaseSlotResolvers.献奉, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        UIHelp.DrawSpellNormal(DRKBaseSlotResolvers.献奉.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (DRKBaseSlotResolvers.献奉.IsUnlockWithCDCheck())
        {
            return 0;
        }

        return -1;
    }

    public void Run()
    {

        if (DRKBaseSlotResolvers.献奉.IsUnlockWithCDCheck())
        {
            var slot = new Slot();
            Spell spell = new Spell(DRKBaseSlotResolvers.献奉.GetSpell().Id, SpellTargetType.Pm2);
            slot.Add(spell);
            slot.Run(AI.Instance.BattleData, false);
        }

    }
}