package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CounterEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CounterEntity>(CounterTable)

    var value by CounterTable.value
}
