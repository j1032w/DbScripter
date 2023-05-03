using System;

namespace DBScripter.Domain
{
    [Flags]
    public enum DatabaseObjectType
    {
        None = 0,

        StoredProcedure = 1 << 0,       //1,    000001
        Table = 1 << 1,                 //2,    000010
        View = 1 << 2,                  //4,    000100
        UserDefined_Function = 1 << 3,   //8,    001000
        UserDefined_Aggregate = 1 << 4,  //16,   010000
        UserDefined_DataType = 1 << 5,   //32,   100000
        UserDefined_TableType = 1 << 6,
        UserDefined_Type = 1 << 7,

        All =   StoredProcedure | 
                Table | 
                View | 
                UserDefined_Function | 
                UserDefined_Aggregate | 
                UserDefined_DataType | 
                UserDefined_TableType | 
                UserDefined_Type
    }
}
