package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MessagesTable : IdTable<String>("\"Messages\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val from = pgEnum("from", "\"From\"", From.entries, From::dbName)
    val content = text("content").nullable()
    val groupId =
        reference(
            "groupId",
            MessagesGroupTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Messages_groupId_fkey\"",
        )
    val special = text("special").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Messages_pkey\"")

    init {
        index("\"Messages_groupId_idx\"", false, groupId)
        index("\"Messages_createdAt_idx\"", false, createdAt)
        index("\"Messages_deletedAt_idx\"", false, deletedAt)
    }
}
