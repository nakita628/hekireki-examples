package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DailyStatsTable : IdTable<Int>("\"DailyStats\"") {
    override val id = integer("id").autoIncrement().entityId()
    val date =
        pgTimestamp("date", 3)
            .databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
            .uniqueIndex("\"DailyStats_date_key\"")
    val totalViews = integer("totalViews").default(0)
    val prevDayViewsChangePercent = text("prevDayViewsChangePercent").default("0")
    val userCount = integer("userCount").default(0)
    val paidUserCount = integer("paidUserCount").default(0)
    val userDelta = integer("userDelta").default(0)
    val paidUserDelta = integer("paidUserDelta").default(0)
    val totalRevenue = double("totalRevenue").default(0.0)
    val totalProfit = double("totalProfit").default(0.0)
    override val primaryKey = PrimaryKey(id, name = "\"DailyStats_pkey\"")
}
