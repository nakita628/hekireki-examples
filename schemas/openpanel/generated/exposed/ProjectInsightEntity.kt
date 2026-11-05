package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ProjectInsightEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ProjectInsightEntity>(ProjectInsightTable)

    var projectId by ProjectInsightTable.projectId
    var moduleKey by ProjectInsightTable.moduleKey
    var dimensionKey by ProjectInsightTable.dimensionKey
    var windowKind by ProjectInsightTable.windowKind
    var state by ProjectInsightTable.state
    var title by ProjectInsightTable.title
    var summary by ProjectInsightTable.summary
    var displayName by ProjectInsightTable.displayName
    var payload by ProjectInsightTable.payload
    var direction by ProjectInsightTable.direction
    var impactScore by ProjectInsightTable.impactScore
    var severityBand by ProjectInsightTable.severityBand
    var relevanceScore by ProjectInsightTable.relevanceScore
    var aiSummary by ProjectInsightTable.aiSummary
    var aiCategory by ProjectInsightTable.aiCategory
    var emailWorthy by ProjectInsightTable.emailWorthy
    var referenceWorthy by ProjectInsightTable.referenceWorthy
    var enrichedAt by ProjectInsightTable.enrichedAt
    var enrichVersion by ProjectInsightTable.enrichVersion
    var version by ProjectInsightTable.version
    var threadId by ProjectInsightTable.threadId
    var windowStart by ProjectInsightTable.windowStart
    var windowEnd by ProjectInsightTable.windowEnd
    var firstDetectedAt by ProjectInsightTable.firstDetectedAt
    var lastUpdatedAt by ProjectInsightTable.lastUpdatedAt
    var lastSeenAt by ProjectInsightTable.lastSeenAt
    val events by InsightEventEntity referrersOn InsightEventTable.insightId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectInsightTable.lastUpdatedAt)) {
                lastUpdatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
