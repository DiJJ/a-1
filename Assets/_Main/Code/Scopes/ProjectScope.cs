using a1.PlayerCharacter;
using a1.ScriptableObjects;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace a1.Scopes
{
    [DisallowMultipleComponent]
    public abstract class BaseLifetimeScope : LifetimeScope
    {
        protected virtual void RegisterMessageBrokers(in IContainerBuilder builder, out MessagePipeOptions options)
        {
            options = builder.RegisterMessagePipe();
        }
    }
    
    public sealed class ProjectScope : BaseLifetimeScope
    {
        [SerializeField] private CameraDataSO m_cameraData;
        [SerializeField] private CharacterStats m_characterStats;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessageBrokers(builder, out _);
            
            RegisterScriptableObjects(builder);
            RegisterServices(builder);
        }

        private void RegisterServices(in IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<InputService>().As<IInputService>();
            
            this.LogRegisterSuccess();
        }
        
        private void RegisterScriptableObjects(in IContainerBuilder builder)
        {
            builder.RegisterInstance(m_cameraData);
            builder.RegisterInstance(m_characterStats);
        }

        protected override void RegisterMessageBrokers(in IContainerBuilder builder, out MessagePipeOptions options)
        {
            base.RegisterMessageBrokers(in builder, out options);
            
            builder.RegisterBuildCallback(c => GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        }
    }
}