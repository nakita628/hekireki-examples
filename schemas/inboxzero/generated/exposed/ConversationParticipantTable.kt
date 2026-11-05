package inboxzero.models

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
    val memberId =
        reference(
            "memberId",
            MemberTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationParticipant_memberId_fkey\"",
        )
    val generation = integer("generation")
    val active = bool("active").default(true)
    val readRevision = integer("readRevision").default(0)
    val muted = bool("muted").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConversationParticipant_pkey\"")

    init {
        index("\"ConversationParticipant_memberId_idx\"", false, memberId)
        uniqueIndex("ConversationParticipant_conversationId_memberId_generation_key", conversationId, memberId, generation)
    }
}
