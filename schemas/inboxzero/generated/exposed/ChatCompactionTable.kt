package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatCompactionTable : IdTable<String>("\"ChatCompaction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val summary = text("summary")
    val messageCount = integer("messageCount")
    val compactedBeforeCreatedAt = pgTimestamp("compactedBeforeCreatedAt", 3)
    val chatId =
        reference(
            "chatId",
            ChatTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ChatCompaction_chatId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ChatCompaction_pkey\"")

    init {
        index("\"ChatCompaction_chatId_idx\"", false, chatId)
    }
}
