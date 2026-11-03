package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamTable : IdTable<Int>("\"Team\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val slug = text("slug").nullable()
    val logoUrl = text("logoUrl").nullable()
    val calVideoLogo = text("calVideoLogo").nullable()
    val appLogo = text("appLogo").nullable()
    val appIconLogo = text("appIconLogo").nullable()
    val bio = text("bio").nullable()
    val hideBranding = bool("hideBranding").default(false)
    val hideTeamProfileLink = bool("hideTeamProfileLink").default(false)
    val isPrivate = bool("isPrivate").default(false)
    val hideBookATeamMember = bool("hideBookATeamMember").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val theme = text("theme").nullable()
    val rrResetInterval =
        pgEnum("rrResetInterval", "\"RRResetInterval\"", RRResetInterval.entries, RRResetInterval::dbName)
            .nullable()
            .default(RRResetInterval.MONTH)
    val rrTimestampBasis =
        pgEnum("rrTimestampBasis", "\"RRTimestampBasis\"", RRTimestampBasis.entries, RRTimestampBasis::dbName).default(
            RRTimestampBasis.CREATED_AT,
        )
    val brandColor = text("brandColor").nullable()
    val darkBrandColor = text("darkBrandColor").nullable()
    val bannerUrl = text("bannerUrl").nullable()
    val parentId =
        optReference(
            "parentId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_parentId_fkey\"",
        )
    val timeFormat = integer("timeFormat").nullable()
    val timeZone = text("timeZone").default("Europe/London")
    val weekStart = text("weekStart").default("Sunday")
    val isOrganization = bool("isOrganization").default(false)
    val pendingPayment = bool("pendingPayment").default(false)
    val isPlatform = bool("isPlatform").default(false)
    val createdByOAuthClientId =
        optReference(
            "createdByOAuthClientId",
            PlatformOAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_createdByOAuthClientId_fkey\"",
        )
    val smsLockState = pgEnum("smsLockState", "\"SMSLockState\"", SMSLockState.entries, SMSLockState::dbName).default(SMSLockState.UNLOCKED)
    val smsLockReviewedByAdmin = bool("smsLockReviewedByAdmin").default(false)
    val bookingLimits = jsonb<String>("bookingLimits", { it }, { it }).nullable()
    val includeManagedEventsInLimits = bool("includeManagedEventsInLimits").default(false)
    val autoOptInFeatures = bool("autoOptInFeatures").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"Team_pkey\"")

    init {
        index("\"Team_parentId_idx\"", false, parentId)
        uniqueIndex("\"Team_slug_parentId_key\"", slug, parentId)
    }
}
