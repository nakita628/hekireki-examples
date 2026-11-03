package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ComputedUpdateStageLedgerEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ComputedUpdateStageLedgerEntity>(ComputedUpdateStageLedgerTable)

    val scopeId by ComputedUpdateStageLedgerTable.scopeId
    val kind by ComputedUpdateStageLedgerTable.kind
    val tableId by ComputedUpdateStageLedgerTable.tableId
    val recordId by ComputedUpdateStageLedgerTable.recordId
    var seq by ComputedUpdateStageLedgerTable.seq
}
