package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomFaqItemTable : IdTable<String>("\"DataroomFaqItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val title = text("title").nullable()
    val editedQuestion = text("editedQuestion")
    val originalQuestion = text("originalQuestion").nullable()
    val answer = text("answer")
    val description = text("description").nullable()
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_dataroomId_fkey\"",
        )
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_linkId_fkey\"",
        )
    val dataroomDocumentId =
        optReference(
            "dataroomDocumentId",
            DataroomDocumentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_dataroomDocumentId_fkey\"",
        )
    val sourceConversationId =
        optReference(
            "sourceConversationId",
            ConversationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_sourceConversationId_fkey\"",
        )
    val questionMessageId =
        optReference(
            "questionMessageId",
            MessageTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_questionMessageId_fkey\"",
        )
    val answerMessageId =
        optReference(
            "answerMessageId",
            MessageTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_answerMessageId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_teamId_fkey\"",
        )
    val publishedByUserId =
        reference(
            "publishedByUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomFaqItem_publishedByUserId_fkey\"",
        )
    val visibilityMode =
        pgEnum("visibilityMode", "\"FaqVisibility\"", FaqVisibility.entries, FaqVisibility::dbName).default(FaqVisibility.PUBLIC_DATAROOM)
    val status = pgEnum("status", "\"FaqStatus\"", FaqStatus.entries, FaqStatus::dbName).default(FaqStatus.PUBLISHED)
    val isAnonymized = bool("isAnonymized").default(true)
    val viewCount = integer("viewCount").default(0)
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val documentPageNumber = integer("documentPageNumber").nullable()
    val documentVersionNumber = integer("documentVersionNumber").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomFaqItem_pkey\"")

    init {
        index("\"DataroomFaqItem_dataroomId_idx\"", false, dataroomId)
        index("\"DataroomFaqItem_linkId_idx\"", false, linkId)
        index("\"DataroomFaqItem_dataroomDocumentId_idx\"", false, dataroomDocumentId)
        index("\"DataroomFaqItem_sourceConversationId_idx\"", false, sourceConversationId)
        index("\"DataroomFaqItem_teamId_idx\"", false, teamId)
        index("\"DataroomFaqItem_publishedByUserId_idx\"", false, publishedByUserId)
        index("\"DataroomFaqItem_status_idx\"", false, status)
        index("\"DataroomFaqItem_visibilityMode_idx\"", false, visibilityMode)
        index("\"DataroomFaqItem_createdAt_idx\"", false, createdAt)
    }
}
