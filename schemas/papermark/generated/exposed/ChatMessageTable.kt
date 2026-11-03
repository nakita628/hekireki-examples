package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatMessageTable : IdTable<String>("\"ChatMessage\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val chatId =
        reference(
            "chatId",
            ChatTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ChatMessage_chatId_fkey\"",
        )
    val role = text("role")
    val content = text("content")
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ChatMessage_pkey\"")

    init {
        index("\"ChatMessage_chatId_idx\"", false, chatId)
        index("\"ChatMessage_chatId_createdAt_idx\"", false, chatId, createdAt)
    }
}
