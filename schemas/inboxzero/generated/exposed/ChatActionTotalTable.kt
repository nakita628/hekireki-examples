package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatActionTotalTable : IdTable<String>("\"ChatActionTotal\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val action = text("action")
    val count = integer("count").default(0)
    val chatId =
        reference(
            "chatId",
            ChatTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ChatActionTotal_chatId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ChatActionTotal_pkey\"")

    init {
        uniqueIndex("\"ChatActionTotal_chatId_action_key\"", chatId, action)
    }
}
