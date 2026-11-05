package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChatMemoryTable : IdTable<String>("\"ChatMemory\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val content = text("content")
    val chatId =
        optReference(
            "chatId",
            ChatTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ChatMemory_chatId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ChatMemory_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ChatMemory_pkey\"")

    init {
        index("\"ChatMemory_emailAccountId_idx\"", false, emailAccountId)
    }
}
