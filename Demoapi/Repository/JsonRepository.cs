using System;
using System.Collections.Generic;
using Demoapi.Interface;
using Demoapi.Models;

public class JsonRepository : IJsonRepository
{
    private List<jsondata> jsonDataList = new List<jsondata>();

    public bool JsonExists(Guid JsonId)
    {
        return jsonDataList.Any(j => j.JsonId == JsonId);
    }

    public bool CreateJson(jsondata json)
    {
        jsonDataList.Add(json);
        return true;
    }

    public bool Save()
    {
        // Save changes to the data store (e.g., a database)
        return true;
    }

    public bool DeleteJson(Guid JsonId)
    {
        var jsonToDelete = jsonDataList.FirstOrDefault(j => j.JsonId == JsonId);
        if (jsonToDelete != null)
        {
            jsonDataList.Remove(jsonToDelete);
            return true;
        }
        return false;
    }

    public bool UpdateJson(Guid JsonId)
    {
        // Implement update logic here
        return true;
    }

    public jsondata GetJson(Guid JsonId)
    {
        return jsonDataList.FirstOrDefault(j => j.JsonId == JsonId);
    }

}
