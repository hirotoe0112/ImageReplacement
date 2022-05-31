using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageReplacement
{
    internal class Image
    {
        public string FilePath { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        internal Image(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("ソース画像ファイルが存在しません。");
            }
            FilePath = filePath;
            Directory = Path.GetDirectoryName(FilePath);
            FileName = Path.GetFileName(FilePath);
            //画像サイズを取得
            uint w, h;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(16, SeekOrigin.Begin);
                byte[] buf = new byte[8];
                fs.Read(buf, 0, 8);

                w = (((uint)buf[0] << 24) | ((uint)buf[1] << 16) | ((uint)buf[2] << 8) | ((uint)buf[3]));
                h = (((uint)buf[4] << 24) | ((uint)buf[5] << 16) | ((uint)buf[6] << 8) | ((uint)buf[7]));
            }
            Width = (int)w;
            Height = (int)h;
        }
    }
}
