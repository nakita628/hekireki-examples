package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SourceStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SourceStateEntity>(SourceStateTable)

    val syncId by SourceStateTable.syncId
    val stream by SourceStateTable.stream
    var state by SourceStateTable.state
    var timestamp by SourceStateTable.timestamp
}
