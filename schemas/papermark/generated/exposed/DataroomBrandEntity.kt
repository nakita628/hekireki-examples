package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomBrandEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomBrandEntity>(DataroomBrandTable)

    var logo by DataroomBrandTable.logo
    var hideLogo by DataroomBrandTable.hideLogo
    var banner by DataroomBrandTable.banner
    var brandColor by DataroomBrandTable.brandColor
    var accentColor by DataroomBrandTable.accentColor
    var accentButtonColor by DataroomBrandTable.accentButtonColor
    var applyAccentColorToDataroomView by DataroomBrandTable.applyAccentColorToDataroomView
    var welcomeMessage by DataroomBrandTable.welcomeMessage
    var cardLayout by DataroomBrandTable.cardLayout
    var showFolderTree by DataroomBrandTable.showFolderTree
    var viewerLayoutPreset by DataroomBrandTable.viewerLayoutPreset
    var viewerHeaderStyle by DataroomBrandTable.viewerHeaderStyle
    var hideFolderIconsInMain by DataroomBrandTable.hideFolderIconsInMain
    var ctaLabel by DataroomBrandTable.ctaLabel
    var ctaUrl by DataroomBrandTable.ctaUrl
    var customLinkPreviewEnabled by DataroomBrandTable.customLinkPreviewEnabled
    var linkPreviewTitle by DataroomBrandTable.linkPreviewTitle
    var linkPreviewDescription by DataroomBrandTable.linkPreviewDescription
    var linkPreviewImage by DataroomBrandTable.linkPreviewImage
    var linkPreviewFavicon by DataroomBrandTable.linkPreviewFavicon
    var defaultLanguage by DataroomBrandTable.defaultLanguage
    var dataroomId by DataroomBrandTable.dataroomId
    var dataroom by DataroomEntity referencedOn DataroomBrandTable.dataroomId
    var createdAt by DataroomBrandTable.createdAt
    var updatedAt by DataroomBrandTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomBrandTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
