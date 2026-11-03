package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("users") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val email = pgCitext("email").uniqueIndex("users_email_key")
    val emailVerified = bool("email_verified").nullable()
    val image = text("image").nullable()
    val timeZone = text("time_zone").nullable()
    val weekStart = integer("week_start").nullable()
    val timeFormat = pgEnum("time_format", "\"time_format\"", TimeFormat.entries, TimeFormat::dbName).nullable()
    val locale = text("locale").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val customerId = text("customer_id").nullable()
    val banned = bool("banned").default(false)
    val bannedAt = pgTimestamp("banned_at", 3).nullable()
    val banReason = text("ban_reason").nullable()
    val banExpires = pgTimestamp("ban_expires", 3).nullable()
    val role = pgEnum("role", "\"user_role\"", UserRole.entries, UserRole::dbName).default(UserRole.USER)
    val lastLoginMethod = text("last_login_method").nullable()
    val isAnonymous = bool("anonymous").default(false)
    val lastSeenAt = pgTimestamp("last_seen_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    val jobTitle = text("job_title").nullable()
    val reviewRequestedAt = pgTimestamp("review_requested_at", 3).nullable()
    val defaultDestinationCalendarId =
        optReference(
            "default_destination_calendar_id",
            ProviderCalendarTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "users_default_destination_calendar_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "users_pkey")

    init {
        index("users_deleted_at_idx", false, deletedAt)
        index("users_anonymous_last_seen_at_idx", false, isAnonymous, lastSeenAt)
    }
}
