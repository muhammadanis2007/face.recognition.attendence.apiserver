using FaceRecognition.Library.DataAcccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaceRecognition.Library.Models
{
    [Table("Images")]
    class Image : EntityBase
    {
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
