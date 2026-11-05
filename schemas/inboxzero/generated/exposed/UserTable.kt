package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name").nullable()
    val email = text("email").uniqueIndex("\"User_email_key\"")
    val emailVerified = bool("emailVerified").nullable().default(false)
    val image = text("image").nullable()
    val completedOnboardingAt = pgTimestamp("completedOnboardingAt", 3).nullable()
    val signupIntent = pgEnum("signupIntent", "\"SignupIntent\"", SignupIntent.entries, SignupIntent::dbName).nullable()
    val onboardingAnswers = jsonb<String>("onboardingAnswers", { it }, { it }).nullable()
    val lastLogin = pgTimestamp("lastLogin", 3).nullable()
    val utms = jsonb<String>("utms", { it }, { it }).nullable()
    val errorMessages = jsonb<String>("errorMessages", { it }, { it }).nullable()
    val announcementDismissedAt = pgTimestamp("announcementDismissedAt", 3).nullable()
    val dismissedHints = array("dismissedHints", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val surveyFeatures = array("surveyFeatures", TextColumnType()).nullable()
    val surveyRole = text("surveyRole").nullable()
    val surveyGoal = text("surveyGoal").nullable()
    val surveyCompanySize = integer("surveyCompanySize").nullable()
    val surveySource = text("surveySource").nullable()
    val surveyImprovements = text("surveyImprovements").nullable()
    val mcpServerEnabled = bool("mcpServerEnabled").default(false)
    val mcpTokenVersion = integer("mcpTokenVersion").default(0)
    val emailOtpEnabled = bool("emailOtpEnabled").default(false)
    val emailOtpVersion = integer("emailOtpVersion").default(0)
    val aiProvider = text("aiProvider").nullable()
    val aiModel = text("aiModel").nullable()
    val aiApiKey = text("aiApiKey").nullable()
    val decisionModelEnabled = bool("decisionModelEnabled").nullable()
    val webhookSecret = text("webhookSecret").nullable()
    val referralCode = text("referralCode").nullable().uniqueIndex("\"User_referralCode_key\"")
    val premiumId =
        optReference(
            "premiumId",
            PremiumTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_premiumId_fkey\"",
        )
    val premiumAdminId =
        optReference(
            "premiumAdminId",
            PremiumTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_premiumAdminId_fkey\"",
        )
    val scimAccessDisabled = bool("scimAccessDisabled").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
