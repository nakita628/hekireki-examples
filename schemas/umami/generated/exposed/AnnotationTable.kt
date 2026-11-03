package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AnnotationTable : IdTable<UUID>("annotation") {
    override val id = javaUUID("annotation_id").entityId()
    val websiteId = javaUUID("website_id")
    val userId = javaUUID("user_id").nullable()
    val date = pgTimestamptz("date", 6)
    val allDay = bool("all_day").default(true)
    val note = varchar("note", 500)
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "annotation_pkey")

    init {
        index("annotation_website_id_date_idx", false, websiteId, date)
    }
}
