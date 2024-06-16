using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AmosFriendsPhotoAlbum.Data;

namespace AmosFriendsPhotoAlbum
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;
        public ExportConDataController(ConDataContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/ConData/friends/csv")]
        [HttpGet("/export/ConData/friends/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFriendsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Friends, Request.Query, false), fileName);
        }

        [HttpGet("/export/ConData/friends/excel")]
        [HttpGet("/export/ConData/friends/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFriendsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Friends, Request.Query, false), fileName);
        }
        [HttpGet("/export/ConData/friendphotos/csv")]
        [HttpGet("/export/ConData/friendphotos/csv(fileName='{fileName}')")]
        public FileStreamResult ExportFriendPhotosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.FriendPhotos, Request.Query, false), fileName);
        }

        [HttpGet("/export/ConData/friendphotos/excel")]
        [HttpGet("/export/ConData/friendphotos/excel(fileName='{fileName}')")]
        public FileStreamResult ExportFriendPhotosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.FriendPhotos, Request.Query, false), fileName);
        }
        [HttpGet("/export/ConData/genders/csv")]
        [HttpGet("/export/ConData/genders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportGendersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Genders, Request.Query, false), fileName);
        }

        [HttpGet("/export/ConData/genders/excel")]
        [HttpGet("/export/ConData/genders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportGendersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Genders, Request.Query, false), fileName);
        }
    }
}
