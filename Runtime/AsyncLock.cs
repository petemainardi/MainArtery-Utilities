using System;
using System.Threading;
using System.Threading.Tasks;

namespace MainArtery.Utilities
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
	 *  A wrapper for SemaphoreSlim to provide a convenient locking mechanism for async contexts.
	 *  The locking methods return an IDisposable to allow for use in using() blocks.
	 *  
	 *  Based on code from the following:
	 *  - https://stackoverflow.com/questions/21011179/how-to-protect-resources-that-may-be-used-in-a-multi-threaded-or-async-environme/21011273#21011273
	 *  - https://devblogs.microsoft.com/pfxteam/building-async-coordination-primitives-part-6-asynclock/
	 */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public class AsyncLock
    {
        /// ==================================================================================
        /// Wrapper for managing the release of a semaphore through IDisposable
        /// ==================================================================================
        private readonly struct Releaser : IDisposable
		{
			private readonly SemaphoreSlim _semaphore;
			public Releaser(SemaphoreSlim semaphore) => _semaphore = semaphore;
			public void Dispose() => _semaphore.Release();
		}

		/// ==================================================================================
		/// Fields & Properties
		/// ==================================================================================
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		private readonly Task<IDisposable> _releaserTask;
		private readonly IDisposable _releaser;

        /// ==================================================================================
        /// Constructor
        /// ==================================================================================
		public AsyncLock()
		{
			_releaser = new Releaser(_semaphore);
			_releaserTask = Task.FromResult(_releaser);
		}

        /// ==================================================================================
        /// Methods
        /// ==================================================================================
		public IDisposable Lock()
		{
			_semaphore.Wait();
			return _releaser;
		}

		public Task<IDisposable> LockAsync()
		{
			Task waitTask = _semaphore.WaitAsync();
			return waitTask.IsCompleted
				? _releaserTask
				: waitTask.ContinueWith(
					(_, releaser) => (IDisposable) releaser,
					_releaser,
					CancellationToken.None,
					TaskContinuationOptions.ExecuteSynchronously,
					TaskScheduler.Default
					);
		}
        /// ==================================================================================
	}
	/// ===========================================================================================
	/// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// ===========================================================================================
}
