package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatTable : IdTable<String>("\"Chat\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val name = text("name").nullable()
    val compactionCount = integer("compactionCount").default(0)
    val lastSeenRulesRevision = integer("lastSeenRulesRevision").nullable()
    val activeStreamId = text("activeStreamId").nullable()
    val activeStreamStartedAt = pgTimestamp("activeStreamStartedAt", 3).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chat_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Chat_pkey\"")

    init {
        index("\"Chat_emailAccountId_idx\"", false, emailAccountId)
        index("\"Chat_deletedAt_idx\"", false, deletedAt)
    }
}
