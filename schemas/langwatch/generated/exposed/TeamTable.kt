package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamTable : IdTable<String>("\"Team\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val slug = text("slug").uniqueIndex("\"Team_slug_key\"")
    val organizationId = text("organizationId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val isPersonal = bool("isPersonal").default(false)
    val ownerUserId = varchar("ownerUserId", 255).nullable()
    val departmentId = text("departmentId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Team_pkey\"")

    init {
        index("\"Team_organizationId_idx\"", false, organizationId)
        index("\"Team_isPersonal_idx\"", false, isPersonal)
        index("\"Team_departmentId_idx\"", false, departmentId)
    }
}
