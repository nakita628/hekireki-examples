package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DepartmentTable : IdTable<String>("\"Department\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val name = text("name")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Department_pkey\"")

    init {
        index("\"Department_organizationId_idx\"", false, organizationId)
    }
}
