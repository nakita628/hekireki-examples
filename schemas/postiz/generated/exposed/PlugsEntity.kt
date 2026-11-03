package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PlugsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlugsEntity>(PlugsTable)

    var organizationId by PlugsTable.organizationId
    var plugFunction by PlugsTable.plugFunction
    var data by PlugsTable.data
    var integrationId by PlugsTable.integrationId
    var activated by PlugsTable.activated
    var integration by IntegrationEntity referencedOn PlugsTable.integrationId
    var organization by OrganizationEntity referencedOn PlugsTable.organizationId
}
