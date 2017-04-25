using DocSolTemplateer.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocSolTemplateer.Infrastructure.BaseTypes
{
    public class SCTControlBase
    { 
        private Dictionary<string, string> ControlIds = new Dictionary<string, string>();
        public List<object> ControlValues =  new List<object>();
        
        public int Order { get; set; }        
        public string DisplayText { get; set; }
        public string InternalName { get; set; }        

        public SCTControlKindEnum Kind { get; set; }
        public int Width { get; set; }
        public Guid Grouping { get; set; }

        public IEnumerable<string> GetObjectIds(string id)
        {
            return ControlIds.Where(x => x.Key == id).Select(x => x.Value);
        }

        public void AddObjectId(string guid, string Id)
        {
            ControlIds.Add(guid, Id);
        }

        public void SetValue(object theValue)
        {
            ControlValues.Add(theValue);
        }

        #region metainformation
        public Dictionary<string, object> MetaInformation = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        #endregion
    }    
}
