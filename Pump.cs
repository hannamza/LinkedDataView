using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    //전체 펌프 정보 중 펌프 연동 출력은 프로그램에서 필요하지 않으므로 가져오지 않음
    class Pump
    {
        private int facpNumber;
        private int pumpNumber;
        private string pumpType;
        private string pumpName;
        private string lcdName;

        // 연동 데이터 부분은 필요없으므로 가져오지 않음

        public Pump(int facpNumber, int pumpNumber, string pumpType, string pumpName, string lcdName)
        {
            this.facpNumber = facpNumber;
            this.pumpNumber = pumpNumber;
            this.pumpType = pumpType;
            this.pumpName = pumpName;   
            this.lcdName = lcdName;
        }

        public int FacpNumber
        {
            get { return facpNumber; }
            set { facpNumber = value; }
        }

        public int PumpNumber
        { 
            get { return pumpNumber; } 
            set { facpNumber = value; } 
        }

        public string PumpType
        { 
            get { return pumpType; }
            set { pumpType = value; } 
        }

        public string PumpName
        {
            get { return pumpName; }
            set { pumpName = value; }
        }

        public string LcdName
        {
            get { return lcdName; }
            set { lcdName = value; }
        }
    }
}
