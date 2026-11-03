package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NotificationPreferenceTable : IdTable<String>("notification_preferences") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "notification_preferences_user_id_fkey",
        )
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "notification_preferences_project_id_fkey",
        )
    val channel = pgEnum("channel", "\"NotificationChannel\"", NotificationChannel.entries, NotificationChannel::dbName)
    val type = pgEnum("type", "\"NotificationType\"", NotificationType.entries, NotificationType::dbName)
    val enabled = bool("enabled").default(true)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "notification_preferences_pkey")

    init {
        uniqueIndex("notification_preferences_user_id_project_id_channel_type_key", userId, projectId, channel, type)
    }
}
