using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using WinPure.ContactManagement.Common.Interfaces.Virtualization;

namespace WinPure.ContactManagement.Common.Helpers.Virtualization
{
    public class DataProvider<T>:IItemsProvider<T>
    {
        private readonly int _count;
        private readonly int _fetchDelay;
        private readonly ICollection<T> _collection;

        public DataProvider(ICollection<T> collection, int count, int fetchDelay)
        {
            _count = count;
            _fetchDelay = fetchDelay;
            _collection = collection;
        }

        /// <summary>
        /// Fetches the total number of items available.
        /// </summary>
        /// <returns></returns>
        public int FetchCount()
        {
            return _count; 
        }

        /// <summary>
        /// Fetches a range of items.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The number of items to fetch.</param>
        /// <returns></returns>
        public IList<T> FetchRange(int startIndex, int count)
        {
            Trace.WriteLine("FetchRange: " + startIndex + "," + count);
            return _collection.Skip(startIndex).Take(count).ToList();
        }
    }
}
