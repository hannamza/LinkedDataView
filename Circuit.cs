using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    class Circuit
    {
        private int facpNumber;
        private int unitNumber;
        private int loopNumber;
        private int circuitNumber;
        private string inputType;
        private string outputType;
        private List<string> contactControls;
        private List<string> repeaterControls;
        private string inputFullName;
        private string equipmentName;
        private string outputContent;
        private string outputFullName;

        public Circuit(int nFacpNumber, int nUnitNumber, int nLoopNumber, int nCircuitNumber, string strInputType, string strOutputType, List<string> listContactControls, List<string> listRepeaterControls, string strInputFullName, string strEquipmentName, string strOutputContent, string strOutputFullName)
        {
            facpNumber = nFacpNumber;
            unitNumber = nUnitNumber;
            loopNumber = nLoopNumber;
            circuitNumber = nCircuitNumber;
            inputType = strInputType;
            outputType = strOutputType;
            contactControls = listContactControls;
            repeaterControls = listRepeaterControls;
            inputFullName = strInputFullName;
            equipmentName = strEquipmentName;
            outputContent = strOutputContent;
            outputFullName = strOutputFullName;
        }

        ~Circuit()
        {
            if((contactControls != null) && (contactControls.Count > 0))
            {
                contactControls.Clear();
            }

            if ((repeaterControls != null) && (repeaterControls.Count > 0))
            {
                repeaterControls.Clear();
            }
        }

        public int FacpNumber
        {
            get { return facpNumber; }
            set { facpNumber = value; }
        }

        public int UnitNumber
        {
            get { return unitNumber; }
            set { unitNumber = value; }
        }

        public int LoopNumber
        {
            get { return loopNumber; }
            set { loopNumber = value; }
        }

        public int CircuitNumber
        {
            get { return circuitNumber; }
            set { circuitNumber = value; }
        }

        public string InputType
        {
            get { return inputType; }
            set { inputType = value; }
        }

        public string OutputType
        {
            get { return outputType; }
            set { outputType = value; }
        }

        public List<string> ContactControls
        {
            get { return contactControls; }
            set { contactControls = value; }
        }

        public List<string> RepeaterControls
        {
            get { return repeaterControls; }
            set { repeaterControls = value; }
        }

        public string InputFullName
        { 
            get { return inputFullName; } 
            set { inputFullName = value; } 
        }

        public string EquipmentName
        {
            get { return equipmentName; }
            set { equipmentName = value; }
        }

        public string OutputContent
        { 
            get { return outputContent; }
            set { outputContent = value; }
        }

        public string OutputFullName
        {
            get { return outputFullName; }
            set { outputFullName = value; }
        }

        public void AddContactControl(string contactControl)
        {
            contactControls.Add(contactControl);
        }

        public void ClearContactControls()
        {
            if (contactControls != null)
            {
                contactControls.Clear();
            }
        }

        public void AddrepeaterControls(string repeaterControl)
        {
            repeaterControls.Add(repeaterControl);
        }

        public void ClearRepeaterControls()
        {
            if (repeaterControls != null)
            {
                repeaterControls.Clear();
            }
        }
    }
}
