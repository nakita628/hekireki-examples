use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Brand")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub logo: Option<String>,
    #[sea_orm(column_name = "hideLogo", default_value = false)]
    pub hide_logo: bool,
    pub banner: Option<String>,
    #[sea_orm(column_name = "brandColor")]
    pub brand_color: Option<String>,
    #[sea_orm(column_name = "accentColor")]
    pub accent_color: Option<String>,
    #[sea_orm(column_name = "accentButtonColor")]
    pub accent_button_color: Option<String>,
    #[sea_orm(column_name = "applyAccentColorToDataroomView", default_value = false)]
    pub apply_accent_color_to_dataroom_view: bool,
    #[sea_orm(column_name = "welcomeMessage")]
    pub welcome_message: Option<String>,
    #[sea_orm(column_name = "ctaLabel")]
    pub cta_label: Option<String>,
    #[sea_orm(column_name = "ctaUrl")]
    pub cta_url: Option<String>,
    #[sea_orm(column_name = "privacyPolicyUrl")]
    pub privacy_policy_url: Option<String>,
    #[sea_orm(column_name = "cardLayout", default_value = "LIST")]
    pub card_layout: String,
    #[sea_orm(column_name = "showFolderTree", default_value = true)]
    pub show_folder_tree: bool,
    #[sea_orm(column_name = "viewerLayoutPreset", default_value = "STANDARD")]
    pub viewer_layout_preset: String,
    #[sea_orm(column_name = "viewerHeaderStyle", default_value = "DEFAULT")]
    pub viewer_header_style: String,
    #[sea_orm(column_name = "hideFolderIconsInMain", default_value = false)]
    pub hide_folder_icons_in_main: bool,
    #[sea_orm(column_name = "customLinkPreviewEnabled", default_value = false)]
    pub custom_link_preview_enabled: bool,
    #[sea_orm(column_name = "linkPreviewTitle")]
    pub link_preview_title: Option<String>,
    #[sea_orm(column_name = "linkPreviewDescription")]
    pub link_preview_description: Option<String>,
    #[sea_orm(column_name = "linkPreviewImage")]
    pub link_preview_image: Option<String>,
    #[sea_orm(column_name = "linkPreviewFavicon")]
    pub link_preview_favicon: Option<String>,
    #[sea_orm(column_name = "defaultLanguage", default_value = "en")]
    pub default_language: String,
    #[sea_orm(default_value = "Default")]
    pub name: String,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        has_many = "super::team::Entity",
        from = "Column::Id",
        to = "super::team::Column::DefaultBrandId"
    )]
    DefaultForTeam,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::dataroom::Entity")]
    Datarooms,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Datarooms.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
