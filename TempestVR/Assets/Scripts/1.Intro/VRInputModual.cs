using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModual : BaseInputModule {
    public Camera ControllerCam;
    public SteamVR_Input_Sources Target_Controller;
    public SteamVR_Action_Boolean Click;

    private GameObject current_object = null;
    private PointerEventData data = null;

    //why can't this be start
    protected override void Awake()
    {
        base.Awake();
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //Reset data
        data.Reset();
        data.position = new Vector2(ControllerCam.pixelWidth / 2, ControllerCam.pixelHeight / 2);
        //Raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        current_object = data.pointerCurrentRaycast.gameObject;
        //clear
        m_RaycastResultCache.Clear();
        //hover
        HandlePointerExitAndEnter(data, current_object);
        //press/release
        if (Click.GetStateDown(Target_Controller))
        {
            Process_press(data);
        }
        if (Click.GetStateUp(Target_Controller))
        {
            Process_release(data);
        }

    }

    public PointerEventData GetData()
    {
        return data;
    }
    //update
    private void Process_press(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(current_object, data, ExecuteEvents.pointerDownHandler);

        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current_object);
        }
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = current_object;


    }
    private void Process_release(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(current_object);

        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

}
