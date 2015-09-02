using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{
    class ObservedSubject
    {
        protected List<Observer> observers; 
        public ObservedSubject()
        {
            observers = new List<Observer>();
        }

        public void AddObserver(Observer observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        public void RemoveObserver(Observer observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        protected void NotifyAllObservers()
        {
            foreach (Observer o in observers)
            {
                o.Notify(this);
            }
        }
    }
}
