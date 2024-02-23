using UnityEngine;
using Zenject;

namespace GlassyCode.PokePresenter.Core.Api
{
    public class ApiConnectionInstaller : MonoInstaller
    {
        [SerializeField] private ApiConfig _apiConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_apiConfig);
            Container.Bind<ApiConnection>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}