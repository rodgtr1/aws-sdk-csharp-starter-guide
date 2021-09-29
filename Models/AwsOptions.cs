using System;
using System.Collections.Generic;

namespace app.Models {
    public class AwsOptions {
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
        public string AWSRegion { get; set; }
        public string AWSOrgName { get; set; }
    }
}