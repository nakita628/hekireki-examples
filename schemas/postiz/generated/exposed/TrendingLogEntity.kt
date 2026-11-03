package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TrendingLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TrendingLogEntity>(TrendingLogTable)

    var language by TrendingLogTable.language
    var date by TrendingLogTable.date
}
