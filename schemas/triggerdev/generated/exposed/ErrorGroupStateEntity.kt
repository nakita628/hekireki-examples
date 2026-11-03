package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ErrorGroupStateEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ErrorGroupStateEntity>(ErrorGroupStateTable)

    var organization by OrganizationEntity referencedOn ErrorGroupStateTable.organizationId
    var organizationId by ErrorGroupStateTable.organizationId
    var project by ProjectEntity referencedOn ErrorGroupStateTable.projectId
    var projectId by ErrorGroupStateTable.projectId
    var environment by RuntimeEnvironmentEntity optionalReferencedOn ErrorGroupStateTable.environmentId
    var environmentId by ErrorGroupStateTable.environmentId
    var taskIdentifier by ErrorGroupStateTable.taskIdentifier
    var errorFingerprint by ErrorGroupStateTable.errorFingerprint
    var status by ErrorGroupStateTable.status
    var ignoredUntil by ErrorGroupStateTable.ignoredUntil
    var ignoredUntilOccurrenceRate by ErrorGroupStateTable.ignoredUntilOccurrenceRate
    var ignoredUntilTotalOccurrences by ErrorGroupStateTable.ignoredUntilTotalOccurrences
    var ignoredAtOccurrenceCount by ErrorGroupStateTable.ignoredAtOccurrenceCount
    var ignoredAt by ErrorGroupStateTable.ignoredAt
    var ignoredReason by ErrorGroupStateTable.ignoredReason
    var ignoredByUserId by ErrorGroupStateTable.ignoredByUserId
    var resolvedAt by ErrorGroupStateTable.resolvedAt
    var resolvedInVersion by ErrorGroupStateTable.resolvedInVersion
    var resolvedBy by ErrorGroupStateTable.resolvedBy
    var createdAt by ErrorGroupStateTable.createdAt
    var updatedAt by ErrorGroupStateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ErrorGroupStateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
