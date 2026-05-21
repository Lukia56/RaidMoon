using UnityEditor;
//using UnityEditor.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEx : Button
{
    public UnityEvent onPressEvents;
    public UnityEvent onUpEvents;

    private bool isPressed;
    private bool isExit;

    private void Update()
    {
        if (isPressed)
        {
            onPressEvents.Invoke();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (!isExit)
        {
            onUpEvents.Invoke();
        }
        isPressed = false;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isPressed = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        isExit = true;
    }

#if UNITY_EDITOR

    [CanEditMultipleObjects, CustomEditor(typeof(ButtonEx), true)]
    public class ButtonExEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpEvents"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onPressEvents"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
