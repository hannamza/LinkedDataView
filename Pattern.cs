using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    class Pattern
    {
        private int number;
        private string name;
        private List<string> items;

        public Pattern(int number, string strName, List<string> listItems)
        {
            this.number = number;
            this.name = strName;
            items = listItems;
        }

        ~Pattern() 
        {
            if ((items != null) && (items.Count > 0))
            {
                items.Clear();
            }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public string Name 
        { 
            get { return name; }
            set { name = value; }
        }

        public List<string> Items
        {
            get { return items; }
            set { items = value; }
        }

        public void AddItem(string item)
        {
            items.Add(item);
        }
    }
}
