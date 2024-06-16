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
    public partial class AddGenderComponent : ComponentBase
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

        AmosFriendsPhotoAlbum.Models.ConData.Gender _gender;
        protected AmosFriendsPhotoAlbum.Models.ConData.Gender gender
        {
            get
            {
                return _gender;
            }
            set
            {
                if (!object.Equals(_gender, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "gender", NewValue = value, OldValue = _gender };
                    _gender = value;
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
            gender = new AmosFriendsPhotoAlbum.Models.ConData.Gender(){};
        }

        protected async System.Threading.Tasks.Task Form0Submit(AmosFriendsPhotoAlbum.Models.ConData.Gender args)
        {
            try
            {
                var conDataCreateGenderResult = await ConData.CreateGender(gender:gender);
                DialogService.Close(gender);
            }
            catch (System.Exception conDataCreateGenderException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to create new Gender!" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
