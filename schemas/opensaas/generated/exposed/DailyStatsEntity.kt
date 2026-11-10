package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DailyStatsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DailyStatsEntity>(DailyStatsTable)

    var date by DailyStatsTable.date
    var totalViews by DailyStatsTable.totalViews
    var prevDayViewsChangePercent by DailyStatsTable.prevDayViewsChangePercent
    var userCount by DailyStatsTable.userCount
    var paidUserCount by DailyStatsTable.paidUserCount
    var userDelta by DailyStatsTable.userDelta
    var paidUserDelta by DailyStatsTable.paidUserDelta
    var totalRevenue by DailyStatsTable.totalRevenue
    var totalProfit by DailyStatsTable.totalProfit
    val sources by PageViewSourceEntity optionalReferrersOn PageViewSourceTable.dailyStatsId
}
