package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InsightDailyScheduleProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InsightDailyScheduleProjectionEntity>(InsightDailyScheduleProjectionTable)

    var projectId by InsightDailyScheduleProjectionTable.projectId
    var userId by InsightDailyScheduleProjectionTable.userId
    var boardKind by InsightDailyScheduleProjectionTable.boardKind
    var boardId by InsightDailyScheduleProjectionTable.boardId
    var boardName by InsightDailyScheduleProjectionTable.boardName
    var state by InsightDailyScheduleProjectionTable.state
    var offReason by InsightDailyScheduleProjectionTable.offReason
    var hour by InsightDailyScheduleProjectionTable.hour
    var timezone by InsightDailyScheduleProjectionTable.timezone
    var maxInsights by InsightDailyScheduleProjectionTable.maxInsights
    var lastRunId by InsightDailyScheduleProjectionTable.lastRunId
    var lastRunAt by InsightDailyScheduleProjectionTable.lastRunAt
    var lastRunOutcome by InsightDailyScheduleProjectionTable.lastRunOutcome
    var lastRunReason by InsightDailyScheduleProjectionTable.lastRunReason
    var lastRunFiled by InsightDailyScheduleProjectionTable.lastRunFiled
    var lastRunRenewed by InsightDailyScheduleProjectionTable.lastRunRenewed
    var lastRunConversationId by InsightDailyScheduleProjectionTable.lastRunConversationId
    var consecutiveSkips by InsightDailyScheduleProjectionTable.consecutiveSkips
    var skipStreakSlot by InsightDailyScheduleProjectionTable.skipStreakSlot
    var createdAt by InsightDailyScheduleProjectionTable.createdAt
    var updatedAt by InsightDailyScheduleProjectionTable.updatedAt
    var occurredAt by InsightDailyScheduleProjectionTable.occurredAt
    var acceptedAt by InsightDailyScheduleProjectionTable.acceptedAt
    var lastEventId by InsightDailyScheduleProjectionTable.lastEventId
    var projectionVersion by InsightDailyScheduleProjectionTable.projectionVersion
}
