package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SignaturesTable : IdTable<String>("\"Signatures\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Signatures_organizationId_fkey\"",
        )
    val content = text("content")
    val autoAdd = bool("autoAdd")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Signatures_pkey\"")

    init {
        index("\"Signatures_createdAt_idx\"", false, createdAt)
        index("\"Signatures_organizationId_idx\"", false, organizationId)
        index("\"Signatures_deletedAt_idx\"", false, deletedAt)
    }
}
