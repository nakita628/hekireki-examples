package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object IngestionPullRunProjectionTable : IdTable<String>("\"IngestionPullRunProjection\"") {
    override val id = text("id").entityId()
    val sourceId = text("sourceId").uniqueIndex("\"IngestionPullRunProjection_sourceId_key\"")
    val projectId = text("projectId")
    val enabled = bool("enabled").default(false)
    val cron = text("cron").nullable()
    val cursor = text("cursor").nullable()
    val lastRunAt = double("lastRunAt").nullable()
    val lastRunOutcome = text("lastRunOutcome").nullable()
    val lastRunEventCount = integer("lastRunEventCount").default(0)
    val lastRunError = text("lastRunError").nullable()
    val lastRunErrorCode = text("lastRunErrorCode").nullable()
    val consecutiveErrors = integer("consecutiveErrors").default(0)
    val lastSuccessAt = double("lastSuccessAt").nullable()
    val lastRunScheduledFor = double("lastRunScheduledFor").nullable()
    val lastReadThroughAt = double("lastReadThroughAt").nullable()
    val lastRunCompleteness = text("lastRunCompleteness").nullable()
    val lastAgentsListingAt = double("lastAgentsListingAt").nullable()
    val lastAgentsListingOutcome = text("lastAgentsListingOutcome").nullable()
    val lastAgentsListingCount = integer("lastAgentsListingCount").nullable()
    val lastAgentsListingReason = text("lastAgentsListingReason").nullable()
    val lastAgentsListingStatus = integer("lastAgentsListingStatus").nullable()
    val lastPeopleListingAt = double("lastPeopleListingAt").nullable()
    val lastPeopleListingOutcome = text("lastPeopleListingOutcome").nullable()
    val lastPeopleDirectoryCount = integer("lastPeopleDirectoryCount").nullable()
    val lastPeopleWithheldCount = integer("lastPeopleWithheldCount").nullable()
    val lastPeopleListingReason = text("lastPeopleListingReason").nullable()
    val lastPeopleListingStatus = integer("lastPeopleListingStatus").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"IngestionPullRunProjection_pkey\"")

    init {
        index("\"IngestionPullRunProjection_projectId_idx\"", false, projectId)
    }
}
