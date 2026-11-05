package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CleanupJobTable : IdTable<String>("\"CleanupJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val action = pgEnum("action", "\"CleanAction\"", CleanAction.entries, CleanAction::dbName).default(CleanAction.ARCHIVE)
    val daysOld = integer("daysOld").default(7)
    val instructions = text("instructions").nullable()
    val skipReply = bool("skipReply").nullable()
    val skipStarred = bool("skipStarred").nullable()
    val skipCalendar = bool("skipCalendar").nullable()
    val skipReceipt = bool("skipReceipt").nullable()
    val skipAttachment = bool("skipAttachment").nullable()
    val skipConversation = bool("skipConversation").nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CleanupJob_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"CleanupJob_pkey\"")

    init {
        index("\"CleanupJob_emailAccountId_idx\"", false, emailAccountId)
    }
}
