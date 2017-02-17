using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication41
{
    public enum onetwo { onesize = 1, twosize = 2 }
    public abstract class Storage
    {
        String name;
        String model;
        bool flag = true;
        int speed;
        int busy;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public int Busy
        {
            get
            {
                return busy;
            }
            set
            {
                busy = value;
            }
        }
        public virtual void GetInfo()
        {
            Console.WriteLine(name + "  ");
            Console.WriteLine(model + '\n');
        }
        public void ChangeFlag()
        {
            flag = !flag;
        }
        public bool Flag
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }
      
        public abstract int GetSpace();
        public abstract int GetFreeSpace();
        public abstract double Copy(int n);
        public abstract double Write(int n);
        
        public void SetName(String str)
        {
            name = str;
        }
        public void SetModel(String str)
        {
            model = str;
        }
        public Storage(String tname, String tmodel)
        {
            name = tname;
            model = tmodel;
        }
        public Storage()
        {
            name = "unknown";
            model = "unknown";
        }
    }

    public class Flash : Storage
    {
        int storage;
       
        public override void GetInfo()
        {
            base.GetInfo();
            Console.WriteLine("storage = " + storage + "  speed(mbpersec) = " + base.Speed + '\n');
        }
        public Flash()
        {
            storage = 1000;
            base.Speed = 5000;
            GetInfo();
            base.Busy = 0;
        }
        public Flash(String tname, String tmodel, int tstorage)
            : base(tname, tmodel)
        {
            base.Speed = 5000;
            storage = tstorage;
            GetInfo();
            base.Busy = 0;
        }


        public override int GetSpace()
        {
            return storage;
        }

        public override int GetFreeSpace()
        {
            return storage - base.Busy;
        }

        public override double Copy(int n)
        {
            var x = GetFreeSpace();
            if (x < n) return 0;
            else
            {
                base.Busy += n;
                return (double)n / (double)base.Speed;

            }
        }

        public override double Write(int n)
        {
            base.Busy-=n;
            return (double)n / (double)base.Speed;
        }
    }

    public class DVD : Storage
    {
        int storage;
      
        public override void GetInfo()
        {
            base.GetInfo();
            Console.WriteLine("storage = " + storage + "   speed(mbpersec) = " + base.Speed + '\n');
        }
        public DVD()
        {
            storage = 4700;
            base.Speed = 21;
            base.Busy = 0;
            GetInfo();
        }
        public DVD(String tname, String tmodel, onetwo x)
            : base(tname, tmodel)
        {
            base.Speed = 21;
            storage = 4700 * (int)x;
            GetInfo();
           base.Busy = 0;
        }

        public override int GetSpace()
        {
            return storage;
        }

        public override int GetFreeSpace()
        {
            return storage - base.Busy;
        }

        public override double Copy(int n)
        {
            var x = GetFreeSpace();
            if (x < n) return 0;
            else
            {
                base.Busy += n;
                return (double)n / (double)base.Speed;

            }
        }
        public override double Write(int n)
        {
            base.Busy-=n;
            return (double)n / (double)base.Speed;
        }
    }

    public class HDD : Storage
    {
        int section;
        int storage;
      
        public override void GetInfo()
        {
            base.GetInfo();
            Console.WriteLine("sections = " + section + "  storage of sections = " + storage + "   speed(mbpersec) = " + base.Speed + '\n');
        }
        public HDD()
        {
            storage = 1000;
            section = 1;
            base.Speed = 60;
            base.Busy = 0;
            GetInfo();
        }
        public HDD(String tname, String tmodel, int tstorage, int tsection)
            : base(tname, tmodel)
        {
            base.Speed = 60;
            storage = tstorage;
            section = tsection;
            GetInfo();
            base.Busy = 0;
        }
        public override int GetSpace()
        {
            return storage * section;
        }

        public override int GetFreeSpace()
        {
            return storage * section - base.Busy;
        }
        public override double Copy(int n)
        {
            var x = GetFreeSpace();
            if (x < n) return 0;
            else
            {
                base.Busy += n;
                return (double)n / (double)base.Speed;
                
            }
        }
        public override double Write(int n)
        {
            base.Busy -= n;
            return (double)n / (double)base.Speed;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int need = 565000;
            int onefile = 760;
            Console.WriteLine("how many storages you want? ");
            int amount;
            amount = Convert.ToInt32(Console.ReadLine());
            Storage[] stor = new Storage[amount];
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("what is the name of the " + (i + 1) + " storage?\n");
                String tname = Console.ReadLine();
                Console.WriteLine("what is the model of the " + (i + 1) + " storage?\n");
                String tmodel = Console.ReadLine();
                switch (tmodel)
                {
                    case "Flash":
                        Console.WriteLine("enter the storage of flash\n");
                        int tstorage = Convert.ToInt32(Console.ReadLine());
                        Storage st = new Flash(tname, tmodel, tstorage);
                        stor[i] = st;
                        break;
                    case "DVD":
                        Console.WriteLine("enter the amount of size of DVD\n");
                        onetwo ot = (onetwo)int.Parse(Console.ReadLine());
                        Storage st1 = new DVD(tname, tmodel, ot);
                        stor[i] = st1;
                        break;
                    case "HDD":
                        Console.WriteLine("enter the amount of sections of HDD\n");
                        int tsections = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter the size of one section of HDD\n");
                        int tstorage1 = int.Parse(Console.ReadLine());
                        Storage st2 = new HDD(tname, tmodel, tsections, tstorage1);
                        stor[i] = st2;
                        break;
                    default:
                        Console.WriteLine("incorrect input, create FLASH\n");
                        Console.WriteLine("enter the storage of flash\n");
                        int tstorage2 =  Convert.ToInt32(Console.ReadLine());
                        Storage st4 = new Flash(tname, tmodel, tstorage2);
                        stor[i] = st4;
                        break;
                }
            }
            double time = 0;
            int needtocopy=need;
            Console.WriteLine("you need to copy {0} mb", need);
            while(need>0){
                time++;
                for (int i = 0; i < amount; i++)
                {
                    if (needtocopy == 0)
                    {
                        for (int j = 0; j < amount; j++) stor[j].Flag = false;
                        break;
                    }
                    if(stor[i].Flag==true){
                    int free = stor[i].GetFreeSpace();
                        if(free<onefile) {
                            stor[i].ChangeFlag();
                            continue;
                        }
                        if(needtocopy>=stor[i].Speed){
                            if(stor[i].GetFreeSpace()>=stor[i].Speed){
                             int willcopy=stor[i].Speed;
                             stor[i].Copy(willcopy);
                                needtocopy-=willcopy;
                            }
                            else{
                                int willcopy=stor[i].GetFreeSpace();
                                stor[i].Copy(willcopy);
                                needtocopy-=willcopy;
                                stor[i].ChangeFlag();
                            }
                        }
                        else{
                            if(needtocopy>=stor[i].GetFreeSpace()){
                                int willcopy=stor[i].GetFreeSpace();
                                stor[i].Copy(willcopy);
                                needtocopy-=willcopy;
                                stor[i].ChangeFlag();
                            }
                            else{
                                int willcopy=needtocopy;
                                needtocopy=0;
                                stor[i].Copy(willcopy);
                            }
                        }
                       }
                       
                    }
                
                for (int i = 0; i < amount; i++)
                {
                    if (stor[i].Flag == false)
                    {
                        if(stor[i].Busy>stor[i].Speed){
                            int willwrite = stor[i].Speed;
                            stor[i].Write(willwrite);
                            need-=willwrite;
                        }
                        else{
                            int willwrite=stor[i].Busy;
                            stor[i].Write(willwrite);
                            need-=willwrite;
                            stor[i].ChangeFlag();
                        }
                    }
                }
                }
            
            Console.WriteLine("with your storages it will take {0} sec", time);
            
        }
    }
}