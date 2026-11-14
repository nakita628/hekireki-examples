package langwatch.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("\"Organization\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name")
    val kind = text("kind").default("customer")
    val phoneNumber = text("phoneNumber").nullable()
    val slug = text("slug").uniqueIndex("\"Organization_slug_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val usageSpendingMaxLimit = integer("usageSpendingMaxLimit").nullable()
    val datasetAttachmentMaxMb = integer("datasetAttachmentMaxMb").nullable()
    val privateClickHouseLabel = text("privateClickHouseLabel").nullable()
    val privateS3Label = text("privateS3Label").nullable()
    val privateDataplaneSeenAt = pgTimestamp("privateDataplaneSeenAt", 3).nullable()
    val maxSessionDurationDays = integer("maxSessionDurationDays").default(0)
    val mfaRequired = bool("mfaRequired").default(false)
    val lockoutAfterFailedAttempts = integer("lockoutAfterFailedAttempts").default(0)
    val lockoutMinutes = integer("lockoutMinutes").default(30)
    val sessionIdleTimeoutMinutes = integer("sessionIdleTimeoutMinutes").default(1440)
    val sessionMaxLifetimeMinutes = integer("sessionMaxLifetimeMinutes").default(0)
    val signupData = jsonb<String>("signupData", { it }, { it }).nullable()
    val signedDPA = bool("signedDPA").default(false)
    val elasticsearchNodeUrl = text("elasticsearchNodeUrl").nullable()
    val elasticsearchApiKey = text("elasticsearchApiKey").nullable()
    val useCustomElasticsearch = bool("useCustomElasticsearch").default(false)
    val s3Endpoint = text("s3Endpoint").nullable()
    val s3AccessKeyId = text("s3AccessKeyId").nullable()
    val s3SecretAccessKey = text("s3SecretAccessKey").nullable()
    val s3Bucket = text("s3Bucket").nullable()
    val useCustomS3 = bool("useCustomS3").default(false)
    val sentPlanLimitAlert = pgTimestamp("sentPlanLimitAlert", 3).nullable()
    val ssoDomain = text("ssoDomain").nullable().uniqueIndex("\"Organization_ssoDomain_key\"")
    val ssoProvider = text("ssoProvider").nullable()
    val domainJoin = text("domainJoin").default("request")
    val joinerRole =
        pgEnum("joinerRole", "\"OrganizationUserRole\"", OrganizationUserRole.entries, OrganizationUserRole::dbName).default(
            OrganizationUserRole.MEMBER,
        )
    val joinDomains = array("joinDomains", TextColumnType()).nullable()
    val presenceEnabled = bool("presenceEnabled").default(true)
    val traceSharingEnabled = bool("traceSharingEnabled").default(true)
    val supportContact = text("supportContact").nullable()
    val primaryIntent = pgEnum("primaryIntent", "\"OrganizationIntent\"", OrganizationIntent.entries, OrganizationIntent::dbName).nullable()
    val promoCode = text("promoCode").nullable()
    val stripeCustomerId = text("stripeCustomerId").nullable().uniqueIndex("\"Organization_stripeCustomerId_key\"")
    val currency = pgEnum("currency", "\"Currency\"", Currency.entries, Currency::dbName).default(Currency.EUR)
    val pricingModel = pgEnum("pricingModel", "\"PricingModel\"", PricingModel.entries, PricingModel::dbName).default(PricingModel.TIERED)
    val license = text("license").nullable()
    val licenseExpiresAt = pgTimestamp("licenseExpiresAt", 3).nullable()
    val licenseLastValidatedAt = pgTimestamp("licenseLastValidatedAt", 3).nullable()
    val selfHostedCustomer = bool("selfHostedCustomer").default(false)
    val connectServicesDisabled = array("connectServicesDisabled", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val instantEvalsEnabledAt = pgTimestamp("instantEvalsEnabledAt", 3).nullable()
    val instantEvalsEnabledByUserId = text("instantEvalsEnabledByUserId").nullable()
    val connectLastSyncAt = pgTimestamp("connectLastSyncAt", 3).nullable()
    val connectLastSyncError = text("connectLastSyncError").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Organization_pkey\"")

    init {
        index("\"Organization_ssoProvider_idx\"", false, ssoProvider)
    }
}
