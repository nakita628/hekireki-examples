package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MastraThreadStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MastraThreadStateEntity>(MastraThreadStateTable)

    val threadId by MastraThreadStateTable.threadId
    val type by MastraThreadStateTable.type
    var value by MastraThreadStateTable.value
    var createdAt by MastraThreadStateTable.createdAt
    var updatedAt by MastraThreadStateTable.updatedAt
    var createdAtZ by MastraThreadStateTable.createdAtZ
    var updatedAtZ by MastraThreadStateTable.updatedAtZ
}
