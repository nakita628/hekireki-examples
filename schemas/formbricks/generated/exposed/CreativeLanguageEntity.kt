package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class CreativeLanguageEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<CreativeLanguageEntity>(CreativeLanguageTable)

    val language by LanguageEntity referencedOn CreativeLanguageTable.languageId
    val languageId by CreativeLanguageTable.languageId
    val creative by CreativeEntity referencedOn CreativeLanguageTable.creativeId
    val creativeId by CreativeLanguageTable.creativeId
    var default by CreativeLanguageTable.default
    var enabled by CreativeLanguageTable.enabled
}
