using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.Library.Models
{
    public enum FaceRecognitionTrainingStatus
    {
        FoundMoreThenOneFace,
        NoFacesFound,
        TrainingSuccessful,
        TrainingFailure
    }
}
