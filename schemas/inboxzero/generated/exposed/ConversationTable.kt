package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationTable : IdTable<String>("\"Conversation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_organizationId_fkey\"",
        )
    val publisherId =
        reference(
            "publisherId",
            MemberTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_publisherId_fkey\"",
        )
    val threadId = text("threadId")
    val status =
        pgEnum("status", "\"ConversationStatus\"", ConversationStatus.entries, ConversationStatus::dbName).default(
            ConversationStatus.ACTIVE,
        )
    val revision = integer("revision").default(0)
    val generation = integer("generation").default(1)
    override val primaryKey = PrimaryKey(id, name = "\"Conversation_pkey\"")

    init {
        index("\"Conversation_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"Conversation_publisherId_threadId_key\"", publisherId, threadId)
    }
}
