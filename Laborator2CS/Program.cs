using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using tasks.repository;
using Model;
using GUI;
using service;

namespace Laborator2CS
{
    //static class Program
    //{
    //    /// <summary>
    //    /// The main entry point for the application.
    //    /// </summary>
    //    [STAThread]
    //    static void Main(string[] args)
    //    {
    //        Application.EnableVisualStyles();
    //        Application.SetCompatibleTextRenderingDefault(false);
    //        Application.Run(new Form1());

    //        //Console.WriteLine ("Hello World!");
    //        XmlConfigurator.Configure(new System.IO.FileInfo(args[0]));

    //        Console.WriteLine("Sorting Swim Repository DB ...");
    //        ParticipantDbRepository repoParticipant = new ParticipantDbRepository();

    //        // repoParticipant.save(new Participant(2,"sorin",50));
    //        // repoParticipant.delete(2);

    //        Console.WriteLine("Participantii din db");
    //        foreach (Participant t in repoParticipant.findAll())
    //        {
    //            Console.WriteLine(t);
    //        }

    //        Console.WriteLine("Sorting Swim Repository DB ...");
    //        ProbaDbRepository repoProba = new ProbaDbRepository();

    //        // repoProba.save(new Proba(2,50,"liber",0));
    //        // repoProba.delete(2);

    //        Console.WriteLine("Participantii din db");
    //        foreach (Proba t in repoProba.findAll())
    //        {
    //            Console.WriteLine(t);
    //        }
    //        Participant participant = repoParticipant.findOne(1);
    //        repoParticipant.delete(1);
    //        participant.Nume = "Boss";
    //        repoParticipant.save(participant);

    //        Console.WriteLine("Taskurile din db dupa stergere/adaugare");
    //        foreach (Participant t in repoParticipant.findAll())
    //        {
    //            Console.WriteLine(t);
    //        }
    //        Console.ReadLine();
    //    }
    //}

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(GetInstance());
        }

        static MainView GetInstance()
        {
            var participantRepository = new ParticipantDbRepository();
            var probaRepository = new ProbaDbRepository();
            var organizatorRepository = new OrganizatorDbRepository();
            var inscriereRepository = new InscriereDbRepository();

            var service = new Service(participantRepository, probaRepository, organizatorRepository, inscriereRepository);
            return new MainView(service);
        }
    }
}
