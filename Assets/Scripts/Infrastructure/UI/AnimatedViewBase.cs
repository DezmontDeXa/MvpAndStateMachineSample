using UnityEngine;

namespace Infrastructure
{
	[RequireComponent(typeof(ViewAnimator))]
	public abstract class AnimatedViewBase : MonoBehaviour, IView
	{
		private ViewAnimator _animator;

		protected virtual void Awake()
		{
			_animator = GetComponent<ViewAnimator>();
		}

		public void Show() => _animator.Show();

		public void Restore() => _animator.Restore();

		public void Hide() => _animator.Hide();

		public void Back() => _animator.Back();
	}
}