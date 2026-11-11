package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMVerifiedSubjectTable : IdTable<String>("\"LiteLLM_VerifiedSubject\"") {
    override val id = text("subject_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val issuer = text("issuer")
    val tenantId = text("tenant_id")
    val oid = text("oid")
    val kind = text("kind").default("human")
    val userId =
        optReference(
            "user_id",
            LiteLLMUserTableTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_VerifiedSubject_user_id_fkey\"",
        )
    val verifiedVia = text("verified_via").default("sso_interactive")
    val verifiedAt = pgTimestamp("verified_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_VerifiedSubject_pkey\"")

    init {
        index("\"LiteLLM_VerifiedSubject_user_id_idx\"", false, userId)
        uniqueIndex("\"LiteLLM_VerifiedSubject_issuer_tenant_id_oid_key\"", issuer, tenantId, oid)
    }
}
