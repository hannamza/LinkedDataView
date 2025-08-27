using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace LinkedDataView
{
    public class ExcelManager
    {
        public const int MAX_PATTERN_COUNT = 511;
        public const int MAX_PATTERN_ITEM_COUNT = 250;
        public readonly string[] SHEET_TYPES_TEXT = { "UNIT", "Pattern", "EB", "Pump", "PS", "Contact" };
        public const int SEPERATING_NUM_FROM_UNIT_NUM = 1000;

        public enum ENUM_SHEET_TYPES
        {
            UNKNOWN_TYPE = -1,
            UNIT,
            PATTERN,
            EB,
            PUMP,
            PS,
            CONTACT
        }

        // C++ 엑셀 라이브러리와는 달리 0베이스
        enum UNIT_COLS 
        {
            LOOP_NUM = 1,
            CIRCUIT_NUM,
            INPUT_TYPE,
            OUTPUT_TYPE,
            CONTROL_TYPE,
            OUTPUT1 = 6,
            OUTPUT20 = 25,
            INPUT_FULL_NAME,
            EQUIPMENT_NAME,     // 기존에는 없어서 SLP4에서 만들어지는 연동표에 추가해서 출력되도록 함
            OUTPUT_CONTENT      // 기존에는 없어서 SLP4에서 만들어지는 연동표에 추가해서 출력되도록 함
        };

        enum UNIT_ROWS
        {
            ROW_START = 2
        }

        enum PATTERN_COLS
        {
            NUM = 1,
            NAME,
            CONTROL,
            ITEM1   // MAX_PATTERN_ITEM_COUNT 개수까지 있을 수 있음
        }

        enum PATTERN_ROWS
        {
            ROW_START = 2
        }

        enum EB_COLS
        {
            NUM,
            REMARKS,
            COMM_CONTENT
        }

        enum EB_ROWS
        {
            ROW_START = 2
        }

        enum PUMP_COLS
        {
            FACP_NUM,
            PUMP_NUM,
            PUMP_TYPE,
            PUMP_NAME,
            LCD_NAME,
            CONTROL,
            ITEM1,
        }

        enum PUMP_ROWS
        {
            ROW_START = 1
        }

        enum PS_COLS
        {
            FACP_NUM,
            PS_NUM,
            PS_TYPE,
            NAME,
            LCD,
            CONTROL,
            ITEM1,
        }

        enum PS_ROWS
        {
            ROW_START = 1
        }

        enum CONTACT_COLS
        {
            FACP_NUM,
            CONTACT_NUM,
            NAME,
        }

        enum CONTACT_ROWS
        {
            ROW_START = 1
        }


        private static ExcelManager instance;

        private ExcelManager() { }

        public static ExcelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExcelManager();
                }

                return instance;
            }
        }

        // Sheet Name으로 Unit Sheet와 그 외의 Sheet로 분류해서 처리하도록 분류
        private int CheckSheetType(string sheetName)
        {
            int nUnit = 0;

            // 유닛을 제외한 Sheet에 해당하는 지를 찾고 일치하면 유닛 숫자와 분리를 위해 고정 값 (SEPERATING_NUM_FROM_UNIT_NUM)을 곱해서 리턴
            for (int i = (int)ENUM_SHEET_TYPES.PATTERN; i <= (int)ENUM_SHEET_TYPES.CONTACT; i++)
            {
                string strType = SHEET_TYPES_TEXT[i];
                if (sheetName.Equals(strType))
                {
                    return i * SEPERATING_NUM_FROM_UNIT_NUM;
                }
            }

            // 유닛 번호를 가져오고 숫자로 가져오지 않으면 최종 Unknown 처리
            if (int.TryParse(sheetName, out nUnit))
            {
                if (nUnit >= 0 && nUnit < 63)
                {
                    return nUnit;
                }
                else
                {
                    return (int)ENUM_SHEET_TYPES.UNKNOWN_TYPE;
                }
            }
            else
            {
                return (int)ENUM_SHEET_TYPES.UNKNOWN_TYPE;
            }
        }

        // Pattern Sheet Parsing
        private void ParsingPatternSheet(DataTable table)
        {
            int patternNumber = -1;
            string patternName = string.Empty;
            List<string> patternItems = null;

            for(int row = (int)PATTERN_ROWS.ROW_START; row < table.Rows.Count; row++)
            {
                var rowData = table.Rows[row];

                // 짝수 줄은 변수 초기화
                if (row % 2 == 0)
                {
                    patternNumber = -1;
                    patternName = string.Empty;
                    patternItems = new List<string>();
                }

                // 라이브러리에서 감지한 Column 최대 개수가 이하로만 루프를 돔
                for (int col = (int)PATTERN_COLS.NUM; col < table.Columns.Count; col++)
                {
                    var cellValue = rowData[col];
                
                    if (row % 2 == 0)
                    {
                        // 짝수 줄은 패턴 번호와 패턴 이름이 있고 이외는 없음
                        if (col == (int)PATTERN_COLS.NUM)
                        {
                            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                            {
                                string onlyPatternNumber = cellValue.ToString().Substring(1);
                                patternNumber = int.Parse(onlyPatternNumber);
                            }
                        }
                        else if (col == (int)PATTERN_COLS.NAME)
                        {
                            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                            {
                                patternName = cellValue.ToString();
                            }
                        }
                        else
                        {
                            break;
                        }                        
                    }
                    else
                    {
                        // 순차적으로 진행되기 때문에 값이 없으면 break;
                        if(col >= (int)PATTERN_COLS.ITEM1)
                        {
                            string circuitNumber = string.Empty;
                            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                            {
                                circuitNumber = cellValue.ToString();
                                patternItems.Add(circuitNumber);
                            }
                            else
                            {
                                break;
                            }                          
                        }
                    }
                }

                // 두 줄을 처리하고 마지막에 Pattern 정보를 넣음
                if (row % 2 == 1)
                {
                    Pattern pattern = new Pattern(patternNumber, patternName, patternItems);
                    DataManager dataManager = DataManager.Instance;
                    dataManager.AddToPatternDic(patternNumber, pattern);
                }
            }
        }

        // EB Sheet Parsing
        private void ParsingEBSheet(DataTable table)
        {
            DataManager dataManager = DataManager.Instance;
            for (int row = (int)EB_ROWS.ROW_START; row < table.Rows.Count; row++)
            {
                var rowData = table.Rows[row];

                int number = -1;
                string remarks = string.Empty;
                string commContent = string.Empty;

                // 번호
                number = int.Parse(rowData[(int)EB_COLS.NUM].ToString());

                // 비고
                remarks = rowData[(int)EB_COLS.REMARKS].ToString();

                // 통신내용
                commContent = rowData[(int)EB_COLS.COMM_CONTENT].ToString();

                EB eb = new EB(number, remarks, commContent);
                dataManager.AddEB(number, eb);
            }
        }

        private void ParsingPumpSheet(DataTable table)
        {
            DataManager dataManager = DataManager.Instance;
            for(int row = (int)PUMP_ROWS.ROW_START; row < table.Rows.Count; row += 2)
            {
                var rowData = table.Rows[row];

                int facpNumber = -1;
                int pumpNumber = -1;
                string pumpType = string.Empty;
                string pumpName = string.Empty;
                string lcdName = string.Empty;

                // 수신기 번호
                facpNumber = int.Parse(rowData[(int)PUMP_COLS.FACP_NUM].ToString());

                // 펌프 번호
                pumpNumber = int.Parse(rowData[(int)PUMP_COLS.PUMP_NUM].ToString());

                // 펌프 타입
                pumpType = rowData[(int)PUMP_COLS.PUMP_TYPE].ToString();

                // 펌프 이름
                pumpName = rowData[(int)PUMP_COLS.PUMP_NAME].ToString();

                // LCD 이름
                lcdName = rowData[(int)PUMP_COLS.LCD_NAME].ToString();

                // 펌프 출력 정보는 가져오지 않음

                Pump pump = new Pump(facpNumber, pumpNumber, pumpType, pumpName, lcdName);
                dataManager.AddPump(pumpNumber, pump);
            }
        }

        private void ParsingContactSheet(DataTable table)
        {
            DataManager dataManager = DataManager.Instance;
            for (int row = (int)CONTACT_ROWS.ROW_START; row < table.Rows.Count; row ++)
            {
                var rowData = table.Rows[row];

                int facpNumber = -1;
                int contactNumber = -1;
                string name = string.Empty;

                // 수신기 번호
                facpNumber = int.Parse(rowData[(int)CONTACT_COLS.FACP_NUM].ToString());

                // 접점 번호
                contactNumber = int.Parse(rowData[(int)CONTACT_COLS.CONTACT_NUM].ToString());

                // 이름
                name = rowData[(int)CONTACT_COLS.NAME].ToString();

                Contact contact = new Contact(facpNumber, contactNumber, name);
                dataManager.AppContact(contactNumber, contact);
            }
        }

        // Unit Sheet Parsing
        private void ParsingUnitSheet(DataTable table, int facpNumber, int unitNumber)
        {
            int nLoopNum = -1;
            int nCircuitNum = -1;
            string strInputType = string.Empty;
            string strOutputType = string.Empty;
            string strInputFullName = string.Empty;
            string strEquipmentName = string.Empty;
            string strOutputContent = string.Empty;
            string strContactControl = string.Empty;
            string strRepeaterControl = string.Empty;
            List<string> contactControls = null;
            List<string> repeaterControls = null;

            int nMaxRow = table.Rows.Count;
            for (int row = (int)UNIT_ROWS.ROW_START; row < nMaxRow; row++)
            {
                if (row % 2 == 0)
                {
                    nLoopNum = -1;
                    nCircuitNum = -1;
                    strInputType = string.Empty;
                    strOutputType = string.Empty;
                    strInputFullName = string.Empty;
                    strEquipmentName = string.Empty;
                    strOutputContent = string.Empty;
                    strContactControl = string.Empty;
                    strRepeaterControl = string.Empty;
                    contactControls = new List<string>();
                    repeaterControls = new List<string>();
                }
                
                // 실제 값이 들어간 col의 최대값으로 넣음, SLP4에 설비명과 출력내용이 들어갈 예정이지만, 기존 엑셀 파일을 로드하더라도 이렇게 처리해야 프로그램이 죽지 않음
                int nMaxCol = table.Columns.Count;
                for (int col = (int)UNIT_COLS.LOOP_NUM; col < nMaxCol; col++)
                {
                    var rowData = table.Rows[row];

                    if(row % 2 == 0)
                    {
                        switch (col)
                        {
                            case (int)UNIT_COLS.LOOP_NUM:
                                if (double.TryParse(rowData[col]?.ToString(), out double loopNum))
                                {
                                    nLoopNum = (int)loopNum; 
                                }
                                break;
                            case (int)UNIT_COLS.CIRCUIT_NUM:
                                if (double.TryParse(rowData[col]?.ToString(), out double circuitNum))
                                {
                                    nCircuitNum = (int)circuitNum;
                                }
                                break;
                            case (int)UNIT_COLS.INPUT_TYPE:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    strInputType = (string)rowData[col];
                                }
                                else
                                {
                                    strInputType = string.Empty;
                                }
                                break;
                            case (int)UNIT_COLS.OUTPUT_TYPE:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    strOutputType = (string)rowData[col];
                                }
                                else
                                { 
                                    strOutputType = string.Empty; 
                                }  
                                break;
                            case (int)UNIT_COLS.CONTROL_TYPE:
                                break;
                            case (int)UNIT_COLS.INPUT_FULL_NAME:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    strInputFullName = (string)rowData[col];
                                }
                                else
                                {
                                    strInputFullName = string.Empty;
                                }
                                break;
                            case (int)UNIT_COLS.EQUIPMENT_NAME:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    strEquipmentName = (string)rowData[col];
                                }
                                else
                                {
                                    strEquipmentName = string.Empty;    
                                }                                    
                                break;
                            case (int)UNIT_COLS.OUTPUT_CONTENT:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    strOutputContent = (string)rowData[col];
                                }
                                else 
                                { 
                                    strOutputContent = string.Empty; 
                                }   
                                break;
                            default:
                                if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                                {
                                    //나머지는 접점제어로 순차적으로 위에서 col 루프에 따라 순차적으로 넣어진다.
                                    strContactControl = (string)rowData[col];
                                    contactControls.Add(strContactControl);
                                }
                                break;
                        }
                    }
                    else
                    {
                        //중계기 제어는 col 루프에 따라 순차적으로 넣어진다.
                        if((col >= (int)UNIT_COLS.OUTPUT1) && (col <= (int)UNIT_COLS.OUTPUT20))
                        {
                            if (rowData[col] != null && !string.IsNullOrWhiteSpace(rowData[col].ToString()))
                            {
                                strRepeaterControl = (string)rowData[col];
                                repeaterControls.Add(strRepeaterControl);
                            }      
                        }
                    }
                }

                if (row % 2 == 0)
                {
                    //출력 FULL NAME 만들기
                    string strOutputFullName = string.Empty;

                    if(strOutputContent != string.Empty)
                    {
                        strOutputFullName = strInputFullName.Replace(strInputType, strOutputType);
                    }
                    else
                    {
                        strOutputFullName = strInputFullName.Replace(strInputType, "");
                    }

                    if (strEquipmentName != string.Empty)
                    {
                        strOutputFullName = strOutputFullName.Replace(strEquipmentName, strOutputContent);
                    }
                    else // 설비 명이 없으면 설비 번호도 없으므로 출력 내용을 단순히 가져다 붙인다.
                    {
                        strOutputFullName += " " + strOutputContent;
                    }

                    //회로 정보 생성
                    Circuit circuit = new Circuit(facpNumber, unitNumber, nLoopNum, nCircuitNum, strInputType, strOutputType, contactControls, repeaterControls, strInputFullName, strEquipmentName, strOutputContent, strOutputFullName);

                    DataManager dataManager = DataManager.Instance;

                    //Dictionary에 추가

                    //회로번호
                    string strFullCircuitNum = string.Format("{0:D2}{1:D2}-{2:D1}{3:D3}", facpNumber, unitNumber, nLoopNum, nCircuitNum);
                    dataManager.AddToCircuitDic(strFullCircuitNum, circuit);

                    //입력타입
                    dataManager.AddToInputTypeDic(strInputType, circuit);
                    
                    //출력타입
                    dataManager.AddToOutputTypeDic(strOutputType, circuit);

                    //설비명
                    dataManager.AddToEquipmentNameDic(strEquipmentName, circuit);

                    //출력내용
                    dataManager.AddToOutputContentDic(strOutputContent, circuit);   
                }
            }
        }

        public void GetLinkedData(string[] filePathList, Dictionary<string, string> facpNumDic)
        {
            // 유닛 정보 외의 파싱은 한 번만 실행
            bool bPatternParsing = false;
            bool bEBParsing = false;
            bool bPumpParsing = false;
            bool bPSParsing = false;
            bool bContactParsing = false;

            foreach (string filePath in filePathList)
            {
                // 수신기 번호 얻기 : 파일 명에 있음
                string strFacpNum = facpNumDic[filePath];
                int nFacpNum = int.Parse(strFacpNum);

                using (var Stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(Stream))
                    {
                        // 전체 workbook
                        var result = reader.AsDataSet();

                        // Sheet 개수
                        int nTable = reader.ResultsCount;

                        for (int nSheet = 0; nSheet < nTable; nSheet++)
                        {
                            // Sheet 가져오기
                            var table = result.Tables[nSheet];

                            // Sheet 이름
                            string sheetName = table.TableName;

                            // 어떤 Sheet인지 판단
                            int nSheetType = CheckSheetType(sheetName);

                            if (nSheetType > (int)ENUM_SHEET_TYPES.UNKNOWN_TYPE)
                            {
                                if (nSheetType >= SEPERATING_NUM_FROM_UNIT_NUM)
                                {
                                    nSheetType = nSheetType / SEPERATING_NUM_FROM_UNIT_NUM;

                                    switch (nSheetType)
                                    {
                                        case (int)ENUM_SHEET_TYPES.PATTERN:
                                            if (!bPatternParsing)
                                            {
                                                ParsingPatternSheet(table);
                                                bPatternParsing = true;
                                            }
                                            break;
                                        case (int)ENUM_SHEET_TYPES.EB:
                                            if (!bEBParsing)
                                            {
                                                ParsingEBSheet(table);
                                                bEBParsing = true;
                                            }
                                            break;
                                        case (int)ENUM_SHEET_TYPES.PUMP:
                                            if (!bPumpParsing)
                                            {
                                                ParsingPumpSheet(table);
                                                bPumpParsing = true;
                                            }
                                            break;
                                        case (int)ENUM_SHEET_TYPES.PS:
                                            if (!bPSParsing)
                                            {
                                                // 압력스위치 정보는 가져오지 않음
                                                bPSParsing = true;
                                            }
                                            break;
                                        case (int)ENUM_SHEET_TYPES.CONTACT:
                                            if (!bContactParsing)
                                            {
                                                ParsingContactSheet(table);
                                                bContactParsing = true;
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    ParsingUnitSheet(table, nFacpNum, nSheetType);
                                }
                            }
                        }
                    }
                }           
            }
        }
    }
}
