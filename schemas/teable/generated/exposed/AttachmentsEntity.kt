package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AttachmentsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttachmentsEntity>(AttachmentsTable)

    var token by AttachmentsTable.token
    var hash by AttachmentsTable.hash
    var size by AttachmentsTable.size
    var mimetype by AttachmentsTable.mimetype
    var path by AttachmentsTable.path
    var width by AttachmentsTable.width
    var height by AttachmentsTable.height
    var deletedTime by AttachmentsTable.deletedTime
    var createdTime by AttachmentsTable.createdTime
    var createdBy by AttachmentsTable.createdBy
    var lastModifiedBy by AttachmentsTable.lastModifiedBy
    var thumbnailPath by AttachmentsTable.thumbnailPath
}
