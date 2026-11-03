package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WaitlistTable : IdTable<String>("waitlist") {
    override val id = text("email").entityId()
    val invite = bool("invite").nullable()
    val inviteTime = pgTimestamp("invite_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("waitlist_email_key", id)
    }
}
