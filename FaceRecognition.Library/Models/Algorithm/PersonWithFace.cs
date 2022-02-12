using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.Library.Models
{
  public  class PersonWithFace
    {

        public SimplePerson PersonOnFace { get; set; }
        public Face FaceForPerson { get; set; }
        public double EigenDistance { get; set; }
        public FaceRecognitionStatus RecognitionStatus { get; set; }

    }
}
