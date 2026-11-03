use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "NotificationPreference")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "projectUserId")]
    pub project_user_id: String,
    #[sea_orm(column_name = "linkUsageSummary", default_value = true)]
    pub link_usage_summary: bool,
    #[sea_orm(column_name = "domainConfigurationUpdates", default_value = true)]
    pub domain_configuration_updates: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project_users::Entity",
        from = "Column::ProjectUserId",
        to = "super::project_users::Column::Id",
        on_delete = "Cascade"
    )]
    ProjectUser,
}

impl Related<super::project_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ProjectUser.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
