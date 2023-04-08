using MachineHealthCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineHealthCheck.Domain.Models
{
    public class WorkQueueDTO
    {
        public string Key { get; set; } = null!;

        public static WorkQueueDTO FromWQ(WorkQueue w)
        {
            WorkQueueDTO dto = new WorkQueueDTO();
            dto.Key = w.Key;
            return dto;
        }
        public WorkQueue ToWQ()
        {
            WorkQueue q = new WorkQueue();
            q.isActive = true;
            q.Key = Key;
            return q;
        }
    }
}
