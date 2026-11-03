package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastActivityAt =
        pgTimestamp("lastActivityAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name").nullable()
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    val image = text("image").nullable()
    val company = text("company").nullable()
    val onboardingCategories = jsonb<String>("onboardingCategories", { it }, { it })
    val referral = text("referral").nullable()
    val graphNavigation = pgEnum("graphNavigation", "\"GraphNavigation\"", GraphNavigation.entries, GraphNavigation::dbName).nullable()
    val preferredAppAppearance = text("preferredAppAppearance").nullable()
    val displayedInAppNotifications = jsonb<String>("displayedInAppNotifications", { it }, { it }).nullable()
    val groupTitlesAutoGeneration = jsonb<String>("groupTitlesAutoGeneration", { it }, { it }).nullable()
    val preferredLanguage = text("preferredLanguage").nullable()
    val termsAcceptedAt = pgTimestamp("termsAcceptedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
