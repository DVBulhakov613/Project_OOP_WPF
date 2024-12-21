using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_OOP_WPF
{
    public class IDManagement
    {
        public IDManagement() { }
        private int nextID = 0;
        private Queue<int> recycledID = new Queue<int>();

        public int GenerateID() 
        {
            if (recycledID.Count == 0)
                return nextID++;
            else return recycledID.Dequeue();
        }
        public void RecycleID(int id) 
        {
            if (id > 0 && id < nextID)
                recycledID.Enqueue(id);
            else throw new ArgumentException("Invalid ID for recycling!");
        }
    }
}
