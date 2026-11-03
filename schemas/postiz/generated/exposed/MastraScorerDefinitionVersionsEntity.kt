package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraScorerDefinitionVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraScorerDefinitionVersionsEntity>(MastraScorerDefinitionVersionsTable)

    var scorerDefinitionId by MastraScorerDefinitionVersionsTable.scorerDefinitionId
    var versionNumber by MastraScorerDefinitionVersionsTable.versionNumber
    var name by MastraScorerDefinitionVersionsTable.name
    var description by MastraScorerDefinitionVersionsTable.description
    var type by MastraScorerDefinitionVersionsTable.type
    var model by MastraScorerDefinitionVersionsTable.model
    var instructions by MastraScorerDefinitionVersionsTable.instructions
    var scoreRange by MastraScorerDefinitionVersionsTable.scoreRange
    var presetConfig by MastraScorerDefinitionVersionsTable.presetConfig
    var defaultSampling by MastraScorerDefinitionVersionsTable.defaultSampling
    var changedFields by MastraScorerDefinitionVersionsTable.changedFields
    var changeMessage by MastraScorerDefinitionVersionsTable.changeMessage
    var createdAt by MastraScorerDefinitionVersionsTable.createdAt
    var createdAtZ by MastraScorerDefinitionVersionsTable.createdAtZ
}
