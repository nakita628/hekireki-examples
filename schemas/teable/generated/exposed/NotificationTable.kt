package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NotificationTable : IdTable<String>("notification") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val fromUserId = text("from_user_id")
    val toUserId = text("to_user_id")
    val type = text("type")
    val message = text("message")
    val messageI18n = text("message_i18n").nullable()
    val severity = text("severity").default("info")
    val urlPath = text("url_path").nullable()
    val isRead = bool("is_read").default(false)
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    override val primaryKey = PrimaryKey(id, name = "notification_pkey")

    init {
        index("notification_to_user_id_is_read_created_time_idx", false, toUserId, isRead, createdTime)
        index("notification_created_time_idx", false, createdTime)
    }
}
