using CsvHelper;
using ServerSocketChat.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace ServerSocketChat.Helpers
{
	public class CsvHandler
	{
		public static IEnumerable<ChatInfoParameters> ReadCsvFile<T>(Stream stream)
		{
			using (StreamReader csvStream = new StreamReader(stream))
			{
				using (var csv = new CsvReader(csvStream, CultureInfo.CurrentCulture))
				{
					csv.Configuration.HasHeaderRecord = false;

					while (csv.Read())
					{
						ChatInfoParameters value;
						for (int i = 0; csv.TryGetField<ChatInfoParameters>(i, out value); i++)
						{
							if (value != null)
							{
								yield return value;
							}
						}
					}
				}
			}
		}

	}
}
