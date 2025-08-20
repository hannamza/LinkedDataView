using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkedDataView
{
    internal class ColumnHeaderSorting : IComparer
    {
        public int Column { get; set; } = 0;          // 정렬할 컬럼 인덱스
        public SortOrder Order { get; set; } = SortOrder.Ascending;

        public int Compare(object x, object y)
        {
            string strX = ((ListViewItem)x).SubItems[Column].Text;
            string strY = ((ListViewItem)y).SubItems[Column].Text;

            int result = string.Compare(strX, strY);

            if (Order == SortOrder.Descending)
            {
                result *= -1; // 내림차순
            }

            return result;
        }
    }
}
