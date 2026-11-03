package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class CreativeDailyStatEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<CreativeDailyStatEntity>(CreativeDailyStatTable)

    val creative by CreativeEntity referencedOn CreativeDailyStatTable.creativeId
    val creativeId by CreativeDailyStatTable.creativeId
    val date by CreativeDailyStatTable.date
    val deviceType by CreativeDailyStatTable.deviceType
    var impressions by CreativeDailyStatTable.impressions
    var clicks by CreativeDailyStatTable.clicks
}
