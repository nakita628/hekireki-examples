package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SecretReferenceTable : IdTable<String>("\"SecretReference\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val key = text("key").uniqueIndex("\"SecretReference_key_key\"")
    val provider =
        pgEnum("provider", "\"SecretStoreProvider\"", SecretStoreProvider.entries, SecretStoreProvider::dbName).default(
            SecretStoreProvider.DATABASE,
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SecretReference_pkey\"")
}
