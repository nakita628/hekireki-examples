package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OutboundEmailAuditTable : IdTable<String>("\"OutboundEmailAudit\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val contentExpiresAt = pgTimestamp("contentExpiresAt", 3)
    val environment = text("environment")
    val emailType = text("emailType")
    val trigger = text("trigger")
    val actorKind = text("actorKind")
    val actorUserId = text("actorUserId").nullable()
    val configuredByUserId = text("configuredByUserId").nullable()
    val actorUserHash = text("actorUserHash").nullable()
    val configuredByUserHash = text("configuredByUserHash").nullable()
    val organizationId = text("organizationId").nullable()
    val surveyId = text("surveyId").nullable()
    val responseId = text("responseId").nullable()
    val followUpId = text("followUpId").nullable()
    val workflowId = text("workflowId").nullable()
    val workflowRunId = text("workflowRunId").nullable()
    val inviteId = text("inviteId").nullable()
    val recipientMac = text("recipientMac").nullable()
    val sesMessageHash = text("sesMessageHash").nullable()
    val snapshotKey = text("snapshotKey").nullable()
    val snapshotSha256 = text("snapshotSha256").nullable()
    val status = text("status").default("prepared")
    val statusRecordedAt = pgTimestamp("statusRecordedAt", 3).nullable()
    val auditGap = bool("auditGap").default(false)
    val erasureRequestedAt = pgTimestamp("erasureRequestedAt", 3).nullable()
    val erasedAt = pgTimestamp("erasedAt", 3).nullable()
    val contentDeleteRetryAt = pgTimestamp("contentDeleteRetryAt", 3).nullable()
    val contentDeleteFailures = integer("contentDeleteFailures").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"OutboundEmailAudit_pkey\"")

    init {
        index("\"OutboundEmailAudit_environment_createdAt_id_idx\"", false, environment, createdAt, id)
        index("\"OutboundEmailAudit_organizationId_createdAt_id_idx\"", false, organizationId, createdAt, id)
        index("\"OutboundEmailAudit_surveyId_createdAt_id_idx\"", false, surveyId, createdAt, id)
        index("\"OutboundEmailAudit_responseId_createdAt_id_idx\"", false, responseId, createdAt, id)
        index("\"OutboundEmailAudit_actorUserId_createdAt_id_idx\"", false, actorUserId, createdAt, id)
        index("\"OutboundEmailAudit_configuredByUserId_createdAt_id_idx\"", false, configuredByUserId, createdAt, id)
        index("\"OutboundEmailAudit_actorUserHash_createdAt_id_idx\"", false, actorUserHash, createdAt, id)
        index("\"OutboundEmailAudit_configuredByUserHash_createdAt_id_idx\"", false, configuredByUserHash, createdAt, id)
        index("\"OutboundEmailAudit_recipientMac_createdAt_id_idx\"", false, recipientMac, createdAt, id)
        index("\"OutboundEmailAudit_sesMessageHash_idx\"", false, sesMessageHash)
        index("\"OutboundEmailAudit_status_createdAt_id_idx\"", false, status, createdAt, id)
        index("\"OutboundEmailAudit_contentExpiresAt_id_idx\"", false, contentExpiresAt, id)
        index("\"OutboundEmailAudit_expiresAt_id_idx\"", false, expiresAt, id)
        index("\"OutboundEmailAudit_erasureRequestedAt_id_idx\"", false, erasureRequestedAt, id)
        index("\"OutboundEmailAudit_contentDeleteRetryAt_id_idx\"", false, contentDeleteRetryAt, id)
    }
}
