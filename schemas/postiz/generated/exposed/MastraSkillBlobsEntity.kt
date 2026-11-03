package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraSkillBlobsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraSkillBlobsEntity>(MastraSkillBlobsTable)

    var content by MastraSkillBlobsTable.content
    var size by MastraSkillBlobsTable.size
    var mimeType by MastraSkillBlobsTable.mimeType
    var createdAt by MastraSkillBlobsTable.createdAt
    var createdAtZ by MastraSkillBlobsTable.createdAtZ
}
