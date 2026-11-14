package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object InsightProjectionTable : IdTable<String>("\"InsightProjection\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val title = text("title")
    val body = text("body")
    val tone = text("tone")
    val topic = text("topic").nullable()
    val validDays = integer("validDays")
    val lwql = text("lwql").nullable()
    val sourceConversationId = text("sourceConversationId").nullable()
    val sourceMessageId = text("sourceMessageId").nullable()
    val filedByUserId = text("filedByUserId").nullable()
    val filedAt = double("filedAt")
    val renewedAt = double("renewedAt").nullable()
    val createdAt = double("createdAt")
    val updatedAt = double("updatedAt")
    val occurredAt = double("occurredAt")
    val acceptedAt = double("acceptedAt")
    val lastEventId = text("lastEventId")
    val projectionVersion = text("projectionVersion")
    val boardId = text("boardId").nullable()
    val boardName = text("boardName").nullable()
    val widgetId = text("widgetId").nullable()
    val widgetName = text("widgetName").nullable()
    val filedVia = text("filedVia").default("chat")
    val replayStart = double("replayStart").nullable()
    val replayEnd = double("replayEnd").nullable()
    val replayGranularitySeconds = integer("replayGranularitySeconds").nullable()
    val replayContext = jsonb<String>("replayContext", { it }, { it }).nullable()
    val ownerUserId = text("ownerUserId").nullable()
    val findingKey = text("findingKey").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"InsightProjection_pkey\"")

    init {
        index("\"InsightProjection_projectId_filedAt_idx\"", false, projectId, filedAt)
        index("\"InsightProjection_projectId_ownerUserId_filedAt_idx\"", false, projectId, ownerUserId, filedAt)
    }
}
