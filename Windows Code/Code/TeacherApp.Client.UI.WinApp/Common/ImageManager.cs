using TeacherApp.Client.Shared.Domain.Managers;
using TeacherApp.Client.Shared.Domain.Services;
using TeacherApp.Client.UI.WinApp.Configuration;
using TeacherApp.Client.Domain.Configuration;
using TeacherApp.Client.Domain;
using System.Collections.Generic;
using TeacherApp.Client.UI.Cache;
using MonoTouch.UIKit;
using TeacherApp.Client.UI.Cache.Implementation;
using TeacherApp.Client.UI.WinApp.Cache;
using TeacherApp.Client.UI.Winpp.Cache;
using System.Linq;
using TeacherApp.Server.School.Contracts;
using System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using TeacherApp.Client.UI.WinApp.ViewModels;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace TeacherApp.Client.UI.WinApp.Common
{
    internal class ImageManager
    {

        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IDomainFactory _domainFactory;
        private readonly Dictionary<string, ICacheExpiryManager> _expiryManagers;
        private readonly Dictionary<string, IImageCache<BitmapImage>> _fileCaches;
        private readonly Dictionary<string, IImageCache<BitmapImage>> _memoryCaches;
        private readonly ISecureStorageManager _secureStorageManager;
        private  GroupBrowserCollectionViewModel _groupBrowserCollectionViewModel;
        private readonly IUIFactory _uiFactory;


        public ImageManager(IDomainFactory domainFactory,
                                 AppSettingsProvider appSettingsProvider)
        {           
            _domainFactory = domainFactory;
            _appSettingsProvider = appSettingsProvider;


            _fileCaches = new Dictionary<string, IImageCache<BitmapImage>>();
            _memoryCaches = new Dictionary<string, IImageCache<BitmapImage>>();
            _expiryManagers = new Dictionary<string, ICacheExpiryManager>();
        }


        public IImageProvider<BitmapImage> GetStudentImages()
        {

            IImageProvider<BitmapImage> studentImageProvider = GetStudentImageProvider();
           // IImageProvider<UIImage> staffImageProvider = GetStaffImageProvider();
            return studentImageProvider;
           
        }

        private IImageCache<BitmapImage> GetFileSystemImageCache(string schoolIdentifier)
        {

            if(!_fileCaches.ContainsKey(schoolIdentifier))
            {
                //_fileCaches.Add(schoolIdentifier, new FileSystemImageCache(schoolIdentifier));
                _fileCaches.Add(schoolIdentifier, null);
            }

            return _fileCaches[schoolIdentifier];
        }

        private IImageCache<BitmapImage> GetMemoryCache(string schoolIdentifier)
        {
            if(!_memoryCaches.ContainsKey(schoolIdentifier))
            {
                _memoryCaches.Add(schoolIdentifier, new InMemoryImageCache());
            }

            return _memoryCaches[schoolIdentifier];
        }

        private IImageProvider<BitmapImage> GetStudentImageProvider()
        {
            string schoolIdentifier = _domainFactory.GetConfigurationSettingProvider()["schoolIdentifier"];
            IImageService imageService = _domainFactory.GetImageService();

            return new StudentImageProvider<BitmapImage>(GetMemoryCache(schoolIdentifier), GetFileSystemImageCache(schoolIdentifier), imageService, GetCacheExpiryManager(schoolIdentifier));
        }

        private ICacheExpiryManager GetCacheExpiryManager(string schoolIdentifier)
        {
            if(!_expiryManagers.ContainsKey(schoolIdentifier))
            {
                _expiryManagers.Add(schoolIdentifier, new CacheExpiryManager(schoolIdentifier));
            }
            return _expiryManagers[schoolIdentifier];
        }

        private IImageProvider<BitmapImage> GetStaffImageProvider()
        {
            string schoolIdentifier = _domainFactory.GetConfigurationSettingProvider()["schoolIdentifier"];
            IImageService imageService = _domainFactory.GetStaffImageService();

            return new StaffImageProvider<BitmapImage>(GetMemoryCache(schoolIdentifier), GetFileSystemImageCache(schoolIdentifier), imageService, GetCacheExpiryManager(schoolIdentifier));
        }
    }
   
    public class AllStudentImageProvider<T> : AllImageProvider<T> where T : class
    {
        public AllStudentImageProvider(IImageCache<T> fileSystemCache, IImageService imageService, ICacheExpiryManager cacheExpiryManager) :
            base(fileSystemCache, imageService, cacheExpiryManager)
        {
        }

        public override IEnumerable<ImageDetail<T>> GetImages(IEnumerable<int> personIds)
        {
            List<ImageRequestDTO> imageRequests;
            var imageDetails = GetImageDetails(personIds, out imageRequests);

            if (imageRequests.Any())
            {
                StartDownloadingStudentImages(imageRequests);
            }

            return imageDetails;
        }

        private async void StartDownloadingStudentImages(IEnumerable<ImageRequestDTO> requests)
        {
            await ImageService.StartGettingImagesForStudents(requests);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AllImageProvider<T> : IImageProvider<T> where T : class
    {
        private readonly ICacheExpiryManager _cacheExpiryManager;
        private readonly IImageCache<T> _fileSystemCache;
        private readonly IImageService _imageService;
        private readonly IImageCache<T> _inMemoryCache;
        private ImageUpdatedEventHander<T> _imageUpdated;
        public List<ImageDetail<BitmapImage>> _lstImage;
        private int listSize;
        private int responseListSize;

        public delegate void CallbackEventHandler(List<ImageDetail<BitmapImage>> list);
        public event CallbackEventHandler Callback;
        ImageDetail<BitmapImage> image = new ImageDetail<BitmapImage>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSystemCache"></param>
        /// <param name="imageService"></param>
        /// <param name="cacheExpiryManager"></param>
        protected AllImageProvider(IImageCache<T> fileSystemCache, IImageService imageService, ICacheExpiryManager cacheExpiryManager)
        {
            _fileSystemCache = fileSystemCache;
            _imageService = imageService;
            _cacheExpiryManager = cacheExpiryManager;
            _lstImage = new System.Collections.Generic.List<ImageDetail<BitmapImage>>();
            _imageService.ImageDownloaded += OnImageDownloaded;
            listSize = 0;
            responseListSize = 0;           
        }

        public IImageService ImageService
        {
            get { return _imageService; }
        }

        ImageDetail<T> IImageProvider<T>.GetImage(int personId)
        {
            ImageDetail<T> imageDetail = null;//_inMemoryCache.GetImageDetail(personId);

            if (imageDetail == null)
            {
                imageDetail = _fileSystemCache.GetImageDetail(personId);
               // _inMemoryCache.UpdateFromOtherCacheWithStudentIds(_fileSystemCache, new List<int> { personId });
            }

            return imageDetail;
        }

        public abstract IEnumerable<ImageDetail<T>> GetImages(IEnumerable<int> personIds);

        event ImageUpdatedEventHander<T> IImageProvider<T>.ImageUpdated
        {
            add { _imageUpdated += value; }
            remove { _imageUpdated -= value; }
        }

        public void Dispose()
        {
            _imageService.ImageDownloaded -= OnImageDownloaded;
        }

        public IEnumerable<ImageDetail<T>> GetImageDetails(IEnumerable<int> personIds, out List<ImageRequestDTO> imageRequests)
        {
            var imageDetails = new List<ImageDetail<T>>();
            imageRequests = new List<ImageRequestDTO>();

            foreach (int personId in personIds)
            {
                ImageDetail<T> imageDetail = null;//_inMemoryCache.GetImageDetail(personId);

                if (imageDetail == null)
                {
                    imageDetail = _fileSystemCache.GetImageDetail(personId);
                }

                if (!_cacheExpiryManager.EntryExists(personId) || _cacheExpiryManager.HasExpired(personId))
                {
                    imageRequests.Add(new ImageRequestDTO
                    {
                        PersonId = personId,
                        PhotoId = (imageDetail != null) ? imageDetail.PhotoId : 0,
                    });
                    listSize++;
                }

                if (imageDetail != null)
                {
                    imageDetails.Add(imageDetail);
                }
            }

            //_inMemoryCache.UpdateFromOtherCacheWithStudentIds(_fileSystemCache, personIds);
            return imageDetails;
        }

        private void OnImageUpdated(ImageUpdatedEventHandlerArgs<T> args)
        {

            ImageUpdatedEventHander<T> handler = _imageUpdated;
            if (handler != null)
            {
                handler(args);
            }
        }

        private void OnImageDownloaded(ImageDownloadedEventHandlerArgs args)
        {
            int personId = args.DownloadedImage.PersonId;
            int photoId = args.DownloadedImage.PhotoId;

            _cacheExpiryManager.SetExpiration(personId, DateTime.Now);
            responseListSize = responseListSize + 1;
            if (args.DownloadedImage.PhotoAvailable)
            {
                if (args.DownloadedImage.IsNewImage)
                {
                    byte[] imageBytes = args.DownloadedImage.ImageBytes;
                    //MemoryStream stream = new MemoryStream(imageBytes);

                    BitmapImage bmp = new BitmapImage();
                    //bmp.SetSource(stream.AsRandomAccessStream());
                    _inMemoryCache.AddImageDetail(personId, photoId, imageBytes);
                   // _fileSystemCache.AddImageDetail(personId, photoId, imageBytes);
                    bmp = SetImageFromByteArray(imageBytes);
                    ImageDetail<T> imageDetail = _inMemoryCache.GetImageDetail(personId);

                    if (imageDetail != null)
                    {
                        imageDetail.PhotoAvailable = args.DownloadedImage.PhotoAvailable;
                        OnImageUpdated(new ImageUpdatedEventHandlerArgs<T>(imageDetail));
                    }

                    //(image.PersonId= personId,image.PhotoId= photoId,image.Image = typeof(BitmapImage)); 
                    /*
                    image.Image = bmp;
                    image.PersonId = personId;
                    image.PhotoAvailable = true;
                    _lstImage.Add(image);
                    //responseListSize = responseListSize+1;
                    ImageDetail<T> imageDetail = null;//image;// _fileSystemCache.GetImageDetail(personId);

                    if (imageDetail != null)
                    {
                        imageDetail.PhotoAvailable = args.DownloadedImage.PhotoAvailable;
                        OnImageUpdated(new ImageUpdatedEventHandlerArgs<T>(imageDetail));
                    }
                     */
                    /*
                    if (responseListSize == listSize)
                    {
                        if (Callback != null)
                            Callback();
                    }
                     */
                  
                }
            }
            else
            {
                //_inMemoryCache.DeleteImageDetail(personId);
                _fileSystemCache.DeleteImageDetail(personId);
                BitmapImage bmp = new BitmapImage();
                //bmp.SetSource(stream.AsRandomAccessStream());
                // _inMemoryCache.AddImageDetail(personId, photoId, imageBytes);
                // _fileSystemCache.AddImageDetail(personId, photoId, imageBytes);
                //bmp = SetImageFromByteArray(imageBytes);
                //(image.PersonId= personId,image.PhotoId= photoId,image.Image = typeof(BitmapImage)); 
                image.Image = bmp;
                image.PersonId = personId;
                image.PhotoAvailable = true;
                _lstImage.Add(image);
                OnImageUpdated(new ImageUpdatedEventHandlerArgs<T>(new ImageDetail<T> { PersonId = personId, PhotoAvailable = false }));
            }
            if (responseListSize == listSize)
            {
                if (Callback != null)
                    Callback(_lstImage);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="image"></param>
        public BitmapImage SetImageFromByteArray(byte[] data)
        {
            using (InMemoryRandomAccessStream raStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(raStream))
                {
                    // Write the bytes to the stream
                    writer.WriteBytes(data);

                    // Store the bytes to the MemoryStream
                    //writer.StoreAsync();

                    // Not necessary, but do it anyway
                    // writer.FlushAsync();

                    // Detach from the Memory stream so we don't close it
                    writer.DetachStream();
                }

                raStream.Seek(0);

                BitmapImage bitMapImage = new BitmapImage();
                bitMapImage.SetSource(raStream);   
                return bitMapImage;           
            }
        }

        public bool didDownloadFinish()
        {
            return responseListSize == listSize;
        }

        public List<ImageDetail<BitmapImage>> getResponseList()
        {
            return _lstImage;
        }

    }
    public class studentImage<T> where T : class
    {
        public int personId { get; set; }
        public int photoId { get; set; }
        public BitmapImage image { get; set; }
        public studentImage(int _personId,int _photoId,BitmapImage _image)
        {
            personId = _personId;
            photoId = _photoId;
            image = _image;
        }
    }

}
    
    