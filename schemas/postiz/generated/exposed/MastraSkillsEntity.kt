package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraSkillsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraSkillsEntity>(MastraSkillsTable)

    var status by MastraSkillsTable.status
    var activeVersionId by MastraSkillsTable.activeVersionId
    var authorId by MastraSkillsTable.authorId
    var createdAt by MastraSkillsTable.createdAt
    var updatedAt by MastraSkillsTable.updatedAt
    var createdAtZ by MastraSkillsTable.createdAtZ
    var updatedAtZ by MastraSkillsTable.updatedAtZ
    var visibility by MastraSkillsTable.visibility
    var favoriteCount by MastraSkillsTable.favoriteCount
}
