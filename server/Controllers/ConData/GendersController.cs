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

  [Route("odata/ConData/Genders")]
  public partial class GendersController : ODataController
  {
    private AmosFriendsPhotoAlbum.Data.ConDataContext context;

    public GendersController(AmosFriendsPhotoAlbum.Data.ConDataContext context)
    {
      this.context = context;
    }
    // GET /odata/ConData/Genders
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet]
    public IEnumerable<Models.ConData.Gender> GetGenders()
    {
      var items = this.context.Genders.AsQueryable<Models.ConData.Gender>();
      this.OnGendersRead(ref items);

      return items;
    }

    partial void OnGendersRead(ref IQueryable<Models.ConData.Gender> items);

    partial void OnGenderGet(ref SingleResult<Models.ConData.Gender> item);

    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    [HttpGet("/odata/ConData/Genders(GenderID={GenderID})")]
    public SingleResult<Gender> GetGender(int key)
    {
        var items = this.context.Genders.Where(i=>i.GenderID == key);
        var result = SingleResult.Create(items);

        OnGenderGet(ref result);

        return result;
    }
    partial void OnGenderDeleted(Models.ConData.Gender item);
    partial void OnAfterGenderDeleted(Models.ConData.Gender item);

    [HttpDelete("/odata/ConData/Genders(GenderID={GenderID})")]
    public IActionResult DeleteGender(int key)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var items = this.context.Genders
                .Where(i => i.GenderID == key)
                .Include(i => i.Friends)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.ConData.Gender>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnGenderDeleted(item);
            this.context.Genders.Remove(item);
            this.context.SaveChanges();
            this.OnAfterGenderDeleted(item);

            return new NoContentResult();
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnGenderUpdated(Models.ConData.Gender item);
    partial void OnAfterGenderUpdated(Models.ConData.Gender item);

    [HttpPut("/odata/ConData/Genders(GenderID={GenderID})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PutGender(int key, [FromBody]Models.ConData.Gender newItem)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Genders
                .Where(i => i.GenderID == key)
                .Include(i => i.Friends)
                .AsQueryable();

            items = EntityPatch.ApplyTo<Models.ConData.Gender>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            this.OnGenderUpdated(newItem);
            this.context.Genders.Update(newItem);
            this.context.SaveChanges();

            var itemToReturn = this.context.Genders.Where(i => i.GenderID == key);
            this.OnAfterGenderUpdated(newItem);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    [HttpPatch("/odata/ConData/Genders(GenderID={GenderID})")]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult PatchGender(int key, [FromBody]Delta<Models.ConData.Gender> patch)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = this.context.Genders.Where(i => i.GenderID == key);

            items = EntityPatch.ApplyTo<Models.ConData.Gender>(Request, items);

            var item = items.FirstOrDefault();

            if (item == null)
            {
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }

            patch.Patch(item);

            this.OnGenderUpdated(item);
            this.context.Genders.Update(item);
            this.context.SaveChanges();

            var itemToReturn = this.context.Genders.Where(i => i.GenderID == key);
            return new ObjectResult(SingleResult.Create(itemToReturn));
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }

    partial void OnGenderCreated(Models.ConData.Gender item);
    partial void OnAfterGenderCreated(Models.ConData.Gender item);

    [HttpPost]
    [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
    public IActionResult Post([FromBody] Models.ConData.Gender item)
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

            this.OnGenderCreated(item);
            this.context.Genders.Add(item);
            this.context.SaveChanges();

        
            this.OnAfterGenderCreated(item);
            
            return Created($"odata/ConData/Genders/{item.GenderID}", item);
        }
        catch(Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return BadRequest(ModelState);
        }
    }
  }
}
