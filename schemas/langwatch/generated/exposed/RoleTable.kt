package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoleTable : IdTable<String>("\"Role\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val description = text("description").nullable()
    val permissions = jsonb<String>("permissions", { it }, { it })
    val kind = text("kind").default("custom")
    val occurredAt = pgTimestamp("occurredAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Role_pkey\"")

    init {
        index("\"Role_organizationId_idx\"", false, organizationId)
    }
}
