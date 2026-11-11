package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMJWTKeyMappingTable : IdTable<String>("\"LiteLLM_JWTKeyMapping\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val jwtIssuer = text("jwt_issuer").default("")
    val jwtClaimName = text("jwt_claim_name")
    val jwtClaimValue = text("jwt_claim_value")
    val token =
        reference(
            "token",
            LiteLLMVerificationTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_JWTKeyMapping_token_fkey\"",
        )
    val description = text("description").nullable()
    val isActive = bool("is_active").default(true)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_JWTKeyMapping_pkey\"")

    init {
        index("LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_idx", false, jwtIssuer, jwtClaimName, jwtClaimValue, isActive)
        uniqueIndex("LiteLLM_JWTKeyMapping_jwt_issuer_jwt_claim_name_jwt_claim_v_key", jwtIssuer, jwtClaimName, jwtClaimValue)
    }
}
