package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InsightProjectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InsightProjectionEntity>(InsightProjectionTable)

    var projectId by InsightProjectionTable.projectId
    var title by InsightProjectionTable.title
    var body by InsightProjectionTable.body
    var tone by InsightProjectionTable.tone
    var topic by InsightProjectionTable.topic
    var validDays by InsightProjectionTable.validDays
    var lwql by InsightProjectionTable.lwql
    var sourceConversationId by InsightProjectionTable.sourceConversationId
    var sourceMessageId by InsightProjectionTable.sourceMessageId
    var filedByUserId by InsightProjectionTable.filedByUserId
    var filedAt by InsightProjectionTable.filedAt
    var renewedAt by InsightProjectionTable.renewedAt
    var createdAt by InsightProjectionTable.createdAt
    var updatedAt by InsightProjectionTable.updatedAt
    var occurredAt by InsightProjectionTable.occurredAt
    var acceptedAt by InsightProjectionTable.acceptedAt
    var lastEventId by InsightProjectionTable.lastEventId
    var projectionVersion by InsightProjectionTable.projectionVersion
    var boardId by InsightProjectionTable.boardId
    var boardName by InsightProjectionTable.boardName
    var widgetId by InsightProjectionTable.widgetId
    var widgetName by InsightProjectionTable.widgetName
    var filedVia by InsightProjectionTable.filedVia
    var replayStart by InsightProjectionTable.replayStart
    var replayEnd by InsightProjectionTable.replayEnd
    var replayGranularitySeconds by InsightProjectionTable.replayGranularitySeconds
    var replayContext by InsightProjectionTable.replayContext
    var ownerUserId by InsightProjectionTable.ownerUserId
    var findingKey by InsightProjectionTable.findingKey
}
