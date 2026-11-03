package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<Int>("users") {
    override val id = integer("id").autoIncrement().entityId()
    val uuid = javaUUID("uuid").clientDefault { UUID.randomUUID() }.uniqueIndex("users_uuid_key")
    val username = text("username").nullable()
    val name = text("name").nullable()
    val email = text("email").uniqueIndex("users_email_key")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    val bio = text("bio").nullable()
    val avatarUrl = text("avatarUrl").nullable()
    val timeZone = text("timeZone").default("Europe/London")
    val weekStart = text("weekStart").default("Sunday")
    val bufferTime = integer("bufferTime").default(0)
    val hideBranding = bool("hideBranding").default(false)
    val theme = text("theme").nullable()
    val appTheme = text("appTheme").nullable()
    val createdDate = pgTimestamp("created", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val trialEndsAt = pgTimestamp("trialEndsAt", 3).nullable()
    val lastActiveAt = pgTimestamp("lastActiveAt", 3).nullable()
    val defaultScheduleId = integer("defaultScheduleId").nullable()
    val completedOnboarding = bool("completedOnboarding").default(false)
    val locale = text("locale").nullable()
    val timeFormat = integer("timeFormat").nullable().default(12)
    val twoFactorSecret = text("twoFactorSecret").nullable()
    val twoFactorEnabled = bool("twoFactorEnabled").default(false)
    val backupCodes = text("backupCodes").nullable()
    val identityProvider =
        pgEnum("identityProvider", "\"IdentityProvider\"", IdentityProvider.entries, IdentityProvider::dbName).default(IdentityProvider.CAL)
    val identityProviderId = text("identityProviderId").nullable()
    val invitedTo = integer("invitedTo").nullable()
    val brandColor = text("brandColor").nullable()
    val darkBrandColor = text("darkBrandColor").nullable()
    val allowDynamicBooking = bool("allowDynamicBooking").nullable().default(true)
    val allowSEOIndexing = bool("allowSEOIndexing").nullable().default(true)
    val receiveMonthlyDigestEmail = bool("receiveMonthlyDigestEmail").nullable().default(true)
    val requiresBookerEmailVerification = bool("requiresBookerEmailVerification").nullable().default(false)
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val verified = bool("verified").nullable().default(false)
    val role =
        pgEnum("role", "\"UserPermissionRole\"", UserPermissionRole.entries, UserPermissionRole::dbName).default(UserPermissionRole.USER)
    val organizationId =
        optReference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"users_organizationId_fkey\"",
        )
    val locked = bool("locked").default(false)
    val movedToProfileId =
        optReference(
            "movedToProfileId",
            ProfileTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"users_movedToProfileId_fkey\"",
        ).uniqueIndex("\"users_movedToProfileId_key\"")
    val isPlatformManaged = bool("isPlatformManaged").default(false)
    val smsLockState = pgEnum("smsLockState", "\"SMSLockState\"", SMSLockState.entries, SMSLockState::dbName).default(SMSLockState.UNLOCKED)
    val smsLockReviewedByAdmin = bool("smsLockReviewedByAdmin").default(false)
    val referralLinkId = text("referralLinkId").nullable()
    val creationSource = pgEnum("creationSource", "\"CreationSource\"", CreationSource.entries, CreationSource::dbName).nullable()
    val autoOptInFeatures = bool("autoOptInFeatures").default(false)
    override val primaryKey = PrimaryKey(id, name = "users_pkey")

    init {
        index("users_username_idx", false, username)
        index("\"users_emailVerified_idx\"", false, emailVerified)
        index("\"users_identityProvider_idx\"", false, identityProvider)
        index("\"users_identityProviderId_idx\"", false, identityProviderId)
        uniqueIndex("users_email_username_key", email, username)
        uniqueIndex("\"users_username_organizationId_key\"", username, organizationId)
    }
}
