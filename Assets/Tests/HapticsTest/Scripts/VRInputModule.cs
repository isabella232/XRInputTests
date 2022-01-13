using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

[RequireComponent(typeof(XRInput))]
[AddComponentMenu("Event/VR Input Module")]
public class VRInputModule : StandaloneInputModule
{
    protected override void Awake()
    {
        m_InputOverride = GetComponent<XRInput>();
    }

    public override bool IsModuleSupported()
    {
        return base.IsModuleSupported() && XRSettings.isDeviceActive;
    }

    protected override MouseState GetMousePointerEventData(int id)
    {
        var state = base.GetMousePointerEventData(id);
        var button = state.GetButtonState(PointerEventData.InputButton.Left);

        // HACK: Setting delta to non-zero makes the pointer be moving. It doesn't look like this value used beyond that.
        // This is required in order for dragging to work with the VR input.
        button.eventData.buttonData.delta = Vector2.one;

        state.SetButtonState(PointerEventData.InputButton.Left, button.eventData.buttonState, button.eventData.buttonData);
        return state;
    }
}
