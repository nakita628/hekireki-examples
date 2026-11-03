package calcom.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PlatformOAuthClientTable : IdTable<String>("\"PlatformOAuthClient\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val secret = text("secret")
    val permissions = integer("permissions")
    val logo = text("logo").nullable()
    val redirectUris = array("redirectUris", TextColumnType()).nullable()
    val organizationId =
        reference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformOAuthClient_organizationId_fkey\"",
        )
    val bookingRedirectUri = text("bookingRedirectUri").nullable()
    val bookingCancelRedirectUri = text("bookingCancelRedirectUri").nullable()
    val bookingRescheduleRedirectUri = text("bookingRescheduleRedirectUri").nullable()
    val areEmailsEnabled = bool("areEmailsEnabled").default(false)
    val areDefaultEventTypesEnabled = bool("areDefaultEventTypesEnabled").default(true)
    val areCalendarEventsEnabled = bool("areCalendarEventsEnabled").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PlatformOAuthClient_pkey\"")
}
