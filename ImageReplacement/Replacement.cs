using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageReplacement
{
    internal class Replacement
    {
        private string _srcPath;
        private string _distPath;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal Replacement(string srcPath, string distPath)
        {
            if (!Directory.Exists(srcPath) || !Directory.Exists(distPath))
            {
                throw new Exception("コピー元フォルダまたはコピー先フォルダが存在しません。");
            }
            _srcPath = srcPath;
            _distPath = distPath;
        }

        /// <summary>
        /// 入れ替え処理
        /// </summary>
        internal void Replace()
        {
            //ソース画像ファイル一覧取得
            var srcImages = Directory.GetFiles(_srcPath, "*.png").ToList();
            //追加の情報を取得してリストにする
            var srcInformations = new List<Image>();
            srcImages.ForEach(m =>
            {
                srcInformations.Add(new Image(m));
            });
            //入れ替え先画像ファイル一覧取得
            var distImages = Directory.GetFiles(_distPath, "*.png").ToList();
            //各画像に対して同じサイズの画像をソース画像ファイルから探し出し、上書きする
            distImages.ForEach(m =>
            {
                var distImage = new Image(m);
                //コピー元ファイルの検索
                var srcImage = srcInformations.Where(n => n.Width == distImage.Width && n.Height == distImage.Height).FirstOrDefault();
                if(srcImage != null)
                {
                    try
                    {
                        File.Copy(srcImage.FilePath, distImage.FilePath, true);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"コピー失敗[{ex.Message}]file={distImage.FileName}");
                    }
                }
            });
        }
    }
}
