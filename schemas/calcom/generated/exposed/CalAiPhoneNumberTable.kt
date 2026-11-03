package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CalAiPhoneNumberTable : IdTable<Int>("\"CalAiPhoneNumber\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalAiPhoneNumber_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalAiPhoneNumber_teamId_fkey\"",
        )
    val phoneNumber = text("phoneNumber").uniqueIndex("\"CalAiPhoneNumber_phoneNumber_key\"")
    val provider = text("provider")
    val providerPhoneNumberId = text("providerPhoneNumberId").nullable().uniqueIndex("\"CalAiPhoneNumber_providerPhoneNumberId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val stripeCustomerId = text("stripeCustomerId").nullable()
    val stripeSubscriptionId = text("stripeSubscriptionId").nullable().uniqueIndex("\"CalAiPhoneNumber_stripeSubscriptionId_key\"")
    val subscriptionStatus =
        pgEnum(
            "subscriptionStatus",
            "\"PhoneNumberSubscriptionStatus\"",
            PhoneNumberSubscriptionStatus.entries,
            PhoneNumberSubscriptionStatus::dbName,
        ).nullable()
    val inboundAgentId =
        optReference(
            "inboundAgentId",
            AgentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalAiPhoneNumber_inboundAgentId_fkey\"",
        )
    val outboundAgentId =
        optReference(
            "outboundAgentId",
            AgentTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CalAiPhoneNumber_outboundAgentId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"CalAiPhoneNumber_pkey\"")

    init {
        index("\"CalAiPhoneNumber_userId_idx\"", false, userId)
        index("\"CalAiPhoneNumber_teamId_idx\"", false, teamId)
        index("\"CalAiPhoneNumber_inboundAgentId_idx\"", false, inboundAgentId)
        index("\"CalAiPhoneNumber_outboundAgentId_idx\"", false, outboundAgentId)
    }
}
