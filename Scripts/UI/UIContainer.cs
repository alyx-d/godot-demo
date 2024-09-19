using Godot;

namespace demo.Scripts.UI;

public partial class UIContainer : VBoxContainer
{
    [Export] public ContainerType Container { get; private set; }
}