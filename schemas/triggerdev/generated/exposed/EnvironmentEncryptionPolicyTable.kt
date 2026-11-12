package triggerdev.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object EnvironmentEncryptionPolicyTable : IdTable<String>("\"EnvironmentEncryptionPolicy\"") {
    override val id =
        text("environmentId")
            .references(
                RuntimeEnvironmentTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"EnvironmentEncryptionPolicy_environmentId_fkey\"",
            ).entityId()
    val revision = integer("revision").default(0)
    val wrappingProvider =
        pgEnum(
            "wrappingProvider",
            "\"EnvironmentKeyWrappingProvider\"",
            EnvironmentKeyWrappingProvider.entries,
            EnvironmentKeyWrappingProvider::dbName,
        ).nullable()
    val wrappingKeyReference = text("wrappingKeyReference").nullable()
    val activeKeyId = text("activeKeyId").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"EnvironmentEncryptionPolicy_pkey\"")

    init {
        foreignKey(
            activeKeyId to EnvironmentEncryptionKeyTable.id,
            id to EnvironmentEncryptionKeyTable.environmentId,
            onDelete = ReferenceOption.NO_ACTION,
            onUpdate = ReferenceOption.NO_ACTION,
            name = "\"EnvironmentEncryptionPolicy_activeKeyId_environmentId_fkey\"",
        )
    }
}
