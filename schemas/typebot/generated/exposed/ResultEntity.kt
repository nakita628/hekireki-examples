package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ResultEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ResultEntity>(ResultTable)

    var createdAt by ResultTable.createdAt
    var typebotId by ResultTable.typebotId
    var variables by ResultTable.variables
    var isCompleted by ResultTable.isCompleted
    var hasStarted by ResultTable.hasStarted
    var isArchived by ResultTable.isArchived
    var lastChatSessionId by ResultTable.lastChatSessionId
    var typebot by TypebotEntity referencedOn ResultTable.typebotId
    val answers by AnswerEntity referrersOn AnswerTable.resultId
    val answersV2 by AnswerV2Entity referrersOn AnswerV2Table.resultId
    val logs by LogEntity referrersOn LogTable.resultId
    val edges by VisitedEdgeEntity referrersOn VisitedEdgeTable.resultId
    val setVariableHistory by SetVariableHistoryItemEntity referrersOn SetVariableHistoryItemTable.resultId
}
