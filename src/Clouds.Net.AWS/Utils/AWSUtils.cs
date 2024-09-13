using Amazon;

namespace Clouds.Net.AWS.Utils
{
    public class AWSUtils
    {
        public static RegionEndpoint GetRegionFromString(string region)
        {
            var defaultRegion = RegionEndpoint.USEast1;

            if (string.IsNullOrEmpty(region))
            {
                return defaultRegion;
            }

            switch (region)
            {
                case "af-south-1":
                    return RegionEndpoint.AFSouth1;

                case "ap-east-1":
                    return RegionEndpoint.APEast1;

                case "ap-northeast-1":
                    return RegionEndpoint.APNortheast1;

                case "ap-northeast-2":
                    return RegionEndpoint.APNortheast2;

                case "ap-northeast-3":
                    return RegionEndpoint.APNortheast3;

                case "ap-south-1":
                    return RegionEndpoint.APSouth1;

                case "ap-south-2":
                    return RegionEndpoint.APSouth2;

                case "ap-southeast-1":
                    return RegionEndpoint.APSoutheast1;

                case "ap-southeast-2":
                    return RegionEndpoint.APSoutheast2;

                case "ap-southeast-3":
                    return RegionEndpoint.APSoutheast3;

                case "ap-southeast-4":
                    return RegionEndpoint.APSoutheast4;

                case "ap-southeast-5":
                    return RegionEndpoint.APSoutheast5;

                case "ca-central-1":
                    return RegionEndpoint.CACentral1;

                case "ca-west-1":
                    return RegionEndpoint.CAWest1;

                case "eu-central-1":
                    return RegionEndpoint.EUCentral1;

                case "eu-central-2":
                    return RegionEndpoint.EUCentral2;

                case "eu-north-1":
                    return RegionEndpoint.EUNorth1;

                case "eu-south-1":
                    return RegionEndpoint.EUSouth1;

                case "eu-south-2":
                    return RegionEndpoint.EUSouth2;

                case "eu-west-1":
                    return RegionEndpoint.EUWest1;

                case "eu-west-2":
                    return RegionEndpoint.EUWest2;

                case "eu-west-3":
                    return RegionEndpoint.EUWest3;

                case "il-central-1":
                    return RegionEndpoint.ILCentral1;

                case "me-central-1":
                    return RegionEndpoint.MECentral1;

                case "me-south-1":
                    return RegionEndpoint.MESouth1;

                case "sa-east-1":
                    return RegionEndpoint.SAEast1;

                case "us-east-1":
                    return RegionEndpoint.USEast1;

                case "us-east-2":
                    return RegionEndpoint.USEast2;

                case "us-west-1":
                    return RegionEndpoint.USWest1;

                case "us-west-2":
                    return RegionEndpoint.USWest2;

                case "cn-north-1":
                    return RegionEndpoint.CNNorth1;

                case "cn-northwest-1":
                    return RegionEndpoint.CNNorthWest1;

                case "us-gov-east-1":
                    return RegionEndpoint.USGovCloudEast1;

                case "us-gov-west-1":
                    return RegionEndpoint.USGovCloudWest1;

                case "us-iso-east-1":
                    return RegionEndpoint.USIsoEast1;

                case "us-iso-west-1":
                    return RegionEndpoint.USIsoWest1;

                case "us-isob-east-1":
                    return RegionEndpoint.USIsobEast1;

                case "eu-isoe-west-1":
                    return RegionEndpoint.EUIsoeWest1;

                default:
                    return defaultRegion;
            }
        }
    }
}
