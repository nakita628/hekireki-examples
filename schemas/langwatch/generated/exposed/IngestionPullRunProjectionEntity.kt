package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IngestionPullRunProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IngestionPullRunProjectionEntity>(IngestionPullRunProjectionTable)

    var sourceId by IngestionPullRunProjectionTable.sourceId
    var projectId by IngestionPullRunProjectionTable.projectId
    var enabled by IngestionPullRunProjectionTable.enabled
    var cron by IngestionPullRunProjectionTable.cron
    var cursor by IngestionPullRunProjectionTable.cursor
    var lastRunAt by IngestionPullRunProjectionTable.lastRunAt
    var lastRunOutcome by IngestionPullRunProjectionTable.lastRunOutcome
    var lastRunEventCount by IngestionPullRunProjectionTable.lastRunEventCount
    var lastRunError by IngestionPullRunProjectionTable.lastRunError
    var lastRunErrorCode by IngestionPullRunProjectionTable.lastRunErrorCode
    var consecutiveErrors by IngestionPullRunProjectionTable.consecutiveErrors
    var lastSuccessAt by IngestionPullRunProjectionTable.lastSuccessAt
    var lastRunScheduledFor by IngestionPullRunProjectionTable.lastRunScheduledFor
    var lastReadThroughAt by IngestionPullRunProjectionTable.lastReadThroughAt
    var lastRunCompleteness by IngestionPullRunProjectionTable.lastRunCompleteness
    var lastAgentsListingAt by IngestionPullRunProjectionTable.lastAgentsListingAt
    var lastAgentsListingOutcome by IngestionPullRunProjectionTable.lastAgentsListingOutcome
    var lastAgentsListingCount by IngestionPullRunProjectionTable.lastAgentsListingCount
    var lastAgentsListingReason by IngestionPullRunProjectionTable.lastAgentsListingReason
    var lastAgentsListingStatus by IngestionPullRunProjectionTable.lastAgentsListingStatus
    var lastPeopleListingAt by IngestionPullRunProjectionTable.lastPeopleListingAt
    var lastPeopleListingOutcome by IngestionPullRunProjectionTable.lastPeopleListingOutcome
    var lastPeopleDirectoryCount by IngestionPullRunProjectionTable.lastPeopleDirectoryCount
    var lastPeopleWithheldCount by IngestionPullRunProjectionTable.lastPeopleWithheldCount
    var lastPeopleListingReason by IngestionPullRunProjectionTable.lastPeopleListingReason
    var lastPeopleListingStatus by IngestionPullRunProjectionTable.lastPeopleListingStatus
    var createdAt by IngestionPullRunProjectionTable.createdAt
    var updatedAt by IngestionPullRunProjectionTable.updatedAt
    var occurredAt by IngestionPullRunProjectionTable.occurredAt
    var acceptedAt by IngestionPullRunProjectionTable.acceptedAt
    var lastEventId by IngestionPullRunProjectionTable.lastEventId
    var projectionVersion by IngestionPullRunProjectionTable.projectionVersion
}
