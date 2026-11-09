package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class SourceSpecEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<SourceSpecEntity>(SourceSpecTable)

    val `package` by SourceSpecTable.`package`
    val version by SourceSpecTable.version
    var specs by SourceSpecTable.specs
    var timestamp by SourceSpecTable.timestamp
    var error by SourceSpecTable.error
}
