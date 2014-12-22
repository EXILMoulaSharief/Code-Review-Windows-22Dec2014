using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TeacherApp.Client.UI.Cache;

namespace TeacherApp.Client.UI.Winpp.Cache
{
    public class FileSystemImageCache : IImageCache<UIImage>
    {
        private readonly string _schoolIdentifier;
        private string _schoolCacheDirectoryPath;

        public FileSystemImageCache(string schoolIdentifier)
        {
            _schoolIdentifier = schoolIdentifier;

            SetBaseDirectoryForCache();
        }

        void IImageCache<UIImage>.AddImageDetail(int personId, int photoId, byte[] imageBytes)
        {
            if(imageBytes != null)
            {
                NSData imageData = NSData.FromArray(imageBytes);
                AddToFileSystemCache(personId, photoId, imageData);
            }
        }

        void IImageCache<UIImage>.UpdateFromOtherCacheWithStudentIds(IImageCache<UIImage> cacheToUpdateFrom, IEnumerable<int> withStudentIds)
        {
            foreach(var withStudentId in withStudentIds)
            {
                ImageDetail<UIImage> imageDetail = cacheToUpdateFrom.GetImageDetail(withStudentId);
                if(imageDetail != null)
                {
                    AddToFileSystemCache(imageDetail.PersonId, imageDetail.PhotoId, imageDetail.Image.AsJPEG());
                }
            }
        }

        ImageDetail<UIImage> IImageCache<UIImage>.GetImageDetail(int studentId)
        {
            ImageDetail<UIImage> studentDetail = null;

            string studentImageCachePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path; //Directory.GetFiles(_schoolCacheDirectoryPath, string.Format("{0}_*.jpg", studentId)).SingleOrDefault();

            bool studentImageExists = !string.IsNullOrWhiteSpace(studentImageCachePath); //&& File.Exists(studentImageCachePath);
            //Hard coded need to chnage
            if(!studentImageExists)
            {
                UIImage studentImage = UIImage.FromFile(studentImageCachePath);

                int photoId;
                int.TryParse(Path.GetFileNameWithoutExtension(studentImageCachePath).Replace(string.Format("{0}_", studentId), string.Empty), out photoId);

                studentDetail = new ImageDetail<UIImage>
                                {
                                    Image = studentImage,
                                    PersonId = studentId,
                                    PhotoId = photoId,
                                };
            }

            return studentDetail;
        }

        void IImageCache<UIImage>.DeleteImageDetail(int studentId)
        {
            string[] strings = null;// Directory.GetFiles(_schoolCacheDirectoryPath, string.Format("{0}_*.jpg", studentId));

            if(strings != null && strings.Any())
            {
                foreach(var oldFilePath in strings)
                {
                   // File.Delete(oldFilePath);
                }
            }
        }

        public void Dispose()
        {
            // Not Required.
        }

        private void AddToFileSystemCache(int studentId, int photoId, NSData imageData)
        {
            string studentImageCachePath = Path.Combine(_schoolCacheDirectoryPath, string.Format("{0}_{1}.jpg", studentId, photoId));

            string[] strings = null;// Directory.GetFiles(_schoolCacheDirectoryPath, string.Format("{0}_*.jpg", studentId));

            if(strings != null && strings.Any())
            {
                foreach(var oldFilePath in strings.Where(path => !path.Equals(studentImageCachePath)))
                {
                    //File.Delete(oldFilePath);
                }
            }

            NSDictionary directoryWithProtectionLevel = NSDictionary.FromObjectAndKey(NSFileManager.FileProtectionComplete, NSFileManager.FileProtectionKey);

            NSFileManager.DefaultManager.CreateFile(studentImageCachePath, imageData, directoryWithProtectionLevel);
        }

        private void SetBaseDirectoryForCache()
        {

            //var internetCachePath = (object) null;// Windows.Storage.ApplicationData.Current.LocalFolder.Path;// Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);

            var schoolPath = _schoolIdentifier;//Path.Combine(null, _schoolIdentifier);
            var FileExists = Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync(schoolPath);
            string fullpath = string.Empty;
            if(FileExists.Status == Windows.Foundation.AsyncStatus.Error)
            {
               // Directory.CreateDirectory(schoolPath);

                var DocumentsLibrary = Windows.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(schoolPath);
                fullpath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path,_schoolIdentifier);
            }

            _schoolCacheDirectoryPath = fullpath;
        }
    }
}