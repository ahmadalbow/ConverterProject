using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ConverterServer
{
    [ServiceContract]
    public interface IService
    {
        // This is the only methods that need to be accessed by the client
        [OperationContract]
        string ConvertDollarsToText(string value);
    }
}
