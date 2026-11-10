package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class PageViewSourceEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<PageViewSourceEntity>(PageViewSourceTable)

    val name by PageViewSourceTable.name
    val date by PageViewSourceTable.date
    var dailyStats by DailyStatsEntity optionalReferencedOn PageViewSourceTable.dailyStatsId
    var dailyStatsId by PageViewSourceTable.dailyStatsId
    var visitors by PageViewSourceTable.visitors
}
