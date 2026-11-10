package opensaas.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ContactFormMessageTable : IdTable<String>("\"ContactFormMessage\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ContactFormMessage_userId_fkey\"",
        )
    val content = text("content")
    val isRead = bool("isRead").default(false)
    val repliedAt = pgTimestamp("repliedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ContactFormMessage_pkey\"")
}
