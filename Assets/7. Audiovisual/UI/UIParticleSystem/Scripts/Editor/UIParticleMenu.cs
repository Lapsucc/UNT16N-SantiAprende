using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.UIExtensions
{
    internal class UIParticleMenu
    {
        [MenuItem("GameObject/UI/Particle System (Empty)", false, 2018)]
        private static void AddParticleEmpty(MenuCommand menuCommand)
        {
            // Create empty UI element.
            EditorApplication.ExecuteMenuItem("GameObject/UI/Image");
            GameObject ui = Selection.activeGameObject;
            Object.DestroyImmediate(ui.GetComponent<Image>());

            // Add UIParticle.
            UIParticle uiParticle = ui.AddComponent<UIParticle>();
            uiParticle.name = "UIParticle";
            uiParticle.scale = 10;
            uiParticle.rectTransform.sizeDelta = Vector2.zero;
        }

        [MenuItem("GameObject/UI/Particle System", false, 2019)]
        private static void AddParticle(MenuCommand menuCommand)
        {
            // Create empty UIEffect.
            AddParticleEmpty(menuCommand);
            UIParticle uiParticle = Selection.activeGameObject.GetComponent<UIParticle>();

            // Create ParticleSystem.
            EditorApplication.ExecuteMenuItem("GameObject/Effects/Particle System");
            GameObject ps = Selection.activeGameObject;
            ps.transform.SetParent(uiParticle.transform, false);
            ps.transform.localPosition = Vector3.zero;

            // Assign default material.
            ParticleSystemRenderer renderer = ps.GetComponent<ParticleSystemRenderer>();
            Material defaultMat = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Particle.mat");
            renderer.material = defaultMat ? defaultMat : renderer.material;

            // Refresh particles.
            uiParticle.RefreshParticles();
        }
    }
}
