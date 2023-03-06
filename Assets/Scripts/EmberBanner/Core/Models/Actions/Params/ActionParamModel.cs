using System;
using EmberBanner.Core.Enums;
using EmberBanner.Core.Enums.Actions;

namespace EmberBanner.Core.Models.Actions.Params
{
    [Serializable]
    public class ActionParamModel : AbstractModel
    {
        public ActionParamType Type;
        public int IntValue;
        public string StringValue;
        public string TagString;
    }
}