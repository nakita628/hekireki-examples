package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PlatformNotificationTable : IdTable<String>("\"PlatformNotification\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId =
        text("friendlyId").clientDefault { CUID.randomCUID1().toString() }.uniqueIndex("\"PlatformNotification_friendlyId_key\"")
    val title = text("title")
    val payload = jsonb<String>("payload", { it }, { it })
    val surface =
        pgEnum("surface", "\"PlatformNotificationSurface\"", PlatformNotificationSurface.entries, PlatformNotificationSurface::dbName)
    val scope = pgEnum("scope", "\"PlatformNotificationScope\"", PlatformNotificationScope.entries, PlatformNotificationScope::dbName)
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformNotification_userId_fkey\"",
        )
    val organizationId =
        optReference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformNotification_organizationId_fkey\"",
        )
    val projectId =
        optReference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformNotification_projectId_fkey\"",
        )
    val startsAt = pgTimestamp("startsAt", 3)
    val endsAt = pgTimestamp("endsAt", 3)
    val cliMaxDaysAfterFirstSeen = integer("cliMaxDaysAfterFirstSeen").nullable()
    val cliMaxShowCount = integer("cliMaxShowCount").nullable()
    val cliShowEvery = integer("cliShowEvery").nullable()
    val priority = integer("priority").default(0)
    val isDraft = bool("isDraft").default(false)
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PlatformNotification_pkey\"")

    init {
        index("\"PlatformNotification_surface_scope_startsAt_idx\"", false, surface, scope, startsAt)
    }
}
