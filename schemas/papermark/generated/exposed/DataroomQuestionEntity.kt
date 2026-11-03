package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomQuestionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomQuestionEntity>(DataroomQuestionTable)

    var content by DataroomQuestionTable.content
    var category by DataroomQuestionTable.category
    var priority by DataroomQuestionTable.priority
    var status by DataroomQuestionTable.status
    var orderIndex by DataroomQuestionTable.orderIndex
    var importBatchId by DataroomQuestionTable.importBatchId
    var dataroomId by DataroomQuestionTable.dataroomId
    var dataroom by DataroomEntity referencedOn DataroomQuestionTable.dataroomId
    var teamId by DataroomQuestionTable.teamId
    var team by TeamEntity referencedOn DataroomQuestionTable.teamId
    var createdByUserId by DataroomQuestionTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn DataroomQuestionTable.createdByUserId
    var dataroomDocumentId by DataroomQuestionTable.dataroomDocumentId
    var dataroomDocument by DataroomDocumentEntity optionalReferencedOn DataroomQuestionTable.dataroomDocumentId
    var documentPageNumber by DataroomQuestionTable.documentPageNumber
    val assignments by DataroomQuestionAssignmentEntity referrersOn DataroomQuestionAssignmentTable.questionId
    val conversations by ConversationEntity optionalReferrersOn ConversationTable.questionId
    var createdAt by DataroomQuestionTable.createdAt
    var updatedAt by DataroomQuestionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomQuestionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
