using MachineHealthCheck.Domain.Entities;
using System.Diagnostics;
using Microsoft.Win32;
using Hardware.Info;

namespace MachineHealthCheck.Remote.Services
{
    internal class SystemInfoRetrievalService
    {
    
        static readonly IHardwareInfo hardwareInfo = new HardwareInfo();
        public SystemInfoRetrievalService()
        { 
           
        }

        public HealthCheck GetInfo()
        {
            HealthCheck healthCheck = new HealthCheck();

            hardwareInfo.RefreshAll();
            healthCheck.OperatingSystem = hardwareInfo.OperatingSystem.Name;
            healthCheck.OSVersion = hardwareInfo.OperatingSystem.VersionString;
            var cpulst = new List<CPUInfo>() { GetCPUInfo() };
            healthCheck.CPUInfo = cpulst;
            healthCheck.MemoryInfo = GetMemoryInfo();
            var disklst = new List<DiskInfo>() { GetDiskInfo() };
            healthCheck.DiskInfo = disklst;
            healthCheck.SqlInfo = GetSqlInfo();
 
            return healthCheck;
        }

        public SqlInfo GetSqlInfo()
        {
            SqlInfo info = new SqlInfo();

            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    info.HasSqlServer = true;
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        RegistryKey versionKey = hklm.OpenSubKey(@$"SOFTWARE\Microsoft\Microsoft SQL Server\{instanceName}\MSSQLServer\CurrentVersion", false);
                        if (versionKey != null)
                        {
                            info.SqlServerVersion = (string)versionKey.GetValue(versionKey.GetValueNames()[0]);
                        }
                    }
                }
            }
            return info;
        }
        public CPUInfo GetCPUInfo(int index = 0)
        {
            CPUInfo info = new CPUInfo();

            var cpuUsage = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            Thread.Sleep(500);
            var firstCall = cpuUsage.NextValue();

            for (int i = 0; i < 5; i++)
            {
                cpuUsage.NextValue();
                Thread.Sleep(500);
                if (i+1 == 5) info.PercentInUse = (int)cpuUsage.NextValue();
            }

            info.Name = hardwareInfo.CpuList[index].Name;
            info.NumOfCores = (int)hardwareInfo.CpuList[index].NumberOfCores;
            info.NumOfLogicalProcessors = (int)hardwareInfo.CpuList[index].NumberOfLogicalProcessors;
            info.CurrClockSpeed = (int)hardwareInfo.CpuList[index].CurrentClockSpeed;
            return info;
        }
        public MemoryInfo GetMemoryInfo(int index = 0)
        {
            MemoryInfo info = new MemoryInfo();

            var memUsage = new PerformanceCounter("Memory", "Available MBytes");
            info.TotalPhysicalMb = hardwareInfo.MemoryStatus.TotalPhysical / 1024 / 1024;
            var used = memUsage.NextValue();
            info.PercentInUse = (100.0 * (info.TotalPhysicalMb - used) / info.TotalPhysicalMb);
            return info;

        }
        public DiskInfo GetDiskInfo(int index = 0)
        {
            DiskInfo info = new DiskInfo();

            PerformanceCounter disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            Thread.Sleep(500);
            var firstCall = disk.NextValue();

            for (int i = 0; i < 5; i++)
            {
                disk.NextValue();
                Thread.Sleep(500);
                if (i+1 == 5) info.PercentUtilization = disk.NextValue();
            }

            info.FreeSpaceMb = System.IO.DriveInfo.GetDrives()[0].AvailableFreeSpace / 1024 / 1024;
            info.CapacityMb = hardwareInfo.DriveList[index].Size / 1024 / 1024;
            return info;
        }
    }
}
