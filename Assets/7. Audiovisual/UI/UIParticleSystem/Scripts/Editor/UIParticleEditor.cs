using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UI;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions {
    [CustomEditor (typeof (UIParticle))]
    [CanEditMultipleObjects]
    internal class UIParticleEditor : GraphicEditor {
        //################################
        // Constant or Static Members.
        //################################
        private static readonly GUIContent s_ContentRenderingOrder = new("Rendering Order");
        private static readonly GUIContent s_ContentRefresh = new("Refresh");
        private static readonly GUIContent s_ContentFix = new("Fix");
        private static readonly GUIContent s_ContentMaterial = new("Material");
        private static readonly GUIContent s_ContentTrailMaterial = new("Trail Material");
        private static readonly GUIContent s_Content3D = new("3D");
        private static readonly GUIContent s_ContentScale = new("Scale");

        private SerializedProperty _spMaskable;
        private SerializedProperty _spScale;
        private SerializedProperty _spIgnoreCanvasScaler;
        private SerializedProperty _spAnimatableProperties;
        private SerializedProperty _spShrinkByMaterial;

        private ReorderableList _ro;
        private bool _xyzMode;
        private bool _showMaterials;

        private static readonly List<string> s_MaskablePropertyNames = new() {
            "_Stencil",
            "_StencilComp",
            "_StencilOp",
            "_StencilWriteMask",
            "_StencilReadMask",
            "_ColorMask",
        };

        //################################
        // Public/Protected Members.
        //################################
        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        protected override void OnEnable () {
            base.OnEnable ();
            _spMaskable = serializedObject.FindProperty ("m_Maskable");
            _spScale = serializedObject.FindProperty ("m_Scale3D");
            _spIgnoreCanvasScaler = serializedObject.FindProperty ("m_IgnoreCanvasScaler");
            _spAnimatableProperties = serializedObject.FindProperty ("m_AnimatableProperties");
            _spShrinkByMaterial = serializedObject.FindProperty ("m_ShrinkByMaterial");
            _showMaterials = EditorPrefs.GetBool ("Coffee.UIExtensions.UIParticleEditor._showMaterials", true);

            SerializedProperty sp = serializedObject.FindProperty ("m_Particles");
            _ro = new ReorderableList (sp.serializedObject, sp, true, true, true, true);
            _ro.elementHeight = EditorGUIUtility.singleLineHeight * 3 + 4;
            _ro.elementHeightCallback = _ => _showMaterials ?
                3 * (EditorGUIUtility.singleLineHeight + 2) :
                EditorGUIUtility.singleLineHeight + 2;
            _ro.drawElementCallback = (rect, index, active, focused) => {
                EditorGUI.BeginDisabledGroup (sp.hasMultipleDifferentValues);
                rect.y += 1;
                rect.height = EditorGUIUtility.singleLineHeight;
                SerializedProperty p = sp.GetArrayElementAtIndex (index);
                EditorGUI.ObjectField (rect, p, GUIContent.none);
                if (!_showMaterials) return;

                rect.x += 15;
                rect.width -= 15;
                ParticleSystem ps = p.objectReferenceValue as ParticleSystem;
                SerializedProperty materials = ps ?
                    new SerializedObject (ps.GetComponent<ParticleSystemRenderer> ()).FindProperty ("m_Materials") :
                    null;
                rect.y += rect.height + 1;
                MaterialField (rect, s_ContentMaterial, materials, 0);
                rect.y += rect.height + 1;
                MaterialField (rect, s_ContentTrailMaterial, materials, 1);
                EditorGUI.EndDisabledGroup ();
                if (materials != null) {
                    materials.serializedObject.ApplyModifiedProperties ();
                }
            };
            _ro.drawHeaderCallback += rect => {
#if !UNITY_2019_3_OR_NEWER
                rect.y -= 1;
#endif
                EditorGUI.LabelField (new Rect (rect.x, rect.y, 150, rect.height), s_ContentRenderingOrder);

                GUIContent content = EditorGUIUtility.IconContent (_showMaterials ? "VisibilityOn" : "VisibilityOff");
                _showMaterials = GUI.Toggle (new Rect (rect.width - 55, rect.y, 24, 20), _showMaterials, content, EditorStyles.label);

                if (GUI.Button (new Rect (rect.width - 35, rect.y, 60, rect.height), s_ContentRefresh, EditorStyles.miniButton)) {
                    foreach (UIParticle t in targets) {
                        t.RefreshParticles ();
                    }
                }
            };
        }

        private static void MaterialField (Rect rect, GUIContent label, SerializedProperty sp, int index) {
            if (sp == null || sp.arraySize <= index) {
                EditorGUI.BeginDisabledGroup (true);
                EditorGUI.ObjectField (rect, label, null, typeof (Material), true);
                EditorGUI.EndDisabledGroup ();
            } else {
                EditorGUI.PropertyField (rect, sp.GetArrayElementAtIndex (index), label);
            }
        }

        /// <summary>
        /// Implement this function to make a custom inspector.
        /// </summary>
        public override void OnInspectorGUI () {
            UIParticle current = target as UIParticle;
            if (current == null) return;

            serializedObject.Update ();

            // Maskable
            EditorGUILayout.PropertyField (_spMaskable);

            // IgnoreCanvasScaler
            using (EditorGUI.ChangeCheckScope ccs = new()) {
                EditorGUILayout.PropertyField (_spIgnoreCanvasScaler);
                if (ccs.changed) {
                    foreach (UIParticle p in targets) {
                        p.ignoreCanvasScaler = _spIgnoreCanvasScaler.boolValue;
                    }
                }
            }

            // Scale
            _xyzMode = DrawFloatOrVector3Field (_spScale, _xyzMode);

            // AnimatableProperties
            Material[] mats = current.particles
                .Where (x => x)
                .Select (x => x.GetComponent<ParticleSystemRenderer> ().sharedMaterial)
                .Where (x => x)
                .ToArray ();

            // Animated properties
            EditorGUI.BeginChangeCheck ();
            AnimatedPropertiesEditor.DrawAnimatableProperties (_spAnimatableProperties, mats);
            if (EditorGUI.EndChangeCheck ()) {
                foreach (UIParticle t in targets)
                    t.SetMaterialDirty ();
            }

            // ShrinkByMaterial
            EditorGUILayout.PropertyField (_spShrinkByMaterial);

            // Target ParticleSystems.
            _ro.DoLayoutList ();

            serializedObject.ApplyModifiedProperties ();

            // Does the shader support UI masks?
            if (current.maskable && current.GetComponentInParent<Mask> ()) {
                foreach (Material mat in current.materials) {
                    if (!mat || !mat.shader) continue;
                    Shader shader = mat.shader;
                    foreach (string propName in s_MaskablePropertyNames) {
                        if (mat.HasProperty (propName)) continue;

                        EditorGUILayout.HelpBox (string.Format ("Shader '{0}' doesn't have '{1}' property. This graphic cannot be masked.", shader.name, propName), MessageType.Warning);
                        break;
                    }
                }
            }

            // Does the shader support UI masks?

            if (FixButton (current.m_IsTrail, "This UIParticle component should be removed. The UIParticle for trails is no longer needed.")) {
                DestroyUIParticle (current);
                return;
            }
        }

        void DestroyUIParticle (UIParticle p, bool ignoreCurrent = false) {
            if (!p || ignoreCurrent && target == p) return;

            CanvasRenderer cr = p.canvasRenderer;
            DestroyImmediate (p);
            DestroyImmediate (cr);

#if UNITY_2018_3_OR_NEWER
            UnityEditor.SceneManagement.PrefabStage stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage ();
            if (stage != null && stage.scene.isLoaded) {
#if UNITY_2020_1_OR_NEWER
                string prefabAssetPath = stage.assetPath;
#else
                string prefabAssetPath = stage.prefabAssetPath;
#endif
                PrefabUtility.SaveAsPrefabAsset (stage.prefabContentsRoot, prefabAssetPath);
            }
#endif
        }

        bool FixButton (bool show, string text) {
            if (!show) return false;
            using (new EditorGUILayout.HorizontalScope (GUILayout.ExpandWidth (true))) {
                EditorGUILayout.HelpBox (text, MessageType.Warning, true);
                using (new EditorGUILayout.VerticalScope ()) {
                    return GUILayout.Button (s_ContentFix, GUILayout.Width (30));
                }
            }
        }

        private static bool DrawFloatOrVector3Field (SerializedProperty sp, bool showXyz) {
            SerializedProperty x = sp.FindPropertyRelative ("x");
            SerializedProperty y = sp.FindPropertyRelative ("y");
            SerializedProperty z = sp.FindPropertyRelative ("z");

            showXyz |= !Mathf.Approximately (x.floatValue, y.floatValue) ||
                !Mathf.Approximately (y.floatValue, z.floatValue) ||
                y.hasMultipleDifferentValues ||
                z.hasMultipleDifferentValues;

            EditorGUILayout.BeginHorizontal ();
            if (showXyz) {
                EditorGUI.BeginChangeCheck ();
                EditorGUILayout.PropertyField (sp);
                if (EditorGUI.EndChangeCheck ()) {
                    x.floatValue = Mathf.Max (0.001f, x.floatValue);
                    y.floatValue = Mathf.Max (0.001f, y.floatValue);
                    z.floatValue = Mathf.Max (0.001f, z.floatValue);
                }
            } else {
                EditorGUI.BeginChangeCheck ();
                EditorGUILayout.PropertyField (x, s_ContentScale);
                if (EditorGUI.EndChangeCheck ()) {
                    x.floatValue = Mathf.Max (0.001f, x.floatValue);
                    y.floatValue = Mathf.Max (0.001f, x.floatValue);
                    z.floatValue = Mathf.Max (0.001f, x.floatValue);
                }
            }

            EditorGUI.BeginChangeCheck ();
            showXyz = GUILayout.Toggle (showXyz, s_Content3D, EditorStyles.miniButton, GUILayout.Width (30));
            if (EditorGUI.EndChangeCheck () && !showXyz)
                z.floatValue = y.floatValue = x.floatValue;
            EditorGUILayout.EndHorizontal ();

            return showXyz;
        }
    }
}
