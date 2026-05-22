using UnityEditor;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor.UI;
#endif

public class ButtonEx : Button
{
    public UnityEvent onDownEvents;
    public UnityEvent onUpEvents;
    public UnityEvent onPressEvents;

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
        isExit = false;
        onDownEvents.Invoke();
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

            EditorGUILayout.PropertyField(serializedObject.FindProperty("onDownEvents"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpEvents"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onPressEvents"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}
