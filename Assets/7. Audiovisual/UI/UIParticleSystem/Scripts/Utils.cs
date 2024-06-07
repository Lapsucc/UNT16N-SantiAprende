using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Coffee.UIParticleExtensions
{
    internal static class SpriteExtensions
    {
#if UNITY_EDITOR
        private static Type tSpriteEditorExtension =
            Type.GetType("UnityEditor.Experimental.U2D.SpriteEditorExtension, UnityEditor")
            ?? Type.GetType("UnityEditor.U2D.SpriteEditorExtension, UnityEditor");

        private static MethodInfo miGetActiveAtlasTexture = tSpriteEditorExtension
            .GetMethod("GetActiveAtlasTexture", BindingFlags.Static | BindingFlags.NonPublic);

        public static Texture2D GetActualTexture(this Sprite self)
        {
            if (!self) return null;

            if (Application.isPlaying) return self.texture;
            Texture2D ret = miGetActiveAtlasTexture.Invoke(null, new[] {self}) as Texture2D;
            return ret ? ret : self.texture;
        }
#else
        internal static Texture2D GetActualTexture(this Sprite self)
        {
            return self ? self.texture : null;
        }
#endif
    }

    internal static class ListExtensions
    {
        public static bool SequenceEqualFast(this List<bool> self, List<bool> value)
        {
            if (self.Count != value.Count) return false;
            for (int i = 0; i < self.Count; ++i)
            {
                if (self[i] != value[i]) return false;
            }

            return true;
        }

        public static int CountFast(this List<bool> self)
        {
            int count = 0;
            for (int i = 0; i < self.Count; ++i)
            {
                if (self[i]) count++;
            }

            return count;
        }

        public static bool AnyFast<T>(this List<T> self) where T : Object
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (self[i]) return true;
            }

            return false;
        }

        public static bool AnyFast<T>(this List<T> self, Predicate<T> predicate) where T : Object
        {
            for (int i = 0; i < self.Count; ++i)
            {
                if (self[i] && predicate(self[i])) return true;
            }

            return false;
        }
    }

    internal static class MeshExtensions
    {
        // static readonly List<Color32> s_Colors = new List<Color32>();

        // public static void ModifyColorSpaceToLinear(this Mesh self)
        // {
        //     self.GetColors(s_Colors);
        //
        //     for (var i = 0; i < s_Colors.Count; i++)
        //         s_Colors[i] = ((Color) s_Colors[i]).gamma;
        //
        //     self.SetColors(s_Colors);
        //     s_Colors.Clear();
        // }

        public static void Clear(this CombineInstance[] self)
        {
            for (int i = 0; i < self.Length; i++)
            {
                MeshPool.Return(self[i].mesh);
                self[i].mesh = null;
            }
        }
    }

    internal static class MeshPool
    {
        private static readonly Stack<Mesh> s_Pool = new(32);
        private static readonly HashSet<int> s_HashPool = new();

        public static void Init()
        {
        }

        static MeshPool()
        {
            for (int i = 0; i < 32; i++)
            {
                Mesh m = new();
                m.MarkDynamic();
                s_Pool.Push(m);
                s_HashPool.Add(m.GetInstanceID());
            }
        }

        public static Mesh Rent()
        {
            Mesh m;
            while (0 < s_Pool.Count)
            {
                m = s_Pool.Pop();
                if (m)
                {
                    s_HashPool.Remove(m.GetInstanceID());
                    return m;
                }
            }

            m = new Mesh();
            m.MarkDynamic();
            return m;
        }

        public static void Return(Mesh mesh)
        {
            if (!mesh) return;

            int id = mesh.GetInstanceID();
            if (s_HashPool.Contains(id)) return;

            mesh.Clear(false);
            s_Pool.Push(mesh);
            s_HashPool.Add(id);
        }
    }

    internal static class CombineInstanceArrayPool
    {
        private static readonly Dictionary<int, CombineInstance[]> s_Pool;

        public static void Init()
        {
            s_Pool.Clear();
        }

        static CombineInstanceArrayPool()
        {
            s_Pool = new Dictionary<int, CombineInstance[]>();
        }

        public static CombineInstance[] Get(List<CombineInstance> src)
        {
            CombineInstance[] dst;
            int count = src.Count;
            if (!s_Pool.TryGetValue(count, out dst))
            {
                dst = new CombineInstance[count];
                s_Pool.Add(count, dst);
            }

            for (int i = 0; i < src.Count; i++)
            {
                dst[i].mesh = src[i].mesh;
                dst[i].transform = src[i].transform;
            }

            return dst;
        }

        public static CombineInstance[] Get(List<CombineInstanceEx> src, int count)
        {
            CombineInstance[] dst;
            if (!s_Pool.TryGetValue(count, out dst))
            {
                dst = new CombineInstance[count];
                s_Pool.Add(count, dst);
            }

            for (int i = 0; i < count; i++)
            {
                dst[i].mesh = src[i].mesh;
                dst[i].transform = src[i].transform;
            }

            return dst;
        }
    }

    internal static class ParticleSystemExtensions
    {
        public static void SortForRendering(this List<ParticleSystem> self, Transform transform, bool sortByMaterial)
        {
            self.Sort((a, b) =>
            {
                Transform tr = transform;
                ParticleSystemRenderer aRenderer = a.GetComponent<ParticleSystemRenderer>();
                ParticleSystemRenderer bRenderer = b.GetComponent<ParticleSystemRenderer>();

                // Render queue: ascending
                Material aMat = aRenderer.sharedMaterial ?? aRenderer.trailMaterial;
                Material bMat = bRenderer.sharedMaterial ?? bRenderer.trailMaterial;
                if (!aMat && !bMat) return 0;
                if (!aMat) return -1;
                if (!bMat) return 1;

                if (sortByMaterial)
                    return aMat.GetInstanceID() - bMat.GetInstanceID();

                if (aMat.renderQueue != bMat.renderQueue)
                    return aMat.renderQueue - bMat.renderQueue;

                // Sorting layer: ascending
                if (aRenderer.sortingLayerID != bRenderer.sortingLayerID)
                    return aRenderer.sortingLayerID - bRenderer.sortingLayerID;

                // Sorting order: ascending
                if (aRenderer.sortingOrder != bRenderer.sortingOrder)
                    return aRenderer.sortingOrder - bRenderer.sortingOrder;

                // Z position & sortingFudge: descending
                Transform aTransform = a.transform;
                Transform bTransform = b.transform;
                float aPos = tr.InverseTransformPoint(aTransform.position).z + aRenderer.sortingFudge;
                float bPos = tr.InverseTransformPoint(bTransform.position).z + bRenderer.sortingFudge;
                if (!Mathf.Approximately(aPos, bPos))
                    return (int) Mathf.Sign(bPos - aPos);

                return (int) Mathf.Sign(GetIndex(self, a) - GetIndex(self, b));
            });
        }

        private static int GetIndex(IList<ParticleSystem> list, Object ps)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetInstanceID() == ps.GetInstanceID()) return i;
            }

            return 0;
        }

        public static long GetMaterialHash(this ParticleSystem self, bool trail)
        {
            if (!self) return 0;

            ParticleSystemRenderer r = self.GetComponent<ParticleSystemRenderer>();
            Material mat = trail ? r.trailMaterial : r.sharedMaterial;

            if (!mat) return 0;

            Texture2D tex = trail ? null : self.GetTextureForSprite();
            return ((long) mat.GetHashCode() << 32) + (tex ? tex.GetHashCode() : 0);
        }

        public static Texture2D GetTextureForSprite(this ParticleSystem self)
        {
            if (!self) return null;

            // Get sprite's texture.
            ParticleSystem.TextureSheetAnimationModule tsaModule = self.textureSheetAnimation;
            if (!tsaModule.enabled || tsaModule.mode != ParticleSystemAnimationMode.Sprites) return null;

            for (int i = 0; i < tsaModule.spriteCount; i++)
            {
                Sprite sprite = tsaModule.GetSprite(i);
                if (!sprite) continue;

                return sprite.GetActualTexture();
            }

            return null;
        }

        public static void Exec(this List<ParticleSystem> self, Action<ParticleSystem> action)
        {
            self.RemoveAll(p => !p);
            self.ForEach(action);
        }
    }
}
