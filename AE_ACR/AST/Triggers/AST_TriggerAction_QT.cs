#region

using AE_ACR.GLA;
using AEAssist.CombatRoutine.Trigger;
using AEAssist.GUI;
using ImGuiNET;

#endregion

namespace AE_ACR.AST.Triggers;

//这个类也可以完全复制 改一下上面的namespace和对QT的引用就行
internal class AST_TriggerAction_QT : ITriggerAction
{
    private readonly string[] _qtArray;

    // 辅助数据 因为是private 所以不存档
    private int _selectIndex;

    public string Key = "";
    public bool Value;

    public AST_TriggerAction_QT()
    {
        _qtArray = ASTRotationEntry.QT.GetQtArray();
    }

    public string DisplayName { get; } = "AST/QT";
    public string Remark { get; set; }

    public bool Draw()
    {
        _selectIndex = Array.IndexOf(_qtArray, Key);
        if (_selectIndex == -1) _selectIndex = 0;
        ImGuiHelper.LeftCombo("选择Key", ref _selectIndex, _qtArray);
        Key = _qtArray[_selectIndex];
        ImGui.SameLine();
        using (new GroupWrapper())
        {
            ImGui.Checkbox("", ref Value);
        }

        return true;
    }

    public bool Handle()
    {
        ASTRotationEntry.QT.SetQt(Key, Value);
        return true;
    }
}