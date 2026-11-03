package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationParticipantTable : IdTable<String>("\"ConversationParticipant\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationParticipant_conversationId_fkey\"",
        )
    val role = pgEnum("role", "\"ParticipantRole\"", ParticipantRole.entries, ParticipantRole::dbName).default(ParticipantRole.PARTICIPANT)
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationParticipant_viewerId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationParticipant_userId_fkey\"",
        )
    val receiveNotifications = bool("receiveNotifications").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConversationParticipant_pkey\"")

    init {
        index("\"ConversationParticipant_conversationId_idx\"", false, conversationId)
        index("\"ConversationParticipant_viewerId_idx\"", false, viewerId)
        index("\"ConversationParticipant_userId_idx\"", false, userId)
        uniqueIndex("\"ConversationParticipant_conversationId_viewerId_key\"", conversationId, viewerId)
        uniqueIndex("\"ConversationParticipant_conversationId_userId_key\"", conversationId, userId)
    }
}
