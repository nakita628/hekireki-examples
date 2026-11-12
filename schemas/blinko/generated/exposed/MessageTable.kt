package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object MessageTable : IdTable<Int>("message") {
    override val id = integer("id").autoIncrement().entityId()
    val content = text("content")
    val role = varchar("role", 50)
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"message_conversationId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val metadata = json<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "message_pkey")

    init {
        index("\"message_conversationId_idx\"", false, conversationId)
    }
}
