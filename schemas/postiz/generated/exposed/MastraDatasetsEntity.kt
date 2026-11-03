package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraDatasetsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraDatasetsEntity>(MastraDatasetsTable)

    var name by MastraDatasetsTable.name
    var description by MastraDatasetsTable.description
    var metadata by MastraDatasetsTable.metadata
    var inputSchema by MastraDatasetsTable.inputSchema
    var groundTruthSchema by MastraDatasetsTable.groundTruthSchema
    var requestContextSchema by MastraDatasetsTable.requestContextSchema
    var tags by MastraDatasetsTable.tags
    var targetType by MastraDatasetsTable.targetType
    var targetIds by MastraDatasetsTable.targetIds
    var scorerIds by MastraDatasetsTable.scorerIds
    var version by MastraDatasetsTable.version
    var createdAt by MastraDatasetsTable.createdAt
    var updatedAt by MastraDatasetsTable.updatedAt
    var createdAtZ by MastraDatasetsTable.createdAtZ
    var updatedAtZ by MastraDatasetsTable.updatedAtZ
    var organizationId by MastraDatasetsTable.organizationId
    var projectId by MastraDatasetsTable.projectId
    var candidateKey by MastraDatasetsTable.candidateKey
    var candidateId by MastraDatasetsTable.candidateId
}
