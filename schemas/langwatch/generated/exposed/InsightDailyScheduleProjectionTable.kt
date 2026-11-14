package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object InsightDailyScheduleProjectionTable : IdTable<String>("\"InsightDailyScheduleProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val userId = text("userId")
    val boardKind = text("boardKind")
    val boardId = text("boardId")
    val boardName = text("boardName")
    val state = text("state").default("undecided")
    val offReason = text("offReason").nullable()
    val hour = integer("hour").nullable()
    val timezone = text("timezone").nullable()
    val maxInsights = integer("maxInsights").nullable()
    val lastRunId = text("lastRunId").nullable()
    val lastRunAt = double("lastRunAt").nullable()
    val lastRunOutcome = text("lastRunOutcome").nullable()
    val lastRunReason = text("lastRunReason").nullable()
    val lastRunFiled = integer("lastRunFiled").nullable()
    val lastRunRenewed = integer("lastRunRenewed").nullable()
    val lastRunConversationId = text("lastRunConversationId").nullable()
    val consecutiveSkips = integer("consecutiveSkips").default(0)
    val skipStreakSlot = double("skipStreakSlot").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    override val primaryKey = PrimaryKey(id, name = "\"InsightDailyScheduleProjection_pkey\"")

    init {
        uniqueIndex("\"InsightDailySchedule_projectId_userId_board_key\"", projectId, userId, boardKind, boardId)
    }
}
