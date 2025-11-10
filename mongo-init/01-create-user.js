db = db.getSiblingDB("admin");

// cria usuário root para o Mongo
db.createUser({
  user: process.env.MONGO_INITDB_ROOT_USERNAME || "admin",
  pwd: process.env.MONGO_INITDB_ROOT_PASSWORD || "SenhaDevLocal123!",
  roles: [{ role: "root", db: "admin" }]
});

// cria base e coleção de exemplo
db = db.getSiblingDB(process.env.MONGO_INITDB_DATABASE || "PingMottuDB");
db.createCollection("exemplo");
db.exemplo.insertOne({ seededAt: new Date(), note: "seed inicial" });
