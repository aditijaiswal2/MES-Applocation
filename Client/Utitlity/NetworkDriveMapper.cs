using System.Runtime.InteropServices;

namespace MES.Client.Utitlity
{
    public class NetworkDriveMapper
    {
        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string lpName, int dwFlags, bool bForce);

        [StructLayout(LayoutKind.Sequential)]
        private struct NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        public static int MapDrive(string networkPath, string driveLetter, string username = null, string password = null)
        {
            NETRESOURCE netResource = new NETRESOURCE
            {
                dwType = 1, // RESOURCETYPE_DISK
                lpLocalName = driveLetter,
                lpRemoteName = networkPath
            };

            return WNetAddConnection2(ref netResource, password, username, 0);
        }

        public static int UnmapDrive(string driveLetter, bool force = true)
        {
            return WNetCancelConnection2(driveLetter, 0, force);
        }
    }
}
