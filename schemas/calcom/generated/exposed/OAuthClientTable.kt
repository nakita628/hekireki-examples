package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthClientTable : IdTable<String>("\"OAuthClient\"") {
    override val id = text("clientId").entityId()
    val redirectUri = text("redirectUri")
    val clientSecret = text("clientSecret").nullable()
    val clientType =
        pgEnum("clientType", "\"OAuthClientType\"", OAuthClientType.entries, OAuthClientType::dbName).default(OAuthClientType.CONFIDENTIAL)
    val name = text("name")
    val purpose = text("purpose").nullable()
    val logo = text("logo").nullable()
    val websiteUrl = text("websiteUrl").nullable()
    val rejectionReason = text("rejectionReason").nullable()
    val isTrusted = bool("isTrusted").default(false)
    val status =
        pgEnum("status", "\"OAuthClientStatus\"", OAuthClientStatus.entries, OAuthClientStatus::dbName).default(OAuthClientStatus.APPROVED)
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthClient_userId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthClient_pkey\"")

    init {
        uniqueIndex("\"OAuthClient_clientId_key\"", id)
        index("\"OAuthClient_userId_idx\"", false, userId)
    }
}
