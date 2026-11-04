package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OutboundEmailAuditEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OutboundEmailAuditEntity>(OutboundEmailAuditTable)

    var createdAt by OutboundEmailAuditTable.createdAt
    var updatedAt by OutboundEmailAuditTable.updatedAt
    var expiresAt by OutboundEmailAuditTable.expiresAt
    var contentExpiresAt by OutboundEmailAuditTable.contentExpiresAt
    var environment by OutboundEmailAuditTable.environment
    var emailType by OutboundEmailAuditTable.emailType
    var trigger by OutboundEmailAuditTable.trigger
    var actorKind by OutboundEmailAuditTable.actorKind
    var actorUserId by OutboundEmailAuditTable.actorUserId
    var configuredByUserId by OutboundEmailAuditTable.configuredByUserId
    var actorUserHash by OutboundEmailAuditTable.actorUserHash
    var configuredByUserHash by OutboundEmailAuditTable.configuredByUserHash
    var organizationId by OutboundEmailAuditTable.organizationId
    var surveyId by OutboundEmailAuditTable.surveyId
    var responseId by OutboundEmailAuditTable.responseId
    var followUpId by OutboundEmailAuditTable.followUpId
    var workflowId by OutboundEmailAuditTable.workflowId
    var workflowRunId by OutboundEmailAuditTable.workflowRunId
    var inviteId by OutboundEmailAuditTable.inviteId
    var recipientMac by OutboundEmailAuditTable.recipientMac
    var sesMessageHash by OutboundEmailAuditTable.sesMessageHash
    var snapshotKey by OutboundEmailAuditTable.snapshotKey
    var snapshotSha256 by OutboundEmailAuditTable.snapshotSha256
    var status by OutboundEmailAuditTable.status
    var statusRecordedAt by OutboundEmailAuditTable.statusRecordedAt
    var auditGap by OutboundEmailAuditTable.auditGap
    var erasureRequestedAt by OutboundEmailAuditTable.erasureRequestedAt
    var erasedAt by OutboundEmailAuditTable.erasedAt
    var contentDeleteRetryAt by OutboundEmailAuditTable.contentDeleteRetryAt
    var contentDeleteFailures by OutboundEmailAuditTable.contentDeleteFailures

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OutboundEmailAuditTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
