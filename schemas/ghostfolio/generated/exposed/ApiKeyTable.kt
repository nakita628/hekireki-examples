package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ApiKeyTable : IdTable<String>("\"ApiKey\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val hashedKey = text("hashedKey").uniqueIndex("\"ApiKey_hashedKey_key\"")
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ApiKey_pkey\"")

    init {
        index("\"ApiKey_userId_idx\"", false, userId)
    }
}
