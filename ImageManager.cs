using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkedDataView
{
    internal class ImageManager
    {
        // 트리에 들어갈 이미지 폴더
        public readonly string IMAGES_FOLDER = "Images";

        // 출력내용 아이콘 이미지 폴더
        public readonly string OUTPUT_CONTENT_IMAGES_FOLDER = "OutputContentImages";

        // 출력내용 기본 이미지 이름
        public readonly string DEFAULT_IMAGE_NAME = "default";

        // 출력 내용 이미지 리스트
        List<string> outputContentList = new List<string>();

        public enum ENUM_TREE_IMAGES
        {
            FACP,
            UNIT,
            LOOP,
            CIRCUIT,
            INPUT_TYPE,
            OUTPUT_TYPE,
            EQUIPMENT_NAME,
            OUTPUT_CONTENT,
            PATTERN
        }

        // 트리에서 고정으로 사용될 이미지
        public readonly string[] TREE_IMAGES_NAME = 
        {  
            "f.png",
            "u.png",
            "l.png",
            "c.png",
            "i.png",
            "o.png",
            "e.png",
            "s.png",
            "p.png"
        };

        public ImageManager() 
        {
            GetOutputContentImagesList();
        }

        private static ImageManager instance;

        public static ImageManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ImageManager();
                }

                return instance;
            }
        }

        public List<string> GetOutputContentList()
        {
            return outputContentList;
        }

        // 출력 내용 이미지 리스트 얻기
        private void GetOutputContentImagesList()
        {
            // 실행 중인 exe의 경로
            string exePath = AppDomain.CurrentDomain.BaseDirectory;

            // 이미지 폴더 경로
            string imageFolderPath = Path.Combine(exePath, OUTPUT_CONTENT_IMAGES_FOLDER);

            if (Directory.Exists(imageFolderPath))
            {
                // .png 파일만 필터링
                string[] pngFiles = Directory.GetFiles(imageFolderPath, "*.png");

                Console.WriteLine("PNG 파일 리스트:");
                foreach (string file in pngFiles)
                {
                    // 확장자 제외한 파일명만 추출
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                    outputContentList.Add(fileNameWithoutExtension);
                }
            }
            else
            {
                Console.WriteLine("{0} 폴더가 존재하지 않습니다.", OUTPUT_CONTENT_IMAGES_FOLDER);
            }
        }

        public string GetOutputContentImagePath(string outputContent)
        {
            string strRet = OUTPUT_CONTENT_IMAGES_FOLDER + "\\" + outputContent + ".png";
            return strRet;
        }

        public string GetImageKey(string outputContent)
        {
            string strRet = string.Empty;

            string cleaned = Regex.Replace(outputContent, @"[^a-zA-Z0-9가-힣]", "");
            bool bExist = outputContentList.Contains(cleaned);
            if (bExist)
            {
                strRet = cleaned;
            }
            return strRet;
        }
    }
}
