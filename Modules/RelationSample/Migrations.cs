using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace RelationSample {
    public class Migrations : DataMigrationImpl {

        public int Create() {
			// Creating table dbo_RelationSample_StateRecord
			SchemaBuilder.CreateTable("StateRecord", table => table
				.Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
				.Column<String>("Code", column => column.WithLength(2))
				.Column("Name", DbType.String)
			);

			// Creating table dbo_RelationSample_AddressPartRecord
			SchemaBuilder.CreateTable("AddressPartRecord", table => table
				.ContentPartRecord()
				.Column("Address", DbType.String)
				.Column("City", DbType.String)
				.Column("Zip", DbType.String)
				.Column("StateRecord_Id", DbType.Int32)
			);

            ContentDefinitionManager.AlterPartDefinition("AddressPart",
                    builder => builder.Attachable());

            return 1;
        }
    }
}