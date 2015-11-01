using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IPagedSource<K> Source { get; set; }

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
            var nextPageItems = this.Source.GetPage(++this.CurrentPage, 25);

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
