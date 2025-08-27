using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    class Contact
    {
        private int facpNumber;
        private int contactNumber;
        private string name;

        public Contact(int facpNumber, int contactNumber, string name)
        {
            this.facpNumber = facpNumber;
            this.contactNumber = contactNumber;
            this.name = name;
        }

        public int FacpNumber
        {
            get { return facpNumber; }
            set { facpNumber = value; }
        }

        public int ContactNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
