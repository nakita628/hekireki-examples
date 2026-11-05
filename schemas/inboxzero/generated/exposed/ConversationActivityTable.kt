package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationActivityTable : IdTable<String>("\"ConversationActivity\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val participantId =
        reference(
            "participantId",
            ConversationParticipantTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationActivity_participantId_fkey\"",
        )
    val commentId =
        optReference(
            "commentId",
            ConversationCommentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationActivity_commentId_fkey\"",
        )
    val kind = pgEnum("kind", "\"ConversationActivityKind\"", ConversationActivityKind.entries, ConversationActivityKind::dbName)
    val revision = integer("revision")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConversationActivity_pkey\"")

    init {
        index("\"ConversationActivity_participantId_createdAt_id_idx\"", false, participantId, createdAt, id)
        uniqueIndex("\"ConversationActivity_participantId_kind_revision_key\"", participantId, kind, revision)
    }
}
