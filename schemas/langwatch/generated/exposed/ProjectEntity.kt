package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectEntity>(ProjectTable)

    var name by ProjectTable.name
    var slug by ProjectTable.slug
    var apiKey by ProjectTable.apiKey
    var legacyApiKeyLastUsedAt by ProjectTable.legacyApiKeyLastUsedAt
    var lwqlKey by ProjectTable.lwqlKey
    var teamId by ProjectTable.teamId
    var language by ProjectTable.language
    var framework by ProjectTable.framework
    var kind by ProjectTable.kind
    var aggregateRule by ProjectTable.aggregateRule
    var firstMessage by ProjectTable.firstMessage
    var integrated by ProjectTable.integrated
    var createdAt by ProjectTable.createdAt
    var updatedAt by ProjectTable.updatedAt
    var userLinkTemplate by ProjectTable.userLinkTemplate
    var traceSharingEnabled by ProjectTable.traceSharingEnabled
    var presenceEnabled by ProjectTable.presenceEnabled
    var s3Endpoint by ProjectTable.s3Endpoint
    var s3AccessKeyId by ProjectTable.s3AccessKeyId
    var s3SecretAccessKey by ProjectTable.s3SecretAccessKey
    var s3Bucket by ProjectTable.s3Bucket
    var archivedAt by ProjectTable.archivedAt
    var isPersonal by ProjectTable.isPersonal
    var ownerUserId by ProjectTable.ownerUserId
    var personalFeatures by ProjectTable.personalFeatures
    var departmentId by ProjectTable.departmentId
    var langyEgressAllowlist by ProjectTable.langyEgressAllowlist
    var lastCodingAgentSessionAt by ProjectTable.lastCodingAgentSessionAt
    var lastCodingAgentPullRequestAt by ProjectTable.lastCodingAgentPullRequestAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
