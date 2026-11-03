package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class CreativeDailyPageStatEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<CreativeDailyPageStatEntity>(CreativeDailyPageStatTable)

    val creative by CreativeEntity referencedOn CreativeDailyPageStatTable.creativeId
    val creativeId by CreativeDailyPageStatTable.creativeId
    val date by CreativeDailyPageStatTable.date
    val page by CreativeDailyPageStatTable.page
    var impressions by CreativeDailyPageStatTable.impressions
    var clicks by CreativeDailyPageStatTable.clicks
}
