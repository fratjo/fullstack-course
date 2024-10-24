namespace Devoir.Repositories.Errors;

public enum SQLServerExceptionCode
{
        // Contraintes d'intégrité
        NotNullViolation = 515, // Violation de contrainte NOT NULL
        CheckConstraintViolation = 547, // Utilisé pour plusieurs types de violations de contraintes, y compris CHECK et Foreign Key
        ForeignKeyViolation = 547, // Violation de contrainte FOREIGN KEY
        UniqueViolation = 2601, // Violation de contrainte UNIQUE
        PrimaryKeyViolation = 2627, // Violation de contrainte PRIMARY KEY

        // Erreurs liées aux données
        MaxLengthExceeded = 2628, // La taille des données dépasse la taille de la colonne
        NumericOverflow = 8115,   // Dépassement de capacité d'un type numérique
        DivideByZero = 8134,      // Division par zéro
        ConversionFailed = 245,   // Erreur de conversion de type de données

        // Erreurs d'index et de verrouillage
        DeadlockVictim = 1205,    // Transaction choisie comme victime d'impasse
        LockTimeout = 1222,       // Timeout sur un verrou
        TimeoutExpired = -2,      // Timeout lors de l'attente d'une réponse (SqlCommand timeout)
        
        // Erreurs de transaction
        TransactionCountMismatch = 266, // Décompte des transactions incorrect
        SnapshotIsolationError = 3960,  // Conflit d'isolation de snapshot
        SerializableConflict = 3961,    // Conflit avec l'isolation Serializable
        
        // Erreurs d'instruction SQL
        SyntaxError = 102,             // Erreur de syntaxe dans l'instruction SQL
        InvalidColumnName = 207,       // Nom de colonne invalide
        InvalidObjectName = 208,       // Nom d'objet (table, vue) invalide
        InvalidProcedure = 2812,       // Procédure stockée ou fonction non trouvée
        InvalidDataType = 243,         // Type de données non valide

        // Erreurs d'authentification et d'autorisation
        LoginFailed = 18456,           // Échec de connexion pour l'utilisateur
        PermissionDenied = 229,        // Accès refusé pour une commande ou un objet

        // Erreurs d'insertion et de mise à jour
        IdentityInsertError = 544,     // Tentative d'insertion dans une colonne Identity
        IdentityRangeError = 548,      // Erreur liée à la gamme d'identité
        TriggerError = 334,            // Erreur déclenchée par un trigger

        // Erreurs de fichiers et de stockage
        DiskFull = 1105,               // Le disque est plein
        FileNotFound = 17204,          // Fichier non trouvé
        DatabaseCorruption = 824,      // Corruption de page détectée

        // Erreurs réseau
        NetworkError = 53,             // Erreur réseau ou serveur inaccessible
        ConnectionTimeout = 258,       // Timeout lors de la tentative de connexion

        // Erreurs système
        OutOfMemory = 701,             // Mémoire insuffisante pour exécuter la requête
        InsufficientResources = 8651,  // Ressources système insuffisantes
        InternalError = 3621           // Erreur interne générale
}