using System.Collections.Generic;
using UnityEngine;

namespace Coffee.UIParticleExtensions
{
    internal class CombineInstanceEx
    {
        private int count;
        public long hash = -1;
        public int index = -1;
        private readonly List<CombineInstance> combineInstances = new(32);
        public Mesh mesh;
        public Matrix4x4 transform;

        public void Combine()
        {
            switch (count)
            {
                case 0:
                    return;
                case 1:
                    mesh = combineInstances[0].mesh;
                    transform = combineInstances[0].transform;
                    return;
                default:
                {
                        CombineInstance[] cis = CombineInstanceArrayPool.Get(combineInstances);
                    mesh = MeshPool.Rent();
                    mesh.CombineMeshes(cis, true, true);
                    transform = Matrix4x4.identity;
                    cis.Clear();
                    return;
                }
            }
        }

        public void Clear()
        {
            for (int i = 0; i < combineInstances.Count; i++)
            {
                CombineInstance inst = combineInstances[i];
                MeshPool.Return(inst.mesh);
                inst.mesh = null;
                combineInstances[i] = inst;
            }

            combineInstances.Clear();

            MeshPool.Return(mesh);
            mesh = null;

            count = 0;
            hash = -1;
            index = -1;
        }

        public void Push(Mesh mesh, Matrix4x4 transform)
        {
            combineInstances.Add(new CombineInstance {mesh = mesh, transform = transform});
            count++;
        }
    }
}
