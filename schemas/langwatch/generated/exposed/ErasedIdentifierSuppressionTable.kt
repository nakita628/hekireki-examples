package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ErasedIdentifierSuppressionTable : IdTable<String>("\"ErasedIdentifierSuppression\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val provider = text("provider")
    val identifierHash = text("identifierHash")
    val erasedAt = pgTimestamp("erasedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"ErasedIdentifierSuppression_pkey\"")

    init {
        index("\"ErasedIdentifierSuppression_organizationId_idx\"", false, organizationId)
        uniqueIndex("ErasedIdentifierSuppression_organizationId_provider_identif_key", organizationId, provider, identifierHash)
    }
}
