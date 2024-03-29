using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;

public class CloseExaminePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.onStudyModeChanged?.Invoke(STEPS.NONE, ACTIONS.NONE);
        Controller.onResetField?.Invoke();
        ExaminePanel.SetPanelVisibility(false);
        Camera.SetCameraPosition(Camera.DigitalMatrixPosition, Camera.DigitalMatrixRotation);
    }
}
