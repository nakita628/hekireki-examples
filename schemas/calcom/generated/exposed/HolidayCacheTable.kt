package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object HolidayCacheTable : IdTable<String>("\"HolidayCache\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val countryCode = text("countryCode")
    val calendarId = text("calendarId")
    val eventId = text("eventId")
    val name = text("name")
    val date = pgTimestamp("date", 3)
    val year = integer("year")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"HolidayCache_pkey\"")

    init {
        index("\"HolidayCache_countryCode_year_idx\"", false, countryCode, year)
        index("\"HolidayCache_countryCode_date_idx\"", false, countryCode, date)
        uniqueIndex("\"HolidayCache_countryCode_eventId_key\"", countryCode, eventId)
    }
}
