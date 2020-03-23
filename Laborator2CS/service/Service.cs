using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tasks.repository;
using Model;

namespace service
{
    public class Service 
    {
        ICrudRepository<int, Participant> _participantDbRepository;
        ICrudRepository<int, Proba> _probaDbRepository;
        ICrudRepository<int, Organizator> _organizatorDbRepository;
        ICrudRepository<int, Inscriere> _inscriereDbRepository;

        public Service(ICrudRepository<int, Participant> participantDbRepository, ICrudRepository<int, Proba> probaDbRepository, ICrudRepository<int, Organizator> organizatorDbRepository, ICrudRepository<int, Inscriere> inscriereDbRepository)
        {
            _participantDbRepository = participantDbRepository;
            _probaDbRepository = probaDbRepository;
            _organizatorDbRepository = organizatorDbRepository;
            _inscriereDbRepository = inscriereDbRepository;
        }

        public IEnumerable<Participant> findAllInscrisi(int idProba)
        {
            List<Participant> list = new List<Participant>();
            foreach (Inscriere inscriere in _inscriereDbRepository.findAll())
            {
                if( inscriere.IdProba == idProba)
                {
                    list.Add(_participantDbRepository.findOne(inscriere.IdParticipant));
                }
            }
            return list;
        }
        public IEnumerable<Proba> findAllProbeInscris(int idParticipant)
        {
            List<Proba> list = new List<Proba>();
            foreach(Inscriere inscriere in _inscriereDbRepository.findAll())
            {
                if(inscriere.IdParticipant == idParticipant)
                {
                    list.Add(_probaDbRepository.findOne(inscriere.IdProba));
                }
            }
            return list;
        }
        public Participant addParticipant(string nume, int varsta)
        {
            if(findParticipant(nume,varsta)!= null)
                return null;
            try
            {
                Participant p = new Participant(_participantDbRepository.findAll().Count() + 1, nume, varsta);
                _participantDbRepository.save(p);
                return p;
            }catch(Exception ignore) {
                return null;
            }
        }
        public Inscriere addInscriere(string nume, int varsta, int idProba)
        {
            Participant p = findParticipant(nume, varsta);
            Inscriere inscriere = new Inscriere(_inscriereDbRepository.findAll().Count() + 1, p.IdParticipant, idProba);
            if(findInscriere(p.IdParticipant,idProba)!= null)
            {
                return null;
            }
            _inscriereDbRepository.save(inscriere);
            foreach(Proba proba in _probaDbRepository.findAll())
            {
                if(proba.IdProba == idProba)
                {
                    _probaDbRepository.update(idProba, new Proba(idProba, proba.Lungime, proba.Stil, proba.NrParticipanti + 1));
                }
            }
            return inscriere;
        }

        public Inscriere findInscriere(int idParticipant, int idProba)
        {
            foreach(var inscriere in _inscriereDbRepository.findAll())
            {
                if(inscriere.IdParticipant == idParticipant && inscriere.IdProba == idProba)
                {
                    return inscriere;
                }
            }
            return null;
        }
        public Participant findParticipant(string nume, int varsta)
        {
            foreach(var p in _participantDbRepository.findAll())
            {
                if(p.Nume == nume && p.Varsta==varsta)
                {
                    return p;
                }
            }
            return null;
        }

        public bool validateLogin(string username, string password)
        {
            foreach(Organizator organizator in _organizatorDbRepository.findAll())
            {
                if (organizator.Username.Equals(username) && organizator.Password.Equals(password))
                    return true;
            }
            return false;
        }
        public IEnumerable<Proba> GetAllProba()
        {
            return _probaDbRepository.findAll();
        }
        public IEnumerable<Participant> GetAllParticipant()
        {
            return _participantDbRepository.findAll();
        }
        public IEnumerable<Inscriere> GetAllInscriere()
        {
            return _inscriereDbRepository.findAll();
        }
        public IEnumerable<Organizator> GetAllOrganizator()
        {
            return _organizatorDbRepository.findAll();
        }
    }
}
