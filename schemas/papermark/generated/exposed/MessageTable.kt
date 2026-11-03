package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MessageTable : IdTable<String>("\"Message\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val content = text("content")
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Message_conversationId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Message_userId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Message_viewerId_fkey\"",
        )
    val viewId =
        optReference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Message_viewId_fkey\"",
        )
    val isRead = bool("isRead").default(false)
    val isInternalNote = bool("isInternalNote").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Message_pkey\"")

    init {
        index("\"Message_conversationId_idx\"", false, conversationId)
        index("\"Message_userId_idx\"", false, userId)
        index("\"Message_viewerId_idx\"", false, viewerId)
        index("\"Message_viewId_idx\"", false, viewId)
    }
}
