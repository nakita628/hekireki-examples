package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationCommentTable : IdTable<String>("\"ConversationComment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val conversationId =
        reference(
            "conversationId",
            ConversationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationComment_conversationId_fkey\"",
        )
    val authorMemberId =
        optReference(
            "authorMemberId",
            MemberTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ConversationComment_authorMemberId_fkey\"",
        )
    val clientMutationId = text("clientMutationId")
    val body = text("body")
    val revision = integer("revision")
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConversationComment_pkey\"")

    init {
        uniqueIndex("\"ConversationComment_conversationId_revision_key\"", conversationId, revision)
        uniqueIndex("ConversationComment_conversationId_authorMemberId_clientMut_key", conversationId, authorMemberId, clientMutationId)
    }
}
