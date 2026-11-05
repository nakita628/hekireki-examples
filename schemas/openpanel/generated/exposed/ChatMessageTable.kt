package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatMessageTable : IdTable<String>("chat_messages") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"chat_messages_conversationId_fkey\"",
        )
    val role = text("role")
    val parts = json<String>("parts", { it }, { it })
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "chat_messages_pkey")

    init {
        index("\"chat_messages_conversationId_createdAt_idx\"", false, conversationId, createdAt)
    }
}
