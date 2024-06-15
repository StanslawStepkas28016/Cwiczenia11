/*
{
  "login": "admin",
  "password": "admin"
}
*/

INSERT INTO Doctor
VALUES (1, 'Mary', 'Jane', 'maryjane@email.com')

SELECT *
FROM Doctor

INSERT INTO Medicament
VALUES ('Aspirin', 'Take only two pills a day!', 'Light')

SELECT *
FROM Medicament

/*
{
  "patientDto": {
    "idPatient": 2,
    "firstName": "Jan",
    "lastName": "Kowalski",
    "birthdate": "2000-06-01T11:14:14.983Z"
  },
  "medicamentsDto": [
    {
      "idMedicament": 1,
      "name": "Aspirin",
      "description": "Take only two pills a day!",
      "type": "Light medicine"
    }
  ],
  "date": "2024-06-01T11:14:14.983Z",
  "dueDate": "2024-07-01T11:14:14.983Z"
}
*/

SELECT *
FROM AppUser

SELECT *
FROM Patient

SELECT *
FROM Prescription