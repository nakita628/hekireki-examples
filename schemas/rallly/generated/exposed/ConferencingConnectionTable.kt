package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConferencingConnectionTable : IdTable<String>("conferencing_connections") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "conferencing_connections_user_id_fkey",
        )
    val provider = text("provider")
    val integrationId = text("integration_id")
    val providerAccountId = text("provider_account_id")
    val email = text("email")
    val displayName = text("display_name").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val credentialId =
        reference(
            "credential_id",
            CredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "conferencing_connections_credential_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "conferencing_connections_pkey")

    init {
        uniqueIndex("conferencing_connections_user_id_provider_provider_account__key", userId, provider, providerAccountId)
    }
}
