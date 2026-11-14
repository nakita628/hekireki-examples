package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object GatewayApiKeyAssociationTable : IdTable<String>("gateway_api_key_associations") {
    override val id =
        text("api_key_id")
            .references(
                ApiKeyTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "gateway_api_key_associations_api_key_id_fkey",
            ).entityId()
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val allowedModels = jsonb<String>("allowed_models", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "gateway_api_key_associations_pkey")
}
