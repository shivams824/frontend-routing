using System.ComponentModel.DataAnnotations;

namespace Demoapi.Models
{
    public class SequentialIdService
    {
        private int _currentId = 0;

        public int GetNextId()
        {
            // Increment the current ID for the next sequential ID
            return ++_currentId;
        }
    }


}

