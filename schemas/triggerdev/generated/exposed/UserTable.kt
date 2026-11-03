package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val email = text("email").uniqueIndex("\"User_email_key\"")
    val pendingEmail = text("pendingEmail").nullable()
    val authenticationMethod =
        pgEnum("authenticationMethod", "\"AuthenticationMethod\"", AuthenticationMethod.entries, AuthenticationMethod::dbName)
    val authenticationProfile = jsonb<String>("authenticationProfile", { it }, { it }).nullable()
    val authenticationExtraParams = jsonb<String>("authenticationExtraParams", { it }, { it }).nullable()
    val authIdentifier = text("authIdentifier").nullable().uniqueIndex("\"User_authIdentifier_key\"")
    val displayName = text("displayName").nullable()
    val name = text("name").nullable()
    val avatarUrl = text("avatarUrl").nullable()
    val admin = bool("admin").default(false)
    val dashboardPreferences = jsonb<String>("dashboardPreferences", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val isOnCloudWaitlist = bool("isOnCloudWaitlist").default(false)
    val featureCloud = bool("featureCloud").default(false)
    val isOnHostedRepoWaitlist = bool("isOnHostedRepoWaitlist").default(false)
    val marketingEmails = bool("marketingEmails").default(true)
    val confirmedBasicDetails = bool("confirmedBasicDetails").default(false)
    val referralSource = text("referralSource").nullable()
    val onboardingData = jsonb<String>("onboardingData", { it }, { it }).nullable()
    val mfaEnabledAt = pgTimestamp("mfaEnabledAt", 3).nullable()
    val mfaSecretReferenceId =
        optReference(
            "mfaSecretReferenceId",
            SecretReferenceTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_mfaSecretReferenceId_fkey\"",
        )
    val mfaLastUsedCode = text("mfaLastUsedCode").nullable()
    val sessionDuration = integer("sessionDuration").default(31556952)
    val nextSessionEnd = pgTimestamp("nextSessionEnd", 3).nullable()
    val invitationCodeId =
        optReference(
            "invitationCodeId",
            InvitationCodeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_invitationCodeId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
