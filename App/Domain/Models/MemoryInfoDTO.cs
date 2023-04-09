﻿using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Models
{
    public class MemoryInfoDTO
    {
        public ulong TotalPhysicalMb { get; set; }
        public double PercentInUse { get; set; }

        public static MemoryInfoDTO FromEntity(MemoryInfo i)
        {
            MemoryInfoDTO dto = new MemoryInfoDTO();
            dto.TotalPhysicalMb = i.TotalPhysicalMb;
            dto.PercentInUse = i.PercentInUse;

            return dto;
        }
        public MemoryInfo ToEntity()
        {
            MemoryInfo info = new MemoryInfo();
            info.TotalPhysicalMb = TotalPhysicalMb;
            info.PercentInUse = PercentInUse;
            return info;
        }
    }
   
}
