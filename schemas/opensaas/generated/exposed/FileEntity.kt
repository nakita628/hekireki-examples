package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class FileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FileEntity>(FileTable)

    var createdAt by FileTable.createdAt
    var user by UserEntity referencedOn FileTable.userId
    var userId by FileTable.userId
    var name by FileTable.name
    var type by FileTable.type
    var s3Key by FileTable.s3Key
}
