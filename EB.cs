using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    class EB
    {
        private int number;
        private string remarks;
        private string commContent;

        public EB(int number, string remarks, string commContent) 
        {
            this.number = number;
            this.remarks = remarks;
            this.commContent = (commContent ?? string.Empty).Replace('_', '-');
        }

        public int Number
        {
            get { return number; }
            set { this.number = value; }
        }

        public string Remarks
        { 
            get { return remarks; } 
            set { this.remarks = value; } 
        
        }

        public string CommContent
        {
            get { return commContent; }
            set 
            {
                commContent = (value ?? string.Empty).Replace('_', '-');
            }
        }                                  
    }
}
