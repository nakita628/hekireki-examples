package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectSecretTable : IdTable<String>("\"ProjectSecret\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val encryptedValue = text("encryptedValue")
    val createdById = text("createdById")
    val updatedById = text("updatedById")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ProjectSecret_pkey\"")

    init {
        index("\"ProjectSecret_projectId_idx\"", false, projectId)
        uniqueIndex("\"ProjectSecret_projectId_name_key\"", projectId, name)
    }
}
