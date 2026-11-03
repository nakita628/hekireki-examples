package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CustomerQueryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomerQueryEntity>(CustomerQueryTable)

    var query by CustomerQueryTable.query
    var scope by CustomerQueryTable.scope
    var stats by CustomerQueryTable.stats
    var title by CustomerQueryTable.title
    var source by CustomerQueryTable.source1
    var organization by OrganizationEntity referencedOn CustomerQueryTable.organizationId
    var organizationId by CustomerQueryTable.organizationId
    var project by ProjectEntity optionalReferencedOn CustomerQueryTable.projectId
    var projectId by CustomerQueryTable.projectId
    var environment by RuntimeEnvironmentEntity optionalReferencedOn CustomerQueryTable.environmentId
    var environmentId by CustomerQueryTable.environmentId
    var user by UserEntity optionalReferencedOn CustomerQueryTable.userId
    var userId by CustomerQueryTable.userId
    var filterPeriod by CustomerQueryTable.filterPeriod
    var filterFrom by CustomerQueryTable.filterFrom
    var filterTo by CustomerQueryTable.filterTo
    var createdAt by CustomerQueryTable.createdAt
}
