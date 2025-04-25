using UnityEngine;
namespace ECS
{
    public class ComponentView : MonoBehaviour
    {
        public string Type;
        public object Component { get; set; }
    }
}