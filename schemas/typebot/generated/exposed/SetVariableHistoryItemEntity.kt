package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SetVariableHistoryItemEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SetVariableHistoryItemEntity>(SetVariableHistoryItemTable)

    val result by ResultEntity referencedOn SetVariableHistoryItemTable.resultId
    val resultId by SetVariableHistoryItemTable.resultId
    val index by SetVariableHistoryItemTable.index
    var variableId by SetVariableHistoryItemTable.variableId
    var blockId by SetVariableHistoryItemTable.blockId
    var blockIndex by SetVariableHistoryItemTable.blockIndex
    var value by SetVariableHistoryItemTable.value
}
