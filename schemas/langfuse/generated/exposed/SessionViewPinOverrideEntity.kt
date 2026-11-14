package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SessionViewPinOverrideEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionViewPinOverrideEntity>(SessionViewPinOverrideTable)

    var projectId by SessionViewPinOverrideTable.projectId
    var project by ProjectEntity referencedOn SessionViewPinOverrideTable.projectId
    var userId by SessionViewPinOverrideTable.userId
    var user by UserEntity referencedOn SessionViewPinOverrideTable.userId
    var viewId by SessionViewPinOverrideTable.viewId
    var isPinned by SessionViewPinOverrideTable.isPinned
    var position by SessionViewPinOverrideTable.position
}
