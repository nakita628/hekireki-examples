use sea_orm::entity::prelude::*;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DSyncData")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "directoryId")]
    pub directory_id: String,
    pub tenant: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<i32>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization_settings::Entity",
        from = "Column::OrganizationId",
        to = "super::organization_settings::Column::OrganizationId",
        on_delete = "Cascade"
    )]
    Org,
    #[sea_orm(has_many = "super::dsync_team_group_mapping::Entity")]
    TeamGroupMapping,
    #[sea_orm(
        has_many = "super::attribute_to_user::Entity",
        from = "Column::DirectoryId",
        to = "super::attribute_to_user::Column::CreatedByDSyncId"
    )]
    CreatedAttributeToUsers,
    #[sea_orm(
        has_many = "super::attribute_to_user::Entity",
        from = "Column::DirectoryId",
        to = "super::attribute_to_user::Column::UpdatedByDSyncId"
    )]
    UpdatedAttributeToUsers,
}

impl Related<super::organization_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Org.def()
    }
}

impl Related<super::dsync_team_group_mapping::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TeamGroupMapping.def()
    }
}

impl Related<super::attribute_to_user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedAttributeToUsers.def()
    }
}

impl ActiveModelBehavior for ActiveModel {}
