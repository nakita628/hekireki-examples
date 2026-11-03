package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object VisitedEdgeTable : CompositeIdTable("\"VisitedEdge\"") {
    val resultId =
        reference(
            "resultId",
            ResultTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VisitedEdge_resultId_fkey\"",
        )
    val edgeId = text("edgeId")
    val index = integer("index").entityId()

    init {
        addIdColumn(resultId)
        uniqueIndex("\"VisitedEdge_resultId_index_key\"", resultId, index)
    }
}
