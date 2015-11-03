using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CharMap_Plus.Model
{
    public class IncrementalSource<T, K> : ObservableCollection<K>, ISupportIncrementalLoading
        where T : IPagedSource<K>
    {
        private int VirtualCount { get; set; }
        private int CurrentPage { get; set; }

        private IPagedSource<K> _source;
        public IPagedSource<K> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                this.VirtualCount = int.MaxValue;
                this.CurrentPage = 0;
                LoadMoreItemsAsync(100);
            }
        }

        public IncrementalSource() : this(null)
        { }

        public IncrementalSource(IPagedSource<K> source)
        {
            this.Source = source;
            this.VirtualCount = int.MaxValue;
            this.CurrentPage = 0;
        }

        #region ISupportIncrementalLoading

        public bool HasMoreItems
        {
            get { return this.VirtualCount > this.CurrentPage * 25; }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (this.Source == null)
            {
                return Task.FromResult(new LoadMoreItemsResult() { Count = 0 }).AsAsyncOperation<LoadMoreItemsResult>();
            }

            Debug.WriteLine("Loading More Items " + count);

            var nextPageItems = this.Source.GetPage(++this.CurrentPage, (int)count);

            if (false)
            {
                IPagedResponse<K> result = nextPageItems.Result;

                this.VirtualCount = result.VirtualCount;

                foreach (K item in result.Items)
                    this.Add(item);

                return Task.FromResult(new LoadMoreItemsResult() { Count = (uint)result.Items.Count() }).AsAsyncOperation<LoadMoreItemsResult>();
            }
            else
            {
                CoreDispatcher dispatcher = Window.Current.Dispatcher;
                
                return Task.Run<LoadMoreItemsResult>(
                    async () =>
                    {
                        IPagedResponse<K> result = await nextPageItems;
                        this.VirtualCount = result.VirtualCount;

                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                foreach (K item in result.Items)
                                    this.Add(item);
                            });

                        return new LoadMoreItemsResult() { Count = (uint)result.Items.Count() };

                    }).AsAsyncOperation<LoadMoreItemsResult>();
            }
            
        }

        #endregion
    }
}
