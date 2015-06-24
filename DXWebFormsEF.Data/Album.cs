namespace DXWebFormsEF.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        public Album()
        {
            Track = new HashSet<Track>();
        }

        public int AlbumId { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual ICollection<Track> Track { get; set; }
    }
}
