using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace AmosFriendsPhotoAlbum.Controllers.ConData
{
  using Models;
  using Data;
  using Models.ConData;

  [Route("odata/ConData/FriendPhotos")]
  public partial class FriendPhotosController : ODataController
  {
    private AmosFriendsPhotoAlbum.Data.ConDataContext context;

    public FriendPhotosController(AmosFriendsPhotoAlbum.Data.ConDataContext context)
    {
      this.context = context;
    }
    // GET /odata/ConData/FriendPhotos
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.ConData.FriendPhoto> GetFriendPhotos()
    {
      var items = this.context.FriendPhotos.AsQueryable<Models.ConData.FriendPhoto>();
      this.OnFriendPhotosRead(ref items);

      return items;
    }

    partial void OnFriendPhotosRead(ref IQueryable<Models.ConData.FriendPhoto> items);

    partial void OnFriendPhotoGet(ref SingleResult<Models.ConData.FriendPhoto> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/ConData/FriendPhotos(PhotoID={PhotoID})")]
    public SingleResult<FriendPhoto> GetFriendPhoto(int key)
    {
        var items = this.context.FriendPhotos.Where(i=>i.PhotoID == key);
        var result = SingleResult.Create(items);

        OnFriendPhotoGet(ref result);

        return result;
    }
    partial void OnFriendPhotoDeleted(Models.ConData.FriendPhoto item);
    partial void OnAfterFriendPhotoDeleted(Models.ConData.FriendPhoto item);

    [HttpDelete("/odata/ConData/FriendPhotos(PhotoID={PhotoID})")]
    public IActionResult DeleteFriendPhoto(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.FriendPhotos
                .Where(i => i.PhotoID == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.ConData.FriendPhoto>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFriendPhotoDeleted(item);
            this.context.FriendPhotos.Remove(item);
            this.context.SaveChanges();
            this.OnAfterFriendPhotoDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFriendPhotoUpdated(Models.ConData.FriendPhoto item);
    partial void OnAfterFriendPhotoUpdated(Models.ConData.FriendPhoto item);

    [HttpPut("/odata/ConData/FriendPhotos(PhotoID={PhotoID})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutFriendPhoto(int key, [FromBody]Models.ConData.FriendPhoto newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FriendPhotos
                .Where(i => i.PhotoID == key)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.ConData.FriendPhoto>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnFriendPhotoUpdated(newItem);
            this.context.FriendPhotos.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.FriendPhotos.Where(i => i.PhotoID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Friend");
            this.OnAfterFriendPhotoUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/ConData/FriendPhotos(PhotoID={PhotoID})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchFriendPhoto(int key, [FromBody]Delta<Models.ConData.FriendPhoto> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.FriendPhotos.Where(i => i.PhotoID == key);

            items = EntityPatch.ApplyTo<Models.ConData.FriendPhoto>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnFriendPhotoUpdated(item);
            this.context.FriendPhotos.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.FriendPhotos.Where(i => i.PhotoID == key);
            Request.QueryString = Request.QueryString.Add("$expand", "Friend");
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnFriendPhotoCreated(Models.ConData.FriendPhoto item);
    partial void OnAfterFriendPhotoCreated(Models.ConData.FriendPhoto item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.ConData.FriendPhoto item)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (item == null)
            {
                return BadRequest();
            }

            this.OnFriendPhotoCreated(item);
            this.context.FriendPhotos.Add(item);
            this.context.SaveChanges();

            var key = item.PhotoID;

            var itemToReturn = this.context.FriendPhotos.Where(i => i.PhotoID == key);

            Request.QueryString = Request.QueryString.Add("$expand", "Friend");

            this.OnAfterFriendPhotoCreated(item);

            return new ObjectResult(SingleResult.Create(itemToReturn))
            {
                StatusCode = 201
            };
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
