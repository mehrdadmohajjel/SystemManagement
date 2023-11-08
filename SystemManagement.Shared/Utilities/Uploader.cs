using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SystemManagement.Shared.Utilities

{
    public static class Uploader
    {
        public static void DeleteFiles(string uploadPath, string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                var path = Path.Combine(uploadPath, fileName);
                if (!File.Exists(path))
                {
                    continue;
                }

                File.Delete(path);
            }
        }

        /// <summary>  
        /// original name + ".part_N.X" (N = file part number, X = total files)  
        /// Objective = enumerate files in folder, look for all matching parts of  
        /// split file. If found, merge and return true.  
        /// </summary>  
        /// <param name="FileName"></param>  
        /// <returns></returns>  
        public static bool MergeFile(string FileName)
        {
            var result = false;
            // parse out the different tokens from the filename according to the convention  
            var partToken = ".part_";
            var baseFileName = FileName.Substring(0, FileName.IndexOf(partToken));
            var trailingTokens = FileName.Substring(FileName.IndexOf(partToken) + partToken.Length);
            int.TryParse(trailingTokens.Substring(0, trailingTokens.IndexOf(".")), out var fileIndex);
            int.TryParse(trailingTokens.Substring(trailingTokens.IndexOf(".") + 1), out var fileCount);
            // get a list of all file parts in the temp folder  
            var Searchpattern = Path.GetFileName(baseFileName) + partToken + "*";
            var FilesList = Directory.GetFiles(Path.GetDirectoryName(FileName), Searchpattern);
            //  merge .. improvement would be to confirm individual parts are there / correctly in  
            // sequence, a security check would also be important  
            // only proceed if we have received all the file chunks  
            if (FilesList.Count() == fileCount)
            {
                // use a singleton to stop overlapping processes  
                if (!MergeFileManager.Instance.IsInUse(baseFileName))
                {
                    MergeFileManager.Instance.AddFile(baseFileName);
                    if (System.IO.File.Exists(baseFileName))
                    {
                        System.IO.File.Delete(baseFileName);
                    }
                    // add each file located to a list so we can get them into  
                    // the correct order for rebuilding the file  
                    var MergeList = new List<SortedFile>();
                    foreach (var File in FilesList)
                    {
                        var sFile = new SortedFile
                        {
                            FileName = File
                        };
                        baseFileName = File.Substring(0, File.IndexOf(partToken));
                        trailingTokens = File.Substring(File.IndexOf(partToken) + partToken.Length);
                        int.TryParse(trailingTokens.
                           Substring(0, trailingTokens.IndexOf(".")), out fileIndex);
                        sFile.FileOrder = fileIndex;
                        MergeList.Add(sFile);
                    }
                    // sort by the file-part number to ensure we merge back in the correct order  
                    var MergeOrder = MergeList.OrderBy(s => s.FileOrder).ToList();
                    using (var FS = new FileStream(baseFileName, FileMode.Create))
                    {
                        // merge each file chunk back into one contiguous file stream  
                        foreach (var chunk in MergeOrder)
                        {
                            try
                            {
                                using (var fileChunk =
                                   new FileStream(chunk.FileName, FileMode.Open))
                                {
                                    fileChunk.CopyTo(FS);
                                }
                            }
                            catch (IOException)
                            {
                                // handle  
                            }
                        }
                    }
                    result = true;
                    // unlock the file from singleton  
                    MergeFileManager.Instance.RemoveFile(baseFileName);

                    // delete temp files
                    foreach (var file in FilesList)
                    {
                        System.IO.File.Delete(file);
                    }
                }
            }
            return result;
        }
    }

    public struct SortedFile
    {
        public int FileOrder { get; set; }
        public string FileName { get; set; }
    }

    public class MergeFileManager
    {
        private static MergeFileManager instance;
        private readonly List<string> MergeFileList;

        private MergeFileManager()
        {
            try
            {
                MergeFileList = new List<string>();
            }
            catch { }
        }

        public static MergeFileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MergeFileManager();
                }
                return instance;
            }
        }

        public void AddFile(string BaseFileName) => MergeFileList.Add(BaseFileName);

        public bool IsInUse(string BaseFileName) => MergeFileList.Contains(BaseFileName);

        public bool RemoveFile(string BaseFileName) => MergeFileList.Remove(BaseFileName);
    }
}