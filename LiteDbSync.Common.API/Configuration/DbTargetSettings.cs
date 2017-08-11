﻿namespace LiteDbSync.Common.API.Configuration
{
    public class DbTargetSettings
    {
        public string  DbFilePath      { get; set; }
        public string  UniqueDbName    { get; set; }
        public string  CollectionName  { get; set; }


        public static DbTargetSettings CreateDefault()
        {
            return new DbTargetSettings
            {
                DbFilePath     = "DbFilePath goes here",
                UniqueDbName   = "distinct from others in the list",
                CollectionName = "CollectionName goes here",
            };
        }
    }
}
