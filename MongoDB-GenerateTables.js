/* global use, db */
// MongoDB Playground
// To disable this template go to Settings | MongoDB | Use Default Template For Playground.
// Make sure you are connected to enable completions and to be able to run a playground.
// Use Ctrl+Space inside a snippet or a string literal to trigger completions.
// The result of the last command run in a playground is shown on the results panel.
// By default the first 20 documents will be returned with a cursor.
// Use 'console.log()' to print to the debug output.
// For more documentation on playgrounds please refer to
// https://www.mongodb.com/docs/mongodb-vscode/playgrounds/

// Select the database to use.
use('mongodbVSCodePlaygroundDB');

db.createCollection("cliente", {
    validator: {
      $jsonSchema: {
        bsonType: "object",
        required: ["Correo", "Nombre", "Apellido1", "Contrasena", "Pais", "Fecha_registro", "Fecha_nacimiento"],
        properties: {
          Correo: { bsonType: "string", description: "Correo electrónico del cliente" },
          Nombre: { bsonType: "string", description: "Nombre del cliente" },
          Apellido1: { bsonType: "string", description: "Primer apellido del cliente" },
          Apellido2: { bsonType: ["string", "null"], description: "Segundo apellido del cliente" },
          Contrasena: { bsonType: "string", description: "Contraseña del cliente" },
          Pais: { bsonType: "string", description: "País del cliente" },
          Fecha_registro: { bsonType: "string", description: "Fecha de registro del cliente" },
          Fecha_nacimiento: { bsonType: "string", description: "Fecha de nacimiento del cliente" },
          Estatura: { bsonType: ["int", "null"], description: "Estatura del cliente" },
          Peso: { bsonType: ["int", "null"], description: "Peso del cliente" }
        }
      }
    }
  })
  