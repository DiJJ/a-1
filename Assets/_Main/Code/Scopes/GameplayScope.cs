using a1.Interfaces;
using a1.PlayerCharacter;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace a1.Scopes
{
    public sealed class GameplayScope : BaseLifetimeScope
    {
        [SerializeField]
        private AssetReference m_playerAssetReference;
        private GameObject m_player;

        protected override async void Awake()
        {
            base.Awake();
            
            await LoadAssets();
            Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterServices(builder);
        }
        
        private void RegisterServices(in IContainerBuilder builder)
        {
            PlayerReference playerReference = GameObject.Instantiate(m_player).GetComponent<PlayerReference>();

            builder.RegisterInstance(playerReference).As<IPlayer>();
            builder.RegisterEntryPoint<CameraController>().As<ICameraController>();
            
            builder.RegisterBuildCallback(c =>
            {
                c.InjectGameObject(playerReference.gameObject);
            });
            
            this.LogRegisterSuccess();
        }

        private async UniTask LoadAssets()
        {
            m_player = await m_playerAssetReference.LoadAssetAsync<GameObject>();
        }
    }
}