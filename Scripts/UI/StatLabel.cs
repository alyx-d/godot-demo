using System.Globalization;
using demo.Scripts.Resources;
using Godot;

namespace demo.Scripts.UI;

public partial class StatLabel : Label
{
    
    [Export] private StatResource _statResource;
    public override void _Ready()
    {
        _statResource.OnUpdate += HandleUpdate;
        Text = _statResource.StatValue.ToString(CultureInfo.CurrentCulture);
    }

    private void HandleUpdate()
    {
        Text = _statResource.StatValue.ToString(CultureInfo.CurrentCulture);
    }
}