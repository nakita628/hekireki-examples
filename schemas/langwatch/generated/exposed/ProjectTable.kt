package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProjectTable : IdTable<String>("\"Project\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val slug = text("slug").uniqueIndex("\"Project_slug_key\"")
    val apiKey = text("apiKey").uniqueIndex("\"Project_apiKey_key\"")
    val legacyApiKeyLastUsedAt = pgTimestamp("legacyApiKeyLastUsedAt", 3).nullable()
    val lwqlKey = text("lwqlKey").databaseDefault("(gen_random_uuid())::text").uniqueIndex("\"Project_lwqlKey_key\"")
    val teamId = text("teamId")
    val language = text("language")
    val framework = text("framework")
    val kind = text("kind").default("application")
    val aggregateRule = jsonb<String>("aggregateRule", { it }, { it }).nullable()
    val firstMessage = bool("firstMessage").default(false)
    val integrated = bool("integrated").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userLinkTemplate = text("userLinkTemplate").nullable()
    val traceSharingEnabled = bool("traceSharingEnabled").default(true)
    val presenceEnabled = bool("presenceEnabled").default(true)
    val s3Endpoint = text("s3Endpoint").nullable()
    val s3AccessKeyId = text("s3AccessKeyId").nullable()
    val s3SecretAccessKey = text("s3SecretAccessKey").nullable()
    val s3Bucket = text("s3Bucket").nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val isPersonal = bool("isPersonal").default(false)
    val ownerUserId = varchar("ownerUserId", 255).nullable()
    val personalFeatures = jsonb<String>("personalFeatures", { it }, { it }).default("{}")
    val departmentId = text("departmentId").nullable()
    val langyEgressAllowlist = jsonb<String>("langyEgressAllowlist", { it }, { it }).nullable()
    val lastCodingAgentSessionAt = pgTimestamp("lastCodingAgentSessionAt", 3).nullable()
    val lastCodingAgentPullRequestAt = pgTimestamp("lastCodingAgentPullRequestAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Project_pkey\"")

    init {
        index("\"Project_teamId_idx\"", false, teamId)
        index("\"Project_isPersonal_idx\"", false, isPersonal)
        index("\"Project_ownerUserId_idx\"", false, ownerUserId)
        index("\"Project_teamId_kind_idx\"", false, teamId, kind)
        index("\"Project_departmentId_idx\"", false, departmentId)
    }
}
