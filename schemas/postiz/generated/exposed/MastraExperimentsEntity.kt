package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraExperimentsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraExperimentsEntity>(MastraExperimentsTable)

    var name by MastraExperimentsTable.name
    var description by MastraExperimentsTable.description
    var metadata by MastraExperimentsTable.metadata
    var datasetId by MastraExperimentsTable.datasetId
    var datasetVersion by MastraExperimentsTable.datasetVersion
    var targetType by MastraExperimentsTable.targetType
    var targetId by MastraExperimentsTable.targetId
    var status by MastraExperimentsTable.status
    var totalItems by MastraExperimentsTable.totalItems
    var succeededCount by MastraExperimentsTable.succeededCount
    var failedCount by MastraExperimentsTable.failedCount
    var skippedCount by MastraExperimentsTable.skippedCount
    var startedAt by MastraExperimentsTable.startedAt
    var completedAt by MastraExperimentsTable.completedAt
    var agentVersion by MastraExperimentsTable.agentVersion
    var createdAt by MastraExperimentsTable.createdAt
    var updatedAt by MastraExperimentsTable.updatedAt
    var startedAtZ by MastraExperimentsTable.startedAtZ
    var completedAtZ by MastraExperimentsTable.completedAtZ
    var createdAtZ by MastraExperimentsTable.createdAtZ
    var updatedAtZ by MastraExperimentsTable.updatedAtZ
    var organizationId by MastraExperimentsTable.organizationId
    var projectId by MastraExperimentsTable.projectId
    var provenance by MastraExperimentsTable.provenance
    var runnerAttestation by MastraExperimentsTable.runnerAttestation
    var experimentSetId by MastraExperimentsTable.experimentSetId
    var comparisonId by MastraExperimentsTable.comparisonId
    var variantId by MastraExperimentsTable.variantId
    var trialIndex by MastraExperimentsTable.trialIndex
    var scorerIds by MastraExperimentsTable.scorerIds
}
