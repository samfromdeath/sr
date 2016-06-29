using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace sr
{
    public class MysqlFastLoader
    {
        public object SameDynamicObject = new object();
        public DynamicMethod method;
        
        private delegate void OneParameter<TParameter0>
            (TParameter0 p0);

        public MysqlFastLoader(MySqlDataReader reader)
        {            
            if(reader.Read())
            {
                Type[] types = { typeof(IDataReader) };
                // what we need to do is create a customer reader for this column...                
                method = new DynamicMethod(string.Empty, typeof(void), types);

                ILGenerator il = method.GetILGenerator();

                Label lb = il.DefineLabel();

                il.MarkLabel(lb);

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Label dyn = il.DefineLabel();

                    var typeCode = Type.GetTypeCode(reader.GetFieldType(i));

                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldarg_0);

                    il.Emit(OpCodes.Callvirt, typeof(IDataReader).
                    GetMethod(nameof(IDataReader.IsDBNull), new Type[] { typeof(int) }));
                    il.Emit(OpCodes.Brtrue, dyn);

                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldarg_0);

                    switch (typeCode)
                    {                                                
                        case TypeCode.Boolean:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
                    GetMethod(nameof(IDataReader.GetBoolean), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Char:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetChar), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.SByte:                            
                        case TypeCode.Byte:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetByte), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetInt16), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetInt32), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Int64:                            
                        case TypeCode.UInt64:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetInt64), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Single:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetFloat), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Double:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetDouble), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.Decimal:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetDecimal), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.DateTime:
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
           GetMethod(nameof(IDataReader.GetDateTime), new Type[] { typeof(int) }));

                            il.Emit(OpCodes.Call, typeof(Convert).
                   GetMethod(nameof(Convert.ToString)));
                            break;
                        case TypeCode.String:                            
                            il.Emit(OpCodes.Callvirt, typeof(IDataReader).
                    GetMethod(nameof(IDataReader.GetString)));                            
                            break;
                        default:
                            break;
                    }

                    il.Emit(OpCodes.Call, typeof(Console).
                    GetMethod(nameof(Console.Write), new Type[] { typeof(string) }));

                    il.MarkLabel(dyn);
                }

                il.Emit(OpCodes.Call, typeof(Console).
                    GetMethod(nameof(Console.WriteLine), new Type[] { typeof(void) }));

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, typeof(IDataReader).
                    GetMethod(nameof(IDataReader.Read)));
                il.Emit(OpCodes.Brtrue, lb);

                il.Emit(OpCodes.Ret);

                OneParameter<IDataReader> invokeSquareIt =
    (OneParameter<IDataReader>)
    method.CreateDelegate(typeof(OneParameter<IDataReader>));

                invokeSquareIt(reader);
            }
            //var columnName = reader.GetName(i);
            //var value = reader[i];
            //var dotNetType = reader.GetFieldType(i);
            //var sqlType = reader.GetDataTypeName(i);
            //var specificType = reader.GetProviderSpecificFieldType(i);
            //fieldMetaData.columnName = columnName;
            //fieldMetaData.value = value;
            //fieldMetaData.dotNetType = dotNetType;
            //fieldMetaData.sqlType = sqlType;
            //fieldMetaData.specificType = specificType;
            //metaDataList.Add(fieldMetaData);
        }
    }
}
