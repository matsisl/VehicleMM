using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ServiceException : Exception
    {
        public override string Message
        {
            get => "Došlo je do pogreške kod pristupa podacima u bazi podataka!";
        }
    }
}
