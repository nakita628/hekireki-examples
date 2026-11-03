package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AutoPostTable : IdTable<String>("\"AutoPost\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AutoPost_organizationId_fkey\"",
        )
    val title = text("title")
    val content = text("content").nullable()
    val onSlot = bool("onSlot")
    val syncLast = bool("syncLast")
    val url = text("url")
    val lastUrl = text("lastUrl")
    val active = bool("active")
    val addPicture = bool("addPicture")
    val generateContent = bool("generateContent")
    val integrations = text("integrations")
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AutoPost_pkey\"")

    init {
        index("\"AutoPost_deletedAt_idx\"", false, deletedAt)
    }
}
