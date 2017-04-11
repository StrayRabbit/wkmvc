using Domain;
using System;
using System.Collections.Generic;

namespace Service.IService
{
    public interface ISystemManage : IRepository<SYS_SYSTEM>
    {
        dynamic LoadSystemInfo(List<string> systems);
    }
}
