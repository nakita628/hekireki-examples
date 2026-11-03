package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TagsTable : IdTable<String>("\"Tags\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val color = text("color")
    val orgId =
        reference(
            "orgId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Tags_orgId_fkey\"",
        )
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Tags_pkey\"")

    init {
        index("\"Tags_orgId_idx\"", false, orgId)
        index("\"Tags_deletedAt_idx\"", false, deletedAt)
    }
}
