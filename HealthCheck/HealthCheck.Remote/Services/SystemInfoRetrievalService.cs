using MachineHealthCheck.Domain.Entities;
using System.Diagnostics;
using Microsoft.Win32;
using Hardware.Info;

namespace MachineHealthCheck.Remote.Services
{
    internal class SystemInfoRetrievalService
    {
        private readonly IHardwareInfo hardwareInfo;
        public SystemInfoRetrievalService()
        {
            hardwareInfo = new HardwareInfo();
        }
        public HealthCheck GetInfo()
        {
            HealthCheck healthCheck = new HealthCheck();
            hardwareInfo.RefreshAll();
            healthCheck.OperatingSystem = hardwareInfo.OperatingSystem.Name;
            healthCheck.OSVersion = hardwareInfo.OperatingSystem.VersionString;
            healthCheck.CPUInfo = GetCPUInfo();
            healthCheck.MemoryInfo = GetMemoryInfo();
            healthCheck.DiskInfo = GetDiskInfo();
            healthCheck.SqlInfo = GetSqlInfo();
            return healthCheck;
        }
        private SqlInfo GetSqlInfo()
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
        private List<CPUInfo> GetCPUInfo()
        {
            List<CPUInfo> infoList = new List<CPUInfo>();
            CPUInfo first = new CPUInfo();
            var cpuUsage = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");
            Thread.Sleep(500);
            var firstCall = cpuUsage.NextValue();

            for (int i = 0; i < 5; i++)
            {
                cpuUsage.NextValue();
                Thread.Sleep(500);
                if (i+1 == 5) first.PercentInUse = (int)cpuUsage.NextValue();
            }

            foreach (var cpu in hardwareInfo.CpuList)
            {
                CPUInfo one = new CPUInfo();
                one.PercentInUse = first.PercentInUse;
                one.Name = cpu.Name;
                one.NumOfCores = (int)cpu.NumberOfCores;
                one.NumOfLogicalProcessors = (int)cpu.NumberOfLogicalProcessors;
                one.CurrClockSpeed = (int)cpu.CurrentClockSpeed;
                infoList.Add(one);
            }

            return infoList;
        }
        private MemoryInfo GetMemoryInfo()
        {
            MemoryInfo info = new MemoryInfo();

            var memUsage = new PerformanceCounter("Memory", "Available MBytes");
            info.TotalPhysicalMb = hardwareInfo.MemoryStatus.TotalPhysical / 1024 / 1024;
            var used = memUsage.NextValue();
            info.PercentInUse = (100.0 * (info.TotalPhysicalMb - used) / info.TotalPhysicalMb);
            return info;

        }
        private List<DiskInfo> GetDiskInfo()
        {
            List<DiskInfo> infoList = new List<DiskInfo>();
            DiskInfo first = new DiskInfo();
            PerformanceCounter disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            Thread.Sleep(500);
            var firstCall = disk.NextValue();

            for (int i = 0; i < 5; i++)
            {
                disk.NextValue();
                Thread.Sleep(500);
                if (i+1 == 5) first.PercentUtilization = disk.NextValue();
            }
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                DiskInfo one = new DiskInfo();
                one.PercentUtilization = first.PercentUtilization;
                one.FreeSpaceMb = drive.AvailableFreeSpace / 1024 / 1024;
                one.CapacityMb = (ulong)drive.TotalSize / 1024 / 1024;
                infoList.Add(one);
            }
            return infoList;
        }
    }
}
