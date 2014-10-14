using System.IO;
using System.Web.UI.WebControls;

namespace EC.Common
{
    public class UploadFile
    {
        #region Variables
        private string _filePath = string.Empty;
        private FileUpload _fileUpload;
        private string _status;
        private bool _uploadSucess = false;
        #endregion Variables

        public UploadFile(string filePath, FileUpload fileUpload)
        {
            _filePath = filePath;
            _fileUpload = fileUpload;
        }

        #region Properties
        public string Status
        {
            get { return _status; }
        }

        public bool UploadSucess
        {
            get { return _uploadSucess; }
            set { _uploadSucess = value; }
        }
        public FileUpload FileUpload
        {
            get { return _fileUpload; }
        }

        public string FilePath
        {
            get { return _filePath; }
        }

        public string ContentType
        {
            get { return _fileUpload.PostedFile.ContentType; }
        }

        public int ContentLength
        {
            get { return _fileUpload.PostedFile.ContentLength; }
        }

        public string FileName
        {
            get { return _fileUpload.FileName; }
        }
        
        public string FileExtension
        {
            get { return Path.GetExtension(_fileUpload.FileName);}
        }
                #endregion Properties
        
        public virtual void Upload()
        {

            _filePath += _fileUpload.FileName;

            if (_fileUpload.HasFile)
            {
                try
                {
                    _fileUpload.SaveAs(_filePath);
                    _status = "Received: " + _fileUpload.FileName + " Content Type: " +
                         _fileUpload.PostedFile.ContentType + " Length: " + _fileUpload.PostedFile.ContentLength;
                    _uploadSucess = true;
                }
                catch (System.IO.IOException e)
                {
                    _status = e.Message.ToString();
                }

                catch (System.Exception e)
                {
                    _status = e.Message.ToString();
                }
            }//end if
            else
            {
                _uploadSucess = false;
                _status = "No file was uploaded";
            }
        }
    }

}
