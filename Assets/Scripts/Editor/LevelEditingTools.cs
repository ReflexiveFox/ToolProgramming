using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class LevelEditingTools : EditorWindow
{
    private Slider slider;
    [MenuItem("FITSTIC/Level Editing")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow wnd = GetWindow<LevelEditingTools>();
        wnd.titleContent = new GUIContent("Level Tools");
    }
    
    public void CreateGUI()
    {
        StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/ToolsWindow.uss");
        rootVisualElement.styleSheets.Add( uss );
        VisualElement barrelPanel = new  GroupBox("Explosive Barrels");
        barrelPanel.AddToClassList("barrelPanel");

        slider = new  Slider("CreateDistance");
        slider.value = 1;
       
        slider.RegisterValueChangedCallback((changeEvent) => slider.label = "CreateDistance "+changeEvent.newValue.ToString());
        
        
        Transform ctr = SceneView.lastActiveSceneView.camera.transform;
        Button createBarrellBtn = new Button(() => BarrelsManager.Instance.CreateBarrel("BarrelName", ctr.position+ctr.forward*slider.value));
        createBarrellBtn.text = "Create";
        
        Button dropBarrelBtn = new Button(() => BarrelsManager.Instance.DropBarrel(Selection.activeTransform));
        dropBarrelBtn.text = "Drop Selected";
        
        Button dropAllBarrelsBtn = new Button( () => BarrelsManager.Instance.DropAllBarrels());
        dropAllBarrelsBtn.text = "Drop All";
        
        
        barrelPanel.Add(slider);

        barrelPanel.Add(createBarrellBtn);
        barrelPanel.Add(dropBarrelBtn);
        barrelPanel.Add(dropAllBarrelsBtn);
        
        rootVisualElement.Add(barrelPanel);
    }
}