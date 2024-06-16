using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmosFriendsPhotoAlbum.Models.ConData
{
  [Table("Friends", Schema = "dbo")]
  public partial class Friend
  {
    [NotMapped]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("@odata.etag")]
    public string ETag
    {
        get;
        set;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FriendID
    {
      get;
      set;
    }

    public IEnumerable<FriendPhoto> FriendPhotos { get; set; }
    [ConcurrencyCheck]
    public string FirstName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string LastName
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public DateTime DateOfBirth
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public int? GenderID
    {
      get;
      set;
    }
    public Gender Gender { get; set; }
    [ConcurrencyCheck]
    public string EmailAddress
    {
      get;
      set;
    }
    [ConcurrencyCheck]
    public string PhoneNumber
    {
      get;
      set;
    }
  }
}
