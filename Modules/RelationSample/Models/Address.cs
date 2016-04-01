using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationSample.Models
{
    public class StateRecord
    {
        public virtual int Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public static StateRecord DeserializeStateRecord(string rawStateRecord)
        {
            if (rawStateRecord == null)
            {
                return new StateRecord();
            }

            var stateRecordArray = rawStateRecord.Split(new[] { ',' });

            return new StateRecord()
            {
                Id = String.IsNullOrEmpty(stateRecordArray[0]) ? 0 : Int32.Parse(stateRecordArray[0]),
                Code = stateRecordArray[1],
                Name = stateRecordArray[2]
            };
        }

        public static string SerializeStateRecord(StateRecord stateRecord)
        {
            if (stateRecord == null)
            {
                return "";
            }

            return String.Join(",", stateRecord.Id, stateRecord.Code, stateRecord.Name);
        }
    }


    public class AddressPartRecord: ContentPartRecord
    {
        public virtual String Address { get; set; }
        public virtual String City { get; set; }
        public virtual StateRecord StateRecord { get; set; }
        public virtual string Zip { get; set; }

    }

    public class AddressPart : ContentPart<AddressPartRecord>
    {
        public String Address
        {
            get { return Retrieve(r => r.Address); }
            set { Store(r => r.Address, value); }
        }

        public String City
        {
            get { return Retrieve(r => r.City); }
            set { Store(r => r.City, value); }
        }

        public String Zip
        {
            get { return Retrieve(r => r.Zip); }
            set { Store(r => r.Zip, value); }
        }


        public StateRecord State
        {
            get
            {
                String rawStateRecord = Retrieve<String>("StateRecord");
                return StateRecord.DeserializeStateRecord(rawStateRecord);
            }
            set
            {
                var serializedStateRecord = StateRecord.SerializeStateRecord(value);
                Store("StateRecord", serializedStateRecord);
            }
        }

    }
}