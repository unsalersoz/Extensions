using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Extensions
{
    public static class DataRowExtensions
    {
        public static bool ContentEquals(this DataRow dataRow, DataRow thatDataRow)
        {
            bool result = false;

            if (((dataRow == null) && (thatDataRow == null)) || ReferenceEquals(dataRow, thatDataRow))
            {
                result = true;
            }
            else if ((dataRow != null) && (thatDataRow != null))
            {
                object[] thisDataRowItemArray = dataRow.ItemArray;
                object[] thatDataRowItemArray = thatDataRow.ItemArray;
                Func<object, string> stringConverterFunctionPredicate = item => item.ToString();

                result = (thisDataRowItemArray.Length == thatDataRowItemArray.Length)
                            && thisDataRowItemArray.Select(stringConverterFunctionPredicate)
                                .SequenceEqual(thatDataRowItemArray.Select(stringConverterFunctionPredicate));
            }

            return result;
        }

        public static bool TryAddItem(this IEnumerable<DataRow> inputCollection, DataRow itemToAdd, out ICollection<DataRow> outputCollection)
        {
            bool result = false;
            outputCollection = null;

            if (inputCollection.All(item => !item.ContentEquals(itemToAdd)))
            {
                inputCollection = inputCollection.Concat(new[] { itemToAdd });

                outputCollection = inputCollection.ToArray();
                result = true;
            }

            return result;
        }

        public static IEnumerable<DataRow> Distinct(this IEnumerable<DataRow> dataRowCollection, DataTable schemaTable, byte distinctIndex)
        {
            ICollection<object[]> objectArrayCollection = new Collection<object[]>();

            foreach (DataRow dataRow in dataRowCollection)
            {
                if (!objectArrayCollection.Any())
                {
                    objectArrayCollection.Add(dataRow.ItemArray);
                }
                else
                {
                    if (objectArrayCollection.All(
                        distinctDataRowArray =>
                        {
                            object dateColumnInDataRow = dataRow.ItemArray[distinctIndex];
                            object distinctDateColumnInCollection = distinctDataRowArray[distinctIndex];

                            return (dataRow.ItemArray.Length == distinctDataRowArray.Length)
                                   && ((dateColumnInDataRow != null) && (distinctDateColumnInCollection != null)
                                       && !dataRow.ItemArray.SequenceEqual(distinctDataRowArray));
                        }))
                    {
                        objectArrayCollection.Add(dataRow.ItemArray);
                    }
                }
            }

            foreach (object[] objectArray in objectArrayCollection)
            {
                DataRow newRow = schemaTable.NewRow();
                newRow.ItemArray = objectArray;
                yield return newRow;
            }
        }
    }
}
