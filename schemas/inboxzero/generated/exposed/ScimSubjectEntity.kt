package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimSubjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimSubjectEntity>(ScimSubjectTable)

    var userId by ScimSubjectTable.userId
    var user by UserEntity referencedOn ScimSubjectTable.userId
    var profileSourceId by ScimSubjectTable.profileSourceId
    var revision by ScimSubjectTable.revision
    var createdAt by ScimSubjectTable.createdAt
    var updatedAt by ScimSubjectTable.updatedAt
}
