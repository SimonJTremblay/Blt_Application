using BltData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BltData
{
    public interface IBluf
    {
        Bluf GetById(int id);
        void PostBluf(Bluf bluf);
    }
}
