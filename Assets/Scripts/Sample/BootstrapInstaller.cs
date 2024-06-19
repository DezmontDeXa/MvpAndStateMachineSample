using Infrastructure;
using UnityEngine;
using Zenject;

namespace Sample
{
	[CreateAssetMenu(menuName = "Infrastructure/Bootstrap Installer")]
	public class BootstrapInstaller : ScriptableObjectInstaller<BootstrapInstaller>
	{
		[SerializeField] private Canvas _uiCanvas;
		[SerializeField] private WelcomePage _welcomePagePrefab;

		public override void InstallBindings()
		{
			Container
				.BindInterfacesAndSelfTo<UIStateMachine>()
				.AsSingle();

			BindViews();

			BindPresenters();

			Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

			Container.Bind<Bootstrap>().ToSelf().AsSingle().NonLazy();
		}

		private void BindPresenters()
		{
			Container.BindInterfacesAndSelfTo<WelcomePagePresenter>().AsSingle().NonLazy();
		}

		private void BindViews()
		{
			Container
				.BindInterfacesAndSelfTo<WelcomePage>()
				.FromComponentInNewPrefab(_welcomePagePrefab)
				.UnderTransform(_uiCanvas.transform)
				.AsSingle();
		}
	}
}
