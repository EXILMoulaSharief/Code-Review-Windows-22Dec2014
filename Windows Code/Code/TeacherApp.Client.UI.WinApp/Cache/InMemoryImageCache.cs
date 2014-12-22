using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TeacherApp.Client.UI.Cache;
using SQLite;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;

namespace TeacherApp.Client.UI.WinApp.Cache
{
    public class InMemoryImageCache : IImageCache<BitmapImage>
    {
        //private readonly NSCache _cache;
        private Dictionary<int, ImageDetailMapping> _cache;
       
        public InMemoryImageCache()
        {
            //_cache = new NSCache();
            _cache = new Dictionary<int, ImageDetailMapping>();
        }

        public BitmapImage SetImageFromByteArray(byte[] data)
        {
            using (InMemoryRandomAccessStream raStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(raStream))
                {
                    // Write the bytes to the stream
                    writer.WriteBytes(data);

                    // Store the bytes to the MemoryStream
                    var i = writer.StoreAsync();

                    // Not necessary, but do it anyway
                    var k= writer.FlushAsync();

                    // Detach from the Memory stream so we don't close it
                    writer.DetachStream();
                }

                raStream.Seek(0);

                BitmapImage bitMapImage = new BitmapImage();
                bitMapImage.SetSource(raStream);
                bitMapImage.DecodePixelHeight = 49;
                bitMapImage.DecodePixelWidth = 49;
                return bitMapImage;
            }
        }

        void IImageCache<BitmapImage>.AddImageDetail(int studentId, int photoId, byte[] imageBytes)
        {
            BitmapImage uiImage = SetImageFromByteArray(imageBytes);
            //UIImage uiImage = ConvertBytesToImage(imageBytes);
            /*
            BitmapImage uiImage = new BitmapImage();
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBytes);
            InMemoryRandomAccessStream imRAS = new InMemoryRandomAccessStream();
            byte[] test = null;
            var x = await imRAS.WriteAsync(test.AsBuffer());
            imRAS.Seek(0);
            uiImage.SetSource(imRAS);
            //uiImage.SetSource
            */
            
            //Windows.UI.Xaml.Controls.Image img = new Windows.UI.Xaml.Controls.Image();
            ImageDetailMapping imageDetailMapping = new ImageDetailMapping();
            imageDetailMapping.Image = uiImage;
            imageDetailMapping.PhotoId = photoId;
            /*
            var imageDetailMapping = new ImageDetailMapping
                                     {
                                         Image = uiImage,
                                         PhotoId = photoId,
                                     };*/

            //_cache.SetObjectforKey(imageDetailMapping, NSNumber.FromInt32(studentId));
            if (!_cache.ContainsKey(studentId))
            {
                _cache.Add(studentId, imageDetailMapping);
            }
        }

        ImageDetail<BitmapImage> IImageCache<BitmapImage>.GetImageDetail(int studentId)
        {
            ImageDetail<BitmapImage> imageDetail = null;


            ImageDetailMapping inMemoryMapping = null;
            _cache.TryGetValue(studentId, out inMemoryMapping);
            //var inMemoryMapping = (object)studentId;// _cache.ObjectForKey(NSNumber.FromInt32(studentId)) as ImageDetailMapping;

            if(inMemoryMapping != null)
            {
                imageDetail = new ImageDetail<BitmapImage> { Image = inMemoryMapping.Image, PersonId = studentId, PhotoId = inMemoryMapping.PhotoId };
            }

            return imageDetail;
        }

        void IImageCache<BitmapImage>.UpdateFromOtherCacheWithStudentIds(IImageCache<BitmapImage> cacheToUpdateFrom, IEnumerable<int> withStudentIds)
        {
            foreach(var withStudentId in withStudentIds)
            {
                ImageDetail<BitmapImage> imageDetail = cacheToUpdateFrom.GetImageDetail(withStudentId);

                if(imageDetail != null)
                {
                    var imageDetailMapping = new ImageDetailMapping
                                             {
                                                 Image = imageDetail.Image,
                                                 PhotoId = imageDetail.PhotoId,
                                             };
                   // _cache.SetObjectforKey(imageDetailMapping, NSNumber.FromInt32(imageDetail.PersonId));
                }
            }
        }

        void IImageCache<BitmapImage>.DeleteImageDetail(int studentId)
        {
            //_cache.RemoveObjectForKey(NSNumber.FromInt32(studentId));
        }

        public void Dispose()
        {
           // _cache.Dispose();
        }

        private UIImage ConvertBytesToImage(byte[] byteArray)
        {
            if(byteArray != null)
            {
                //NSData imageData = NSData.FromArray(byteArray);
                NSData mydata = new NSData();
                mydata = NSData.FromArray(byteArray);
               // System.IO.MemoryStream imageData = new System.IO.MemoryStream(byteArray)
                return UIImage.LoadFromData(mydata);
            }
            return null;
        }
    }
}