using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using AmosFriendsPhotoAlbum.Models.ConData;
using AmosFriendsPhotoAlbum.Client.Pages;

namespace AmosFriendsPhotoAlbum.Pages
{
    public partial class FriendPhotosComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

        public void Reload()
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs args)
        {
        }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager UriHelper { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected ConDataService ConData { get; set; }
        protected RadzenDataGrid<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> grid0;

        string _search;
        protected string search
        {
            get
            {
                return _search;
            }
            set
            {
                if (!object.Equals(_search, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "search", NewValue = value, OldValue = _search };
                    _search = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> _getFriendPhotosResult;
        protected IEnumerable<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> getFriendPhotosResult
        {
            get
            {
                return _getFriendPhotosResult;
            }
            set
            {
                if (!object.Equals(_getFriendPhotosResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendPhotosResult", NewValue = value, OldValue = _getFriendPhotosResult };
                    _getFriendPhotosResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        int _getFriendPhotosCount;
        protected int getFriendPhotosCount
        {
            get
            {
                return _getFriendPhotosCount;
            }
            set
            {
                if (!object.Equals(_getFriendPhotosCount, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getFriendPhotosCount", NewValue = value, OldValue = _getFriendPhotosCount };
                    _getFriendPhotosCount = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            if (string.IsNullOrEmpty(search)) {
                search = "";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            var dialogResult = await DialogService.OpenAsync<AddFriendPhoto>("Add Friend Photo", null);
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ConData.ExportFriendPhotosToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Friend", Select = "PhotoID,Friend.FirstName as FriendFirstName,PhotoURL,PhotoDescription,DateTaken" }, $"Friend Photos");

            }

            if (args == null || args.Value == "xlsx")
            {
                await ConData.ExportFriendPhotosToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Friend", Select = "PhotoID,Friend.FirstName as FriendFirstName,PhotoURL,PhotoDescription,DateTaken" }, $"Friend Photos");

            }
        }

        protected async System.Threading.Tasks.Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var conDataGetFriendPhotosResult = await ConData.GetFriendPhotos(filter:$@"(contains(PhotoURL,""{search}"") or contains(PhotoDescription,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby:$"{args.OrderBy}", expand:$"Friend", top:args.Top, skip:args.Skip, count:args.Top != null && args.Skip != null);
                getFriendPhotosResult = conDataGetFriendPhotosResult.Value.AsODataEnumerable();

                getFriendPhotosCount = conDataGetFriendPhotosResult.Count;
            }
            catch (System.Exception conDataGetFriendPhotosException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to load FriendPhotos" });
            }
        }

        protected async System.Threading.Tasks.Task Grid0RowDoubleClick(DataGridRowMouseEventArgs<AmosFriendsPhotoAlbum.Models.ConData.FriendPhoto> args)
        {
            var dialogResult = await DialogService.OpenAsync<EditFriendPhoto>("Edit Friend Photo", new Dictionary<string, object>() { {"PhotoID", args.Data.PhotoID} });
            await grid0.Reload();

            await InvokeAsync(() => { StateHasChanged(); });
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var conDataDeleteFriendPhotoResult = await ConData.DeleteFriendPhoto(photoId:data.PhotoID);
                    if (conDataDeleteFriendPhotoResult != null && conDataDeleteFriendPhotoResult.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        await grid0.Reload();
                    }

                    if (conDataDeleteFriendPhotoResult != null && conDataDeleteFriendPhotoResult.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete FriendPhoto" });
                    }
                }
            }
            catch (System.Exception conDataDeleteFriendPhotoException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete FriendPhoto" });
            }
        }
    }
}
