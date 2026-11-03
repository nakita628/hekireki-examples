package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ErrorsTable : IdTable<String>("\"Errors\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val message = text("message")
    val platform = text("platform")
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Errors_organizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val postId =
        reference(
            "postId",
            PostTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Errors_postId_fkey\"",
        )
    val body = text("body").default("{}")
    override val primaryKey = PrimaryKey(id, name = "\"Errors_pkey\"")

    init {
        index("\"Errors_organizationId_idx\"", false, organizationId)
        index("\"Errors_createdAt_idx\"", false, createdAt)
    }
}
