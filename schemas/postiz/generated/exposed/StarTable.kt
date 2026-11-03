package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.LocalDate
import java.time.ZoneOffset
import java.time.temporal.ChronoUnit
import java.util.UUID

object StarTable : IdTable<String>("\"Star\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val stars = integer("stars")
    val totalStars = integer("totalStars")
    val forks = integer("forks")
    val totalForks = integer("totalForks")
    val login = text("login")
    val date = pgDate("date").databaseDefault("CURRENT_TIMESTAMP") { LocalDate.now(ZoneOffset.UTC) }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Star_pkey\"")

    init {
        uniqueIndex("\"Star_login_date_key\"", login, date)
    }
}
