using NaughtyAttributes;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Stars
{
    [DisallowMultipleComponent]
    public sealed class Test : MonoBehaviour
    {
        [SerializeField] private int a;
        [SerializeField] private int b;
        [Button]
        public void Test1()
        {
            int c = a / b;
            Debug.Log(c);
        }
    }
}