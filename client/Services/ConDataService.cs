
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components;
using AmosFriendsPhotoAlbum.Models.ConData;

namespace AmosFriendsPhotoAlbum
{
    public partial class ConDataService
    {
        private readonly HttpClient httpClient;
        private readonly Uri baseUri;
        private readonly NavigationManager navigationManager;
        public ConDataService(NavigationManager navigationManager, HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            this.navigationManager = navigationManager;
            this.baseUri = new Uri($"{navigationManager.BaseUri}odata/ConData/");
        }

        public async System.Threading.Tasks.Task ExportFriendsToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/friends/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/friends/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFriendsToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/friends/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/friends/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFriends(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.Friend>> GetFriends(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Friends");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFriends(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.Friend>>(response);
        }
        partial void OnCreateFriend(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.Friend> CreateFriend(AmosFriendsPhotoAlbum.Models.ConData.Friend friend = default(AmosFriendsPhotoAlbum.Models.ConData.Friend))
        {
            var uri = new Uri(baseUri, $"Friends");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(friend), Encoding.UTF8, "application/json");

            OnCreateFriend(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.Friend>(response);
        }

        public async System.Threading.Tasks.Task ExportFriendPhotosToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/friendphotos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/friendphotos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportFriendPhotosToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/friendphotos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/friendphotos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetFriendPhotos(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>> GetFriendPhotos(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"FriendPhotos");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFriendPhotos(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>>(response);
        }
        partial void OnCreateFriendPhoto(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> CreateFriendPhoto(AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto friendPhoto = default(AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto))
        {
            var uri = new Uri(baseUri, $"FriendPhotos");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(friendPhoto), Encoding.UTF8, "application/json");

            OnCreateFriendPhoto(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>(response);
        }

        public async System.Threading.Tasks.Task ExportGendersToExcel(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async System.Threading.Tasks.Task ExportGendersToCSV(Radzen.Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/condata/genders/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }
        partial void OnGetGenders(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.Gender>> GetGenders(string filter = default(string), string orderby = default(string), string expand = default(string), int? top = default(int?), int? skip = default(int?), bool? count = default(bool?), string format = default(string), string select = default(string))
        {
            var uri = new Uri(baseUri, $"Genders");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:filter, top:top, skip:skip, orderby:orderby, expand:expand, select:select, count:count);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGenders(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<Radzen.ODataServiceResult<AmosFriendsPhotoAlbum.Models.ConData.Gender>>(response);
        }
        partial void OnCreateGender(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.Gender> CreateGender(AmosFriendsPhotoAlbum.Models.ConData.Gender gender = default(AmosFriendsPhotoAlbum.Models.ConData.Gender))
        {
            var uri = new Uri(baseUri, $"Genders");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);


            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(gender), Encoding.UTF8, "application/json");

            OnCreateGender(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.Gender>(response);
        }
        partial void OnDeleteFriend(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteFriend(int? friendId = default(int?))
        {
            var uri = new Uri(baseUri, $"Friends({friendId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFriend(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetFriendByFriendId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.Friend> GetFriendByFriendId(string expand = default(string), int? friendId = default(int?))
        {
            var uri = new Uri(baseUri, $"Friends({friendId})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFriendByFriendId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.Friend>(response);
        }
        partial void OnUpdateFriend(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateFriend(int? friendId = default(int?), AmosFriendsPhotoAlbum.Models.ConData.Friend friend = default(AmosFriendsPhotoAlbum.Models.ConData.Friend))
        {
            var uri = new Uri(baseUri, $"Friends({friendId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", friend.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(friend), Encoding.UTF8, "application/json");

            OnUpdateFriend(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteFriendPhoto(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteFriendPhoto(int? photoId = default(int?))
        {
            var uri = new Uri(baseUri, $"FriendPhotos({photoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteFriendPhoto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetFriendPhotoByPhotoId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> GetFriendPhotoByPhotoId(string expand = default(string), int? photoId = default(int?))
        {
            var uri = new Uri(baseUri, $"FriendPhotos({photoId})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetFriendPhotoByPhotoId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto>(response);
        }
        partial void OnUpdateFriendPhoto(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateFriendPhoto(int? photoId = default(int?), AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto friendPhoto = default(AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto))
        {
            var uri = new Uri(baseUri, $"FriendPhotos({photoId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", friendPhoto.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(friendPhoto), Encoding.UTF8, "application/json");

            OnUpdateFriendPhoto(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnDeleteGender(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> DeleteGender(int? genderId = default(int?))
        {
            var uri = new Uri(baseUri, $"Genders({genderId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            OnDeleteGender(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
        partial void OnGetGenderByGenderId(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<AmosFriendsPhotoAlbum.Models.ConData.Gender> GetGenderByGenderId(string expand = default(string), int? genderId = default(int?))
        {
            var uri = new Uri(baseUri, $"Genders({genderId})");
            uri = Radzen.ODataExtensions.GetODataUri(uri: uri, filter:null, top:null, skip:null, orderby:null, expand:expand, select:null, count:null);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            OnGetGenderByGenderId(httpRequestMessage);

            var response = await httpClient.SendAsync(httpRequestMessage);

            return await Radzen.HttpResponseMessageExtensions.ReadAsync<AmosFriendsPhotoAlbum.Models.ConData.Gender>(response);
        }
        partial void OnUpdateGender(HttpRequestMessage requestMessage);


        public async System.Threading.Tasks.Task<HttpResponseMessage> UpdateGender(int? genderId = default(int?), AmosFriendsPhotoAlbum.Models.ConData.Gender gender = default(AmosFriendsPhotoAlbum.Models.ConData.Gender))
        {
            var uri = new Uri(baseUri, $"Genders({genderId})");

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Patch, uri);

            httpRequestMessage.Headers.Add("If-Match", gender.ETag);

            httpRequestMessage.Content = new StringContent(Radzen.ODataJsonSerializer.Serialize(gender), Encoding.UTF8, "application/json");

            OnUpdateGender(httpRequestMessage);

            return await httpClient.SendAsync(httpRequestMessage);
        }
    }
}
