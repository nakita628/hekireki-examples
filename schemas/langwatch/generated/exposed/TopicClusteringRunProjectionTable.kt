package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TopicClusteringRunProjectionTable : IdTable<String>("\"TopicClusteringRunProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId").uniqueIndex("\"TopicClusteringRunProjection_projectId_key\"")
    val lastRequestedAt = double("lastRequestedAt").nullable()
    val lastRequestTrigger = text("lastRequestTrigger").nullable()
    val lastRunAt = double("lastRunAt").nullable()
    val lastRunOutcome = text("lastRunOutcome").nullable()
    val lastRunMode = text("lastRunMode").nullable()
    val lastRunSkippedReason = text("lastRunSkippedReason").nullable()
    val lastRunError = text("lastRunError").nullable()
    val lastRunErrorCode = text("lastRunErrorCode").nullable()
    val lastRunErrorUserActionable = bool("lastRunErrorUserActionable").default(false)
    val lastRunTracesProcessed = integer("lastRunTracesProcessed").default(0)
    val lastRunTopicsCount = integer("lastRunTopicsCount").default(0)
    val lastRunSubtopicsCount = integer("lastRunSubtopicsCount").default(0)
    val lastRunPages = integer("lastRunPages").default(0)
    val inProgressRunId = text("inProgressRunId").nullable()
    val inProgressTraces = integer("inProgressTraces").default(0)
    val inProgressPages = integer("inProgressPages").default(0)
    val inProgressStartedAt = double("inProgressStartedAt").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"TopicClusteringRunProjection_pkey\"")
}
