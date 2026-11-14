package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name").nullable()
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val emailVerified = bool("emailVerified").default(false)
    val signupConfirmationPending = bool("signupConfirmationPending").default(false)
    val passkeySignupClaimHash = text("passkeySignupClaimHash").nullable()
    val image = text("image").nullable()
    val pendingSsoSetup = bool("pendingSsoSetup").default(false)
    val userHashKey = text("userHashKey").nullable()
    val twoFactorEnabled = bool("twoFactorEnabled").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastLoginAt = pgTimestamp("lastLoginAt", 3).nullable()
    val deactivatedAt = pgTimestamp("deactivatedAt", 3).nullable()
    val lastHomePath = text("lastHomePath").nullable()
    val tracesExplorerTourDismissedAt = pgTimestamp("tracesExplorerTourDismissedAt", 3).nullable()
    val langyCodeAccessPreference = text("langyCodeAccessPreference").nullable()
    val passkeyNudgeDismissedAt = pgTimestamp("passkeyNudgeDismissedAt", 3).nullable()
    val joinOfferDismissedDomains = array("joinOfferDismissedDomains", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val notificationPreferences = jsonb<String>("notificationPreferences", { it }, { it }).default("{}")
    val passwordResetRequiredAt = pgTimestamp("passwordResetRequiredAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
