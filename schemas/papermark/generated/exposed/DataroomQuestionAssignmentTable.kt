package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomQuestionAssignmentTable : IdTable<String>("\"DataroomQuestionAssignment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val questionId =
        reference(
            "questionId",
            DataroomQuestionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestionAssignment_questionId_fkey\"",
        )
    val linkId =
        optReference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestionAssignment_linkId_fkey\"",
        )
    val groupId =
        optReference(
            "groupId",
            ViewerGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestionAssignment_groupId_fkey\"",
        )
    val viewerId =
        optReference(
            "viewerId",
            ViewerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomQuestionAssignment_viewerId_fkey\"",
        )
    val email = text("email").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomQuestionAssignment_pkey\"")

    init {
        index("\"DataroomQuestionAssignment_questionId_idx\"", false, questionId)
        index("\"DataroomQuestionAssignment_linkId_idx\"", false, linkId)
        index("\"DataroomQuestionAssignment_groupId_idx\"", false, groupId)
        index("\"DataroomQuestionAssignment_viewerId_idx\"", false, viewerId)
        uniqueIndex("\"DataroomQuestionAssignment_questionId_linkId_key\"", questionId, linkId)
        uniqueIndex("\"DataroomQuestionAssignment_questionId_groupId_key\"", questionId, groupId)
        uniqueIndex("\"DataroomQuestionAssignment_questionId_viewerId_key\"", questionId, viewerId)
        uniqueIndex("\"DataroomQuestionAssignment_questionId_email_key\"", questionId, email)
    }
}
