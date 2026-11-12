package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EnvironmentEncryptionKeyTable : IdTable<String>("\"EnvironmentEncryptionKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EnvironmentEncryptionKey_environmentId_fkey\"",
        )
    val wrappedKey = jsonb<String>("wrappedKey", { it }, { it })
    val wrappingProvider =
        pgEnum(
            "wrappingProvider",
            "\"EnvironmentKeyWrappingProvider\"",
            EnvironmentKeyWrappingProvider.entries,
            EnvironmentKeyWrappingProvider::dbName,
        )
    val wrappingKeyReference = text("wrappingKeyReference")
    val revision = integer("revision").default(0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"EnvironmentEncryptionKey_pkey\"")

    init {
        index("\"EnvironmentEncryptionKey_environmentId_idx\"", false, environmentId)
        uniqueIndex("\"EnvironmentEncryptionKey_id_environmentId_key\"", id, environmentId)
    }
}
