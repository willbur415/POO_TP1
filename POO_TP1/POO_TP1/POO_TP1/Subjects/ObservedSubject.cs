using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POO_TP1
{
    public abstract class ObservedSubject
    {
        protected List<Observer> observers; 
        public ObservedSubject()
        {
            observers = new List<Observer>();
        }

        /// <summary>
        /// Adds the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void AddObserver(Observer observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        /// <summary>
        /// Removes the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void RemoveObserver(Observer observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        /// <summary>
        /// Notifies all observers.
        /// </summary>
        protected void NotifyAllObservers()
        {
            foreach (Observer o in observers)
            {
                o.Notify(this);
            }
        }
    }
}
