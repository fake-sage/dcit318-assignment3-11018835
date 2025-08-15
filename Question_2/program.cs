using System;
using System.Collections.Generic;
using System.Linq;

// a) Generic repository
public class Repository<T>
{
    private readonly List<T> items = new();

        public void Add(T item) => items.Add(item);
            public List<T> GetAll() => new(items);
                public T? GetById(Func<T, bool> predicate) => items.FirstOrDefault(predicate);
                    public bool Remove(Func<T, bool> predicate)
                        {
                                var toRemove = items.FirstOrDefault(predicate);
                                        return toRemove is not null && items.Remove(toRemove);
                                            }
                                            }

                                            // b) Patient
                                            public class Patient
                                            {
                                                public int Id;
                                                    public string Name;
                                                        public int Age;
                                                            public string Gender;

                                                                public Patient(int id, string name, int age, string gender)
                                                                    {
                                                                            Id = id; Name = name; Age = age; Gender = gender;
                                                                                }

                                                                                    public override string ToString() => $"Patient #{Id}: {Name}, {Age}, {Gender}";
                                                                                    }

                                                                                    // c) Prescription
                                                                                    public class Prescription
                                                                                    {
                                                                                        public int Id;
                                                                                            public int PatientId;
                                                                                                public string MedicationName;
                                                                                                    public DateTime DateIssued;

                                                                                                        public Prescription(int id, int patientId, string med, DateTime dateIssued)
                                                                                                            {
                                                                                                                    Id = id; PatientId = patientId; MedicationName = med; DateIssued = dateIssued;
                                                                                                                        }

                                                                                                                            public override string ToString() => $"Rx #{Id} for Patient {PatientId}: {MedicationName} on {DateIssued:d}";
                                                                                                                            }

                                                                                                                            public class HealthSystemApp
                                                                                                                            {
                                                                                                                                private readonly Repository<Patient> _patientRepo = new();
                                                                                                                                    private readonly Repository<Prescription> _prescriptionRepo = new();
                                                                                                                                        private Dictionary<int, List<Prescription>> _prescriptionMap = new();

                                                                                                                                            // d/e/f) Methods
                                                                                                                                                public void SeedData()
                                                                                                                                                    {
                                                                                                                                                            _patientRepo.Add(new Patient(1, "Alice Smith", 29, "F"));
                                                                                                                                                                    _patientRepo.Add(new Patient(2, "Baba Mensah", 42, "M"));
                                                                                                                                                                            _patientRepo.Add(new Patient(3, "Kwesi Boateng", 33, "M"));

                                                                                                                                                                                    _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin 500mg", DateTime.Today.AddDays(-10)));
                                                                                                                                                                                            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen 200mg", DateTime.Today.AddDays(-5)));
                                                                                                                                                                                                    _prescriptionRepo.Add(new Prescription(3, 2, "Loratadine 10mg", DateTime.Today.AddDays(-1)));
                                                                                                                                                                                                            _prescriptionRepo.Add(new Prescription(4, 3, "Metformin 500mg", DateTime.Today));
                                                                                                                                                                                                                    _prescriptionRepo.Add(new Prescription(5, 2, "Paracetamol 1g", DateTime.Today.AddDays(-2)));
                                                                                                                                                                                                                        }

                                                                                                                                                                                                                            public void BuildPrescriptionMap()
                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                        _prescriptionMap = _prescriptionRepo
                                                                                                                                                                                                                                                    .GetAll()
                                                                                                                                                                                                                                                                .GroupBy(p => p.PatientId)
                                                                                                                                                                                                                                                                            .ToDictionary(g => g.Key, g => g.ToList());
                                                                                                                                                                                                                                                                                }

                                                                                                                                                                                                                                                                                    public void PrintAllPatients()
                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                Console.WriteLine("== All Patients ==");
                                                                                                                                                                                                                                                                                                        foreach (var p in _patientRepo.GetAll())
                                                                                                                                                                                                                                                                                                                    Console.WriteLine(p);
                                                                                                                                                                                                                                                                                                                        }

                                                                                                                                                                                                                                                                                                                            public List<Prescription> GetPrescriptionsByPatientId(int patientId)
                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                        return _prescriptionMap.TryGetValue(patientId, out var list) ? list : new List<Prescription>();
                                                                                                                                                                                                                                                                                                                                            }

                                                                                                                                                                                                                                                                                                                                                public void PrintPrescriptionsForPatient(int id)
                                                                                                                                                                                                                                                                                                                                                    {
                                                                                                                                                                                                                                                                                                                                                            Console.WriteLine($"\n== Prescriptions for Patient #{id} ==");
                                                                                                                                                                                                                                                                                                                                                                    foreach (var rx in GetPrescriptionsByPatientId(id))
                                                                                                                                                                                                                                                                                                                                                                                Console.WriteLine(rx);
                                                                                                                                                                                                                                                                                                                                                                                    }

                                                                                                                                                                                                                                                                                                                                                                                        public void Run()
                                                                                                                                                                                                                                                                                                                                                                                            {
                                                                                                                                                                                                                                                                                                                                                                                                    SeedData();
                                                                                                                                                                                                                                                                                                                                                                                                            BuildPrescriptionMap();
                                                                                                                                                                                                                                                                                                                                                                                                                    PrintAllPatients();
                                                                                                                                                                                                                                                                                                                                                                                                                            PrintPrescriptionsForPatient(2); // Example: show patient with ID 2
                                                                                                                                                                                                                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                                                                                                                                                                                                                }

                                                                                                                                                                                                                                                                                                                                                                                                                                public class Program
                                                                                                                                                                                                                                                                                                                                                                                                                                {
                                                                                                                                                                                                                                                                                                                                                                                                                                    public static void Main()
                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                new HealthSystemApp().Run();
                                                                                                                                                                                                                                                                                                                                                                                                                                                    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                    }