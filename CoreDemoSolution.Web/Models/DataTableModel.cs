namespace CoreDemoSolution.Web.Models
{
    public class DataTableModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        public string SEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string SSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int IDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int IDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int IColumns { get; set; }

        /// <summary>
        /// Sort column index
        /// </summary>
        public int ISortCol_0 { get; set; }

        /// <summary>
        /// Sort Column Direction
        /// </summary>
        public string SSortDir_0 { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int ISortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string SColumns { get; set; }

    }
}
