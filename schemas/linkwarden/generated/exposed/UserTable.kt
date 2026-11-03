package linkwarden.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<Int>("\"User\"") {
    override val id = integer("id").autoIncrement().entityId()
    val uuid = javaUUID("uuid").clientDefault { UUID.randomUUID() }.uniqueIndex("\"User_uuid_key\"")
    val name = text("name").nullable()
    val username = text("username").nullable().uniqueIndex("\"User_username_key\"")
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    val unverifiedNewEmail = text("unverifiedNewEmail").nullable()
    val image = text("image").nullable()
    val password = text("password").nullable()
    val locale = text("locale").default("en")
    val parentSubscriptionId =
        optReference(
            "parentSubscriptionId",
            SubscriptionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_parentSubscriptionId_fkey\"",
        )
    val collectionOrder = array("collectionOrder", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val linksRouteTo = pgEnum("linksRouteTo", "\"LinksRouteTo\"", LinksRouteTo.entries, LinksRouteTo::dbName).default(LinksRouteTo.ORIGINAL)
    val aiTaggingMethod =
        pgEnum("aiTaggingMethod", "\"AiTaggingMethod\"", AiTaggingMethod.entries, AiTaggingMethod::dbName).default(AiTaggingMethod.DISABLED)
    val aiPredefinedTags = array("aiPredefinedTags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val aiTagExistingLinks = bool("aiTagExistingLinks").default(false)
    val theme = pgEnum("theme", "\"Theme\"", Theme.entries, Theme::dbName).default(Theme.DARK)
    val dismissedAnnouncementId = text("dismissedAnnouncementId").nullable()
    val readableFontFamily = text("readableFontFamily").nullable().default("sans-serif")
    val readableFontSize = text("readableFontSize").nullable().default("20px")
    val readableLineHeight = text("readableLineHeight").nullable().default("1.8")
    val readableLineWidth = text("readableLineWidth").nullable().default("normal")
    val preventDuplicateLinks = bool("preventDuplicateLinks").default(false)
    val archiveAsScreenshot = bool("archiveAsScreenshot").default(true)
    val archiveAsMonolith = bool("archiveAsMonolith").default(true)
    val archiveAsPDF = bool("archiveAsPDF").default(true)
    val archiveAsReadable = bool("archiveAsReadable").default(true)
    val archiveAsWaybackMachine = bool("archiveAsWaybackMachine").default(false)
    val referredBy = text("referredBy").nullable()
    val lastPickedAt = pgTimestamp("lastPickedAt", 3).nullable()
    val acceptPromotionalEmails = bool("acceptPromotionalEmails").default(false)
    val trialEndEmailSent = bool("trialEndEmailSent").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
