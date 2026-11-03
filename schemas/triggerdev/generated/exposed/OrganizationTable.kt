package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("\"Organization\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val slug = text("slug").uniqueIndex("\"Organization_slug_key\"")
    val title = text("title")
    val maximumExecutionTimePerRunInMs = integer("maximumExecutionTimePerRunInMs").default(900000)
    val maximumConcurrencyLimit = integer("maximumConcurrencyLimit").default(10)
    val maximumSchedulesLimit = integer("maximumSchedulesLimit").default(5)
    val maximumDevQueueSize = integer("maximumDevQueueSize").nullable()
    val maximumDeployedQueueSize = integer("maximumDeployedQueueSize").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val companySize = text("companySize").nullable()
    val onboardingData = jsonb<String>("onboardingData", { it }, { it }).nullable()
    val avatar = jsonb<String>("avatar", { it }, { it }).nullable()
    val runsEnabled = bool("runsEnabled").default(true)
    val isActivated = bool("v3Enabled").default(false)
    val v2Enabled = bool("v2Enabled").default(false)
    val v2MarqsEnabled = bool("v2MarqsEnabled").default(false)
    val hasRequestedV3 = bool("hasRequestedV3").default(false)
    val apiRateLimiterConfig = jsonb<String>("apiRateLimiterConfig", { it }, { it }).nullable()
    val realtimeRateLimiterConfig = jsonb<String>("realtimeRateLimiterConfig", { it }, { it }).nullable()
    val batchRateLimitConfig = jsonb<String>("batchRateLimitConfig", { it }, { it }).nullable()
    val batchQueueConcurrencyConfig = jsonb<String>("batchQueueConcurrencyConfig", { it }, { it }).nullable()
    val webhookLimitsConfig = jsonb<String>("webhookLimitsConfig", { it }, { it }).nullable()
    val featureFlags = jsonb<String>("featureFlags", { it }, { it }).nullable()
    val maximumProjectCount = integer("maximumProjectCount").default(25)
    val maxSessionDuration = integer("maxSessionDuration").nullable()
    val streamBasinName = text("streamBasinName").nullable()
    val supportAccessMode =
        pgEnum("supportAccessMode", "\"SupportAccessMode\"", SupportAccessMode.entries, SupportAccessMode::dbName).default(
            SupportAccessMode.ALLOW,
        )
    val supportAccessModeUpdatedAt = pgTimestamp("supportAccessModeUpdatedAt", 3).nullable()
    val supportAccessModeUpdatedById = text("supportAccessModeUpdatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Organization_pkey\"")
}
