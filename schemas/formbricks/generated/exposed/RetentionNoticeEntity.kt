package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RetentionNoticeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionNoticeEntity>(RetentionNoticeTable)

    var organization by OrganizationEntity referencedOn RetentionNoticeTable.organizationId
    var organizationId by RetentionNoticeTable.organizationId
    var entity by RetentionNoticeTable.entity
    var survey by SurveyEntity optionalReferencedOn RetentionNoticeTable.surveyId
    var surveyId by RetentionNoticeTable.surveyId
    var user by UserEntity optionalReferencedOn RetentionNoticeTable.userId
    var userId by RetentionNoticeTable.userId
    var sentAt by RetentionNoticeTable.sentAt
    var deliveredAt by RetentionNoticeTable.deliveredAt
    var emailSent by RetentionNoticeTable.emailSent
    var claimToken by RetentionNoticeTable.claimToken
    var clockAt by RetentionNoticeTable.clockAt
}
