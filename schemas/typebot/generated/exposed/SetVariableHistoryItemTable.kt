package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object SetVariableHistoryItemTable : CompositeIdTable("\"SetVariableHistoryItem\"") {
    val resultId =
        reference(
            "resultId",
            ResultTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SetVariableHistoryItem_resultId_fkey\"",
        )
    val index = integer("index").entityId()
    val variableId = text("variableId")
    val blockId = text("blockId")
    val blockIndex = integer("blockIndex").nullable()
    val value = jsonb<String>("value", { it }, { it })

    init {
        addIdColumn(resultId)
        uniqueIndex("\"SetVariableHistoryItem_resultId_index_key\"", resultId, index)
    }
}
