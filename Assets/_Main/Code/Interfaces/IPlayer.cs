using UnityEngine;

namespace a1.Interfaces
{
    public interface IPlayer : ICharacter
    {
        Transform CameraRoot { get; }
    }
}