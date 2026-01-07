using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.InputSystem;
using System.Text;

#if UNITY_EDITOR
namespace Kronos.Input.Editor
{
    public class InputActionGenerate : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/Kronos/Input Processor Script", false, 20)]
        public static void GenerateScriptFromAsset()
        {
            // Get the selected object
            Object selected = Selection.activeObject;
            if(selected == null || !(selected is InputActionAsset asset))
            {
                Debug.LogWarning("Please select an Input Action Asset first.");
                return;
            }

            string assetName = selected.name;
            string path = AssetDatabase.GetAssetPath(selected);
            string directory = Path.GetDirectoryName(path);
            string newFilePath = Path.Combine(directory, $"{assetName}Processor.cs");

            string script = CreateScript(asset, assetName);

            File.WriteAllText(newFilePath, script);
            AssetDatabase.Refresh();
            Debug.Log($"Succesfully created: {newFilePath}");
        }

        [MenuItem("Assets/Create/Kronos/Input Processor Script", true)]
        public static bool ValidateGenerateScriptFromAsset()
        {
            return Selection.activeObject is InputActionAsset;
        }

        static string CreateScript(InputActionAsset asset, string assetName)
        {
             StringBuilder sb = new StringBuilder();

            // 1. Headers & Namespace
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using Unity.Netcode;");
            sb.AppendLine("using UnityEngine.InputSystem;");
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Reflection;");
            sb.AppendLine("");
            sb.AppendLine("namespace Kronos.Input");
            sb.AppendLine("{");
            sb.AppendLine($"    public class {assetName}Processor : MonoBehaviour");
            sb.AppendLine("    {");
            sb.AppendLine("        public InputActionAsset InputActions;");
            sb.AppendLine("");

            // 2. Variables & Events Loop
            foreach (var map in asset.actionMaps)
            {
                sb.AppendLine($"        // --- Map: {map.name} ---");
                foreach (var action in map.actions)
                {
                    string cleanName = char.ToLower(action.name[0]) + action.name.Substring(1);
                    sb.AppendLine($"        private InputAction {cleanName}Action;");
                    sb.AppendLine($"        public event Action On{action.name}Performed;");
                    sb.AppendLine($"        public event Action On{action.name}Canceled;");
                }
                sb.AppendLine("");
            }
            sb.AppendLine("         Vector2 _input;");
            sb.AppendLine("         Vector2 _look;");
            sb.AppendLine("         public Vector2 Input => _input;");
            sb.AppendLine("         public Vector2 Look => _look;");
            // 3. Unity Callbacks
            sb.AppendLine("        void Awake() => InitializeInputActions();");
            sb.AppendLine("");
            sb.AppendLine("        public void Enable()");
            sb.AppendLine("        {");
            foreach (var map in asset.actionMaps)
            {
                sb.AppendLine($"            InputActions.FindActionMap(\"{map.name}\").Enable();");
            }
            sb.AppendLine("            BindEvents(true);");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        public void Disable()");
            sb.AppendLine("        {");
            sb.AppendLine("            BindEvents(false);");
            sb.AppendLine("        }");
            sb.AppendLine("");
            sb.AppendLine("        public void Update()");
            sb.AppendLine("        {");
            sb.AppendLine("            _input = moveAction.ReadValue<Vector2>();");
            sb.AppendLine("            _look = lookAction.ReadValue<Vector2>();");
            sb.AppendLine("        }");
            sb.AppendLine("");

            // 4. BindEvents Function
            sb.AppendLine("        private void BindEvents(bool bind)");
            sb.AppendLine("        {");
            foreach (var map in asset.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    string cleanName = char.ToLower(action.name[0]) + action.name.Substring(1);
                    sb.AppendLine("            if(bind) {");
                    sb.AppendLine($"                {cleanName}Action.performed += ctx => On{action.name}Performed?.Invoke();");
                    sb.AppendLine($"                {cleanName}Action.canceled += ctx => On{action.name}Canceled?.Invoke();");
                    sb.AppendLine("             }");
                    sb.AppendLine("            else {");
                    sb.AppendLine($"                    {cleanName}Action.performed -= ctx => On{action.name}Performed?.Invoke();");
                    sb.AppendLine($"                    {cleanName}Action.canceled -= ctx => On{action.name}Canceled?.Invoke();");
                    sb.AppendLine("             }");
                }
            }
            sb.AppendLine("        }");
            sb.AppendLine("");

            // 5. Reflection Logic (The "Magic")
            sb.AppendLine("        void InitializeInputActions()");
            sb.AppendLine("        {");
            sb.AppendLine("            FieldInfo[] fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);");
            sb.AppendLine("            foreach(FieldInfo field in fields)");
            sb.AppendLine("            {");
            sb.AppendLine("                if(field.Name.EndsWith(\"Action\"))");
            sb.AppendLine("                {");
            sb.AppendLine("                    string aName = char.ToUpper(field.Name[0]) + field.Name.Substring(1).Replace(\"Action\", \"\");");
            sb.AppendLine("                    field.SetValue(this, InputActions.FindAction(aName));");
            sb.AppendLine("                }");
            sb.AppendLine("            }");
            sb.AppendLine("        }");

            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
#endif