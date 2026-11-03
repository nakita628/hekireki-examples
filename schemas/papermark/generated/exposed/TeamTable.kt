package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamTable : IdTable<String>("\"Team\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val slug = text("slug").nullable().uniqueIndex("\"Team_slug_key\"")
    val defaultBrandId =
        optReference(
            "defaultBrandId",
            BrandTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Team_defaultBrandId_fkey\"",
        )
    val plan = text("plan").default("free")
    val stripeId = text("stripeId").nullable().uniqueIndex("\"Team_stripeId_key\"")
    val subscriptionId = text("subscriptionId").nullable().uniqueIndex("\"Team_subscriptionId_key\"")
    val startsAt = pgTimestamp("startsAt", 3).nullable()
    val endsAt = pgTimestamp("endsAt", 3).nullable()
    val pausedAt = pgTimestamp("pausedAt", 3).nullable()
    val pauseStartsAt = pgTimestamp("pauseStartsAt", 3).nullable()
    val pauseEndsAt = pgTimestamp("pauseEndsAt", 3).nullable()
    val cancelledAt = pgTimestamp("cancelledAt", 3).nullable()
    val trialStartedAt = pgTimestamp("trialStartedAt", 3).nullable()
    val trialEndsAt = pgTimestamp("trialEndsAt", 3).nullable()
    val trialReminderRunId = text("trialReminderRunId").nullable()
    val trialExpiredRunId = text("trialExpiredRunId").nullable()
    val limits = jsonb<String>("limits", { it }, { it }).nullable()
    val enableExcelAdvancedMode = bool("enableExcelAdvancedMode").default(false)
    val replicateDataroomFolders = bool("replicateDataroomFolders").default(true)
    val timezone = text("timezone").default("Etc/UTC")
    val ssoEnabled = bool("ssoEnabled").default(false)
    val ssoEmailDomain = text("ssoEmailDomain").nullable().uniqueIndex("\"Team_ssoEmailDomain_key\"")
    val ssoEnforcedAt = pgTimestamp("ssoEnforcedAt", 3).nullable()
    val surveyData = jsonb<String>("surveyData", { it }, { it }).nullable()
    val agentsEnabled = bool("agentsEnabled").default(false)
    val vectorStoreId = text("vectorStoreId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ignoredDomains = array("ignoredDomains", TextColumnType()).nullable()
    val globalBlockList = array("globalBlockList", TextColumnType()).nullable()
    val referralTrack = pgEnum("referralTrack", "\"ReferralTrack\"", ReferralTrack.entries, ReferralTrack::dbName).nullable()
    val referralTrackSelectedAt = pgTimestamp("referralTrackSelectedAt", 3).nullable()
    val referralProfile = jsonb<String>("referralProfile", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Team_pkey\"")

    init {
        index("\"Team_defaultBrandId_idx\"", false, defaultBrandId)
    }
}
