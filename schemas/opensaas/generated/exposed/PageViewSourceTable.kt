package opensaas.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PageViewSourceTable : CompositeIdTable("\"PageViewSource\"") {
    val name = text("name").entityId()
    val date = pgTimestamp("date", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }.entityId()
    val dailyStatsId =
        optReference(
            "dailyStatsId",
            DailyStatsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PageViewSource_dailyStatsId_fkey\"",
        )
    val visitors = integer("visitors")
    override val primaryKey = PrimaryKey(date, name, name = "\"PageViewSource_pkey\"")
}
