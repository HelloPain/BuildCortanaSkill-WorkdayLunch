using System.Data.SqlClient;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot_Application.API;

abstract class Database_Zomato
{
    /*all these operations are based on assumptions that:
     *  ID won't be deleted
     *  each ID's record won't be changed
     *return value is influenced number of lines
     */
    static async public Task<int> UpdateTableCategoriesAsync(SqlConnection connection, ZomatoClient zomatoClient)
    {
        Task<ZomatoClient.CategoriesJson> newData = zomatoClient.GetCategoriesAsync();
        SortedList<UInt16, UInt16> dataInTable = new SortedList<UInt16, UInt16>();//second UInt16 is useless
        using (SqlCommand getData = new SqlCommand("select id from dbo.Categories", connection))
        {
            using (SqlDataReader reader = getData.ExecuteReader())
            {
                while (reader.Read()) dataInTable.Add((UInt16)reader[0], 0);
            }
        }

        ZomatoClient.CategoriesJson new_categories = await newData;
        Array.Sort(new_categories.categories);
        List<int> insertionIndex = new List<int>();
        for (int i = 0; i < new_categories.categories.Length; ++i)
        {
            if (!dataInTable.ContainsKey(new_categories.categories[i].categories.id)) insertionIndex.Add(i);
        }

        StringBuilder insertionCommand = new StringBuilder("insert into dbo.Categories(id,name) values");
        for (UInt16 i = 0; i < insertionIndex.Count - 1; ++i)
        {
            insertionCommand.Append('(');
            insertionCommand.Append(new_categories.categories[insertionIndex[i]].categories.id);
            insertionCommand.Append(",\'" + new_categories.categories[insertionIndex[i]].categories.name);
            insertionCommand.Append("\'),");
        }
        insertionCommand.Append('(');
        insertionCommand.Append(new_categories.categories[insertionIndex[insertionIndex.Count - 1]].categories.id);
        insertionCommand.Append(",\'" + new_categories.categories[insertionIndex[insertionIndex.Count - 1]].categories.name);
        insertionCommand.Append("\')");
        int result;
        using (SqlCommand insertData = new SqlCommand(insertionCommand.ToString(), connection))
        {
            result = insertData.ExecuteNonQuery();
        }
        return result;
    }
    /*
     * only update not exist cities info
     */
    static async public Task<int> AddCitiesToTableAsync(SqlConnection connection, ZomatoClient zomatoClient, UInt32[] cityIDs)
    {
        int result = 0;
        if (cityIDs.Length > 0)
        {
            //check for exist cities in current database
            SortedList<UInt32, UInt16> citiyIDsInTable = new SortedList<UInt32, UInt16>();//second UInt16 is useless
            using (SqlCommand getData = new SqlCommand("select id from dbo.Cities", connection))
            {
                using (SqlDataReader reader = getData.ExecuteReader())
                {
                    while (reader.Read()) citiyIDsInTable.Add(Convert.ToUInt32(reader[0]), 0);
                }
            }

            StringBuilder insertionCommandString = new StringBuilder("insert into dbo.Cities(id,name,country_id,country_name,is_state,state_id,state_name,state_code) values");
            List<UInt32> requestIDs = new List<UInt32>();
            foreach (var id in cityIDs)
                if (!citiyIDsInTable.ContainsKey(id)) requestIDs.Add(id);
            ZomatoClient.CitiesJson newData = await zomatoClient.GetCitiesAsync(requestIDs.ToArray());
            if (newData.location_suggestions.Length > 0)
            {
                foreach (var newCity in newData.location_suggestions)
                {

                    insertionCommandString.Append('(');
                    insertionCommandString.Append(newCity.id); insertionCommandString.Append(",'");
                    insertionCommandString.Append(newCity.name); insertionCommandString.Append("',");
                    insertionCommandString.Append(newCity.country_id); insertionCommandString.Append(",'");
                    insertionCommandString.Append(newCity.country_name); insertionCommandString.Append("','");
                    if (newCity.is_state) insertionCommandString.Append('0');
                    else insertionCommandString.Append('1');
                    insertionCommandString.Append("',");
                    insertionCommandString.Append(newCity.state_id); insertionCommandString.Append(",'");
                    insertionCommandString.Append(newCity.state_name); insertionCommandString.Append("','");
                    insertionCommandString.Append(newCity.state_code);
                    insertionCommandString.Append("'),");
                }
                insertionCommandString.Remove(insertionCommandString.Length - 1, 1);

                using (SqlCommand insertionCommand = new SqlCommand(insertionCommandString.ToString(), connection))
                {
                    result = insertionCommand.ExecuteNonQuery();
                }
            }
        }
        return result;
    }
}
