package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConversationTable : IdTable<String>("\"Conversation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val title = text("title").nullable()
    val isEnabled = bool("isEnabled").default(true)
    val visibilityMode =
        pgEnum("visibilityMode", "\"ConversationVisibility\"", ConversationVisibility.entries, ConversationVisibility::dbName).default(
            ConversationVisibility.PRIVATE,
        )
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_dataroomId_fkey\"",
        )
    val dataroomDocumentId =
        optReference(
            "dataroomDocumentId",
            DataroomDocumentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_dataroomDocumentId_fkey\"",
        )
    val documentVersionNumber = integer("documentVersionNumber").nullable()
    val documentPageNumber = integer("documentPageNumber").nullable()
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_linkId_fkey\"",
        )
    val viewerGroupId =
        optReference(
            "viewerGroupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_viewerGroupId_fkey\"",
        )
    val questionId =
        optReference(
            "questionId",
            DataroomQuestionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_questionId_fkey\"",
        )
    val origin = text("origin").default("visitor")
    val category = text("category").nullable()
    val priority = text("priority").nullable()
    val recipientViewerId =
        optReference(
            "recipientViewerId",
            ViewerTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_recipientViewerId_fkey\"",
        )
    val initialViewId =
        optReference(
            "initialViewId",
            ViewTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_initialViewId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Conversation_teamId_fkey\"",
        )
    val lastMessageAt = pgTimestamp("lastMessageAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Conversation_pkey\"")

    init {
        index("\"Conversation_dataroomId_idx\"", false, dataroomId)
        index("\"Conversation_dataroomDocumentId_idx\"", false, dataroomDocumentId)
        index("\"Conversation_linkId_idx\"", false, linkId)
        index("\"Conversation_teamId_idx\"", false, teamId)
        index("\"Conversation_viewerGroupId_idx\"", false, viewerGroupId)
        index("\"Conversation_initialViewId_idx\"", false, initialViewId)
        index("\"Conversation_questionId_idx\"", false, questionId)
        index("\"Conversation_recipientViewerId_idx\"", false, recipientViewerId)
        uniqueIndex("\"Conversation_questionId_recipientViewerId_key\"", questionId, recipientViewerId)
    }
}
