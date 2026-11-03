package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object SocialMediaAgencyNicheTable : CompositeIdTable("\"SocialMediaAgencyNiche\"") {
    val agencyId =
        reference(
            "agencyId",
            SocialMediaAgencyTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SocialMediaAgencyNiche_agencyId_fkey\"",
        )
    val niche = text("niche").entityId()
    override val primaryKey = PrimaryKey(agencyId, niche, name = "\"SocialMediaAgencyNiche_pkey\"")

    init {
        addIdColumn(agencyId)
    }
}
