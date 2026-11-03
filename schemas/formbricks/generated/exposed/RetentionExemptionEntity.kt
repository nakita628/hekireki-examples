package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RetentionExemptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RetentionExemptionEntity>(RetentionExemptionTable)

    var createdAt by RetentionExemptionTable.createdAt
    var organization by OrganizationEntity referencedOn RetentionExemptionTable.organizationId
    var organizationId by RetentionExemptionTable.organizationId
    var entity by RetentionExemptionTable.entity
    var survey by SurveyEntity optionalReferencedOn RetentionExemptionTable.surveyId
    var surveyId by RetentionExemptionTable.surveyId
    var until by RetentionExemptionTable.until
    var source by RetentionExemptionTable.source1
    var reason by RetentionExemptionTable.reason
    var createdBy by UserEntity optionalReferencedOn RetentionExemptionTable.createdById
    var createdById by RetentionExemptionTable.createdById
    var revokedAt by RetentionExemptionTable.revokedAt
    var revokedBy by UserEntity optionalReferencedOn RetentionExemptionTable.revokedById
    var revokedById by RetentionExemptionTable.revokedById
}
