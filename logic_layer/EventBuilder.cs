﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.models;

namespace RPG.logic_layer
{
    public class EventBuilder
    {
        static EventBuilder _instance;
        Location[] _locations;
        EventBase[] _events;
        Buff[] _buffs;
        Item[] _items;

        public Location[] locations { get { return _locations; } }
        
        public EventBase[] events { get { return _events; } }

        public Buff[] buffs { get { return _buffs; } }

        public Item[] items { get { return _items; } }
        private EventBuilder() { }

        public static EventBuilder instance
        {
            get
            {
                if(instance == null)
                {
                    _instance = new EventBuilder();
                }
                return _instance;
            }
        }

        public void setLocations(Location[] locations)
        {
            _locations = locations;
        }

        public void setEvents(EventBase[] events)
        {
            _events = events;
        }

        public void setBuffs(Buff[] buffs)
        {
            _buffs = buffs;
        }

        public Event[] nextEvents()
        {
            Random random = new();
            int num = random.Next(2, 4);
            Event[] res = new Event[num];
            for(int i = 0; i < num; i++)
            {
                while (true)
                {
                    int id = random.Next(_events.Length - 1);
                    bool notFound = true;
                    for (int j = 0; j < i; j++)
                    {
                        if(res[j].id == id)
                        {
                            notFound = false;
                        }
                    }
                    if (notFound)
                    {
                        EventBase curr = _events[id];
                        int idLoc = curr.locations[random.Next(curr.locations.Length - 1)];
                        Location location = _locations.Single(location => location.id == idLoc);
                        if(location == null)
                        {
                            throw new Exception("Nie znaleziono lokacji!");
                        }
                        Choice[] choices = new Choice[curr.choices.Length];
                        for(int j = 0; j < choices.Length; j++)
                        {
                            ChoiceBase tmp = curr.choices[j];
                            Item item = null; //TODO: Filtrowac itemy po klasie
                            if (tmp.item)
                            {
                                item = _items[random.Next(_items.Length - 1)];
                            }
                            Buff buff = _buffs.Single(buff => buff.id == tmp.buff);
                            choices[j] = new Choice(tmp.desc, tmp.chance, tmp.win, tmp.lose, tmp.stats, buff, item);
                        }
                        res[i] = new Event(curr.id, curr.desc, location, choices);
                        break;
                    }
                }
            }
            return res;
        }
    }
}