package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PlatformEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformEntity>(PlatformTable)

    val accounts by AccountEntity optionalReferrersOn AccountTable.platformId
    var name by PlatformTable.name
    var url by PlatformTable.url
}
