using System;
using System.Collections.Generic;

namespace Rehber.Model.DataModels
{
    public class UserImages
    {
        public int ImageId { get; set; }
        public int UserId { get; set; }
        public byte[] ImageBinaryData { get; set; }
    }
}
