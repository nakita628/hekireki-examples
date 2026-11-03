package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DataroomQuestionAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomQuestionAssignmentEntity>(DataroomQuestionAssignmentTable)

    var questionId by DataroomQuestionAssignmentTable.questionId
    var question by DataroomQuestionEntity referencedOn DataroomQuestionAssignmentTable.questionId
    var linkId by DataroomQuestionAssignmentTable.linkId
    var link by LinkEntity optionalReferencedOn DataroomQuestionAssignmentTable.linkId
    var groupId by DataroomQuestionAssignmentTable.groupId
    var group by ViewerGroupEntity optionalReferencedOn DataroomQuestionAssignmentTable.groupId
    var viewerId by DataroomQuestionAssignmentTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn DataroomQuestionAssignmentTable.viewerId
    var email by DataroomQuestionAssignmentTable.email
    var createdAt by DataroomQuestionAssignmentTable.createdAt
}
