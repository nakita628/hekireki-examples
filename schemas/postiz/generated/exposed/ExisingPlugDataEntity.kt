package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ExisingPlugDataEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExisingPlugDataEntity>(ExisingPlugDataTable)

    var integrationId by ExisingPlugDataTable.integrationId
    var methodName by ExisingPlugDataTable.methodName
    var value by ExisingPlugDataTable.value
    var integration by IntegrationEntity referencedOn ExisingPlugDataTable.integrationId
}
