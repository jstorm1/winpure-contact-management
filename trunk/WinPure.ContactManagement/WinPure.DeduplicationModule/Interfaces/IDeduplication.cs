using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WinPure.DeduplicationModule.Interfaces
{
    public delegate void ProgressNotifyDelegate(float progress);

    interface IDeduplication
    {
#region Properties

        /// <summary>
        /// Number of comparisons to be performed
        /// </summary>
        long NumberOfComparisons
        {
            get;
            set;
        }

#endregion
        
#region Events

        /// <summary>
        /// Event that notifies about operation progress 
        /// </summary>
        event ProgressNotifyDelegate ProgressNotifyEvent;

#endregion
        
#region Methods

        DataTable SearchMatches(DataTable sourceTable,
                                DataTable columnMapping,
                                bool      isExactMatch,
                                int       threshold,
                                bool      isIgnoreEmptyCells);

        DataTable SearchMatches(DataTable sourceTable,
                                DataTable sourceTable2,
                                DataTable columnMapping,
                                bool      isExactMatch,
                                int       threshold,
                                bool      isIgnoreEmptyCells);

        long ComputeNumberOfComparisons(DataTable sourceTable);

        void NotifyAboutProgress(float progress);

#endregion
    }
}
