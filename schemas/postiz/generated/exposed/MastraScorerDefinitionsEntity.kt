package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraScorerDefinitionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraScorerDefinitionsEntity>(MastraScorerDefinitionsTable)

    var status by MastraScorerDefinitionsTable.status
    var activeVersionId by MastraScorerDefinitionsTable.activeVersionId
    var authorId by MastraScorerDefinitionsTable.authorId
    var metadata by MastraScorerDefinitionsTable.metadata
    var createdAt by MastraScorerDefinitionsTable.createdAt
    var updatedAt by MastraScorerDefinitionsTable.updatedAt
    var createdAtZ by MastraScorerDefinitionsTable.createdAtZ
    var updatedAtZ by MastraScorerDefinitionsTable.updatedAtZ
    var organizationId by MastraScorerDefinitionsTable.organizationId
    var projectId by MastraScorerDefinitionsTable.projectId
}
