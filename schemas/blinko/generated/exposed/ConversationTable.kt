package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationTable : IdTable<Int>("conversation") {
    override val id = integer("id").autoIncrement().entityId()
    val title = varchar("title", 255).default("")
    val isShare = bool("isShare").default(false)
    val accountId =
        reference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"conversation_accountId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "conversation_pkey")

    init {
        index("\"conversation_accountId_idx\"", false, accountId)
    }
}
